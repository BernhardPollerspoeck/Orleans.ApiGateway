using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Orleans.ApiGateway.GrainKey;
using Orleans.ApiGateway.GrainType;
using Orleans.ApiGateway.RequestArgument;
using Orleans.Runtime;

namespace Orleans.ApiGateway;

public static class IEndpointRouteBuilderExtensions
{
	public static RouteHandlerBuilder MapOrleans<TGrain, TResult>(
		this IEndpointRouteBuilder self,
		EHttpMethod method,
		string route,
		Func<TGrain, HttpContext, Task<TResult>> action)
		where TGrain : IGrain
		where TResult : class
	{
		return self.MapMethods(route, [method.ToString()],
			async (IClusterClient clusterClient, HttpContext context, IServiceProvider serviceProvider) =>
			{
				var grainResult = GetGrain<TGrain>(clusterClient, context, serviceProvider);
				if (grainResult is { Result: not null })
				{
					return grainResult.Result;
				}
				if (grainResult is { Grain: null })
				{
					return Results.BadRequest("Could not resolve grain");
				}

				var result = await action(grainResult.Grain, context);
				return Results.Ok(result);
			});
	}

	public static RouteHandlerBuilder MapOrleans<TGrain, TRequest, TResult>(
		this IEndpointRouteBuilder self,
		EHttpMethod method,
		string route,
		EArgumentType argumentType,
		Func<TGrain, TRequest, HttpContext, Task<TResult>> action)
		where TGrain : IGrain
		where TRequest : class
		where TResult : class
	{
		return self.MapMethods(route, [method.ToString()],
			async (IClusterClient clusterClient, HttpContext context, IServiceProvider serviceProvider) =>
			{
				var grainResult = GetGrain<TGrain>(clusterClient, context, serviceProvider);
				if (grainResult is { Result: not null })
				{
					return grainResult.Result;
				}
				if (grainResult is { Grain: null })
				{
					return Results.BadRequest("Could not resolve grain");
				}

				var argumentResolver = serviceProvider.GetRequiredKeyedService<IArgumentResolver>(argumentType);
				var argument = argumentResolver.ResolveArgument<TRequest>(context.Request);
				if (argument is null)
				{
					return Results.BadRequest("Missing 'argument'");
				}
				var result = await action(grainResult.Grain, argument, context);
				return Results.Ok(result);
			});
	}

	private static (TGrain? Grain, IResult? Result) GetGrain<TGrain>(IClusterClient clusterClient, HttpContext context, IServiceProvider serviceProvider)
		where TGrain : IGrain
	{
		var keyResolver = serviceProvider.GetRequiredService<IGrainKeyResolver>();
		var key = keyResolver.Resolvekey(context);
		if (key is null)
		{
			return (default, Results.BadRequest("Missing 'key'"));
		}

		var grainTypeResolver = serviceProvider.GetRequiredService<IGrainTypeResolver>();
		var type = grainTypeResolver.ResolveType(context);
		if (type is null)
		{
			return (default, Results.BadRequest("Missing 'type'"));
		}

		var grainId = GrainId.Create(type, key);
		return (clusterClient.GetGrain<TGrain>(grainId), null);
	}
}
