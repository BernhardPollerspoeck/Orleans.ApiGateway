using Microsoft.AspNetCore.Http;

namespace Orleans.ApiGateway.RequestArgument;

public class HeaderArgumentResolver : IArgumentResolver
{
	#region const
	private const string ARGUMENT = "argument";
	#endregion

	#region IArgumentResolver
	public Task<TArgument?> ResolveArgument<TArgument>(HttpRequest request)
		where TArgument : class
	{
		return Task.FromResult(request.Headers.TryGetValue(ARGUMENT, out var value)
			? value.ToString() as TArgument
			: null);
	}
	#endregion
}
