using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Orleans.ApiGateway.RequestArgument;

public class JsonBodyArgumentResolver : IArgumentResolver
{
    #region IArgumentResolver
    public TArgument? ResolveArgument<TArgument>(HttpRequest request) where TArgument : class
    {
        return JsonSerializer.Deserialize<TArgument>(request.Body);
    }
    #endregion
}
