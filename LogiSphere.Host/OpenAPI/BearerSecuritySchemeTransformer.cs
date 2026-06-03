using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace LogiSphere.Host.Scalar;

internal sealed class BearerSecuritySchemeTransformer(
    IAuthenticationSchemeProvider authenticationSchemeProvider
) : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var schemes = await authenticationSchemeProvider.GetAllSchemesAsync();

        // If Bearer authentication isn't registered in the app pipeline, skip configuration
        if (!schemes.Any(s => s.Name == "Bearer"))
            return;

        // 1. Initialize global components collection
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

        // 2. Define what "Bearer" authentication means to Scalar
        document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Paste your raw JWT token string to authorize requests."
        };

        // 3. Clear global document-level security to prevent empty-bracket inheritance errors
        document.Security = new List<OpenApiSecurityRequirement>();

        // 4. FIX: Process operation-level authentication mapping with document context resolution
        foreach (var description in context.DescriptionGroups.SelectMany(g => g.Items))
        {
            // Resolve the path key and operation method to locate the correct OpenApiOperation block
            var pathKey = description.RelativePath != null ? $"/{description.RelativePath.TrimStart('/')}" : null;
            if (pathKey == null || !document.Paths.TryGetValue(pathKey, out var pathItem))
                continue;

            var httpMethod = description.HttpMethod;
            if (string.IsNullOrEmpty(httpMethod))
                continue;

            // Target the specific operation block matching the routing verb
            var operationEntry = pathItem.Operations!.FirstOrDefault(o => o.Key.ToString().Equals(httpMethod, StringComparison.OrdinalIgnoreCase));
            if (operationEntry.Value == null)
                continue;

            var operation = operationEntry.Value;
            var endpointMetadata = description.ActionDescriptor?.EndpointMetadata;
            if (endpointMetadata == null)
                continue;

            var hasAllowAnonymous = endpointMetadata.Any(m => m is AllowAnonymousAttribute);
            var authorizeAttributes = endpointMetadata.OfType<AuthorizeAttribute>().ToList();

            if (!hasAllowAnonymous)
            {
                // CRITICAL FIX: Pass the document reference context directly into the Reference constructor constructor
                var strictRequirement = new OpenApiSecurityRequirement
                {
                    { new OpenApiSecuritySchemeReference("Bearer", document), [] }
                };

                operation.Security = new List<OpenApiSecurityRequirement> { strictRequirement };

                // Process UI metadata descriptions
                if (authorizeAttributes.Any())
                {
                    var roles = authorizeAttributes
                        .Where(a => !string.IsNullOrEmpty(a.Roles))
                        .Select(a => a.Roles)
                        .ToList();

                    if (roles.Any())
                    {
                        operation.Description = $"**[Required Roles: {string.Join(", ", roles)}]**\n\n{operation.Description}";
                    }
                    else
                    {
                        operation.Description = $"**[Requires Authentication]**\n\n{operation.Description}";
                    }
                }
                else
                {
                    operation.Description = $"**[Requires Authentication]**\n\n{operation.Description}";
                }
            }
            else
            {
                operation.Security = new List<OpenApiSecurityRequirement>();
            }
        }
    }
}
