using Microsoft.Extensions.DependencyInjection;
using Orleans.ApiGateway.GrainKey;
using Orleans.ApiGateway.GrainType;
using Orleans.ApiGateway.RequestArgument;

namespace Orleans.ApiGateway;

public static class IServiceCollectionExtensions
{
	public static void AddOrleansGateway(this IServiceCollection self)
	{
		self.AddSingleton<IGrainKeyResolver, DefaultGrainKeyResolver>();
		self.AddSingleton<IGrainTypeResolver, DefaultGrainTypeResolver>();
		RegisterArgumentResolvers(self);
	}

	public static void AddOrleansGateway<TGrainKeyResolver, TGrainTypeResolver>(this IServiceCollection self)
		where TGrainKeyResolver : class, IGrainKeyResolver
		where TGrainTypeResolver : class, IGrainTypeResolver
	{
		self.AddSingleton<IGrainKeyResolver, TGrainKeyResolver>();
		self.AddSingleton<IGrainTypeResolver, TGrainTypeResolver>();
		RegisterArgumentResolvers(self);
	}

	private static void RegisterArgumentResolvers(IServiceCollection services)
	{
		services.AddKeyedSingleton<IArgumentResolver, HeaderArgumentResolver>(EArgumentType.Header);
		services.AddKeyedSingleton<IArgumentResolver, JsonBodyArgumentResolver>(EArgumentType.JsonBody);
	}

}
