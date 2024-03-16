using Microsoft.AspNetCore.Http;

namespace Orleans.ApiGateway.GrainKey;

public interface IGrainKeyResolver
{
    string? Resolvekey(HttpContext context);
}
