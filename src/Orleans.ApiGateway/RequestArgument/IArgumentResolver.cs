using Microsoft.AspNetCore.Http;

namespace Orleans.ApiGateway.RequestArgument;

public interface IArgumentResolver
{
    TArgument? ResolveArgument<TArgument>(HttpRequest request)
        where TArgument : class;
}
