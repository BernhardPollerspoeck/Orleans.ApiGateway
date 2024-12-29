using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Orleans.ApiGateway.RequestArgument;

public class JsonBodyArgumentResolver : IArgumentResolver
{
	#region IArgumentResolver
	public Task<TArgument?> ResolveArgument<TArgument>(HttpRequest request) where TArgument : class
	{
		return JsonSerializer.DeserializeAsync<TArgument>(request.Body).AsTask();
	}
	#endregion
}
