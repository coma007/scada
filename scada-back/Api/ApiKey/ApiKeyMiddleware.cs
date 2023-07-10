namespace scada_back.Api.ApiKey;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private string _apiKeyName;
    private string _validApiKey;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _apiKeyName = configuration.GetSection("ApiKey:ApiKeyName").Value;
        _validApiKey = configuration.GetSection("ApiKey:ValidApiKey").Value;
    }

    public async Task Invoke(HttpContext context)
    {
        var isApikeyRequired = IsApiKeyRequired(context);

        if (isApikeyRequired && !ValidateApiKey(context.Request.Headers[_apiKeyName]))
        {
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }

        await _next.Invoke(context);
    }

    private bool IsApiKeyRequired(HttpContext context)
    {
        var endpointMetadata = context.GetEndpoint()?.Metadata;

        if (endpointMetadata != null)
        {
            var hasRequireApiKeyAttribute = endpointMetadata.Any(m => m.GetType() == typeof(RequireApiKeyAttribute));
            return hasRequireApiKeyAttribute;
        }

        return false;
    }

    private bool ValidateApiKey(string apiKey)
    {
        return string.Equals(apiKey, _validApiKey, StringComparison.OrdinalIgnoreCase);
    }
}