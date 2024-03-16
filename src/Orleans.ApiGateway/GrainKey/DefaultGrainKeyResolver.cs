using Microsoft.AspNetCore.Http;

namespace Orleans.ApiGateway.GrainKey;

internal class DefaultGrainKeyResolver : IGrainKeyResolver
{
    #region const
    private const string GRAIN_KEY = "grainKey";
    #endregion

    #region IGrainKeyResolver
    public string? Resolvekey(HttpContext context)
    {
        return context.Request.Headers.TryGetValue(GRAIN_KEY, out var value)
            ? Uri.UnescapeDataString(value.ToString())
            : null;
    }
    #endregion
}
