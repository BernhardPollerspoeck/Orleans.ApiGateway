using Microsoft.AspNetCore.Http;

namespace Orleans.ApiGateway.RequestArgument;

public interface IArgumentResolver
{
    Task<TArgument?> ResolveArgument<TArgument>(HttpRequest request)
        where TArgument : class;
}
