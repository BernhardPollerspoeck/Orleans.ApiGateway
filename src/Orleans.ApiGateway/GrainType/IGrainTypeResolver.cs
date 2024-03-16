using Microsoft.AspNetCore.Http;

namespace Orleans.ApiGateway.GrainType;

public interface IGrainTypeResolver
{
    string? ResolveType(HttpContext context);
}
