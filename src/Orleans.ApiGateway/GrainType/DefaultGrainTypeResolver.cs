using Microsoft.AspNetCore.Http;

namespace Orleans.ApiGateway.GrainType;

public class DefaultGrainTypeResolver : IGrainTypeResolver
{
    #region const
    private const string GRAIN_TYPE = "grainType";
    #endregion

    #region IGrainTypeResolver
    public string? ResolveType(HttpContext context)
    {
        return context.Request.Headers.TryGetValue(GRAIN_TYPE, out var value)
            ? Uri.UnescapeDataString(value.ToString())
            : null;
    }
    #endregion
}
