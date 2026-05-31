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

        if (!schemes.Any(s => s.Name == "Bearer"))
            return;

        document.Components ??= new OpenApiComponents();

        if (document.Components.SecuritySchemes == null)
            document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>();

        var schemeId = "Bearer";

        document.Components.SecuritySchemes[schemeId] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization"
        };

        var schemeReference = new OpenApiSecuritySchemeReference(schemeId);
        var strictRequirement = new OpenApiSecurityRequirement();

        foreach (var group in context.DescriptionGroups)
        {
            foreach (var description in group.Items)
            {
                var relativePath = description.RelativePath?.Trim('/');
                if (string.IsNullOrEmpty(relativePath)) continue;

                var formattedPathKey = $"/{relativePath}";
                if (!document.Paths.ContainsKey(formattedPathKey)) continue;

                var pathItem = document.Paths[formattedPathKey];
                var httpMethod = description.HttpMethod?.ToUpperInvariant();
                if (string.IsNullOrEmpty(httpMethod)) continue;

                var operationKey = pathItem.Operations!.Keys.FirstOrDefault(k =>
                    k.ToString().Equals(httpMethod, StringComparison.OrdinalIgnoreCase));

                if (!pathItem.Operations.TryGetValue(operationKey!, out var operation)) continue;

                var endpointMetadata = description.ActionDescriptor?.EndpointMetadata;
                if (endpointMetadata == null) continue;

                var hasAllowAnonymous = endpointMetadata.Any(m => m is AllowAnonymousAttribute);
                var authorizeAttributes = endpointMetadata.OfType<AuthorizeAttribute>().ToList();

                if (!hasAllowAnonymous)
                {
                    operation.Security = new List<OpenApiSecurityRequirement>
                    {
                        new OpenApiSecurityRequirement
                        {
                            [new OpenApiSecuritySchemeReference(schemeId)] = new List<string>()
                        }
                    };

                    if (authorizeAttributes.Any())
                    {
                        var roles = authorizeAttributes
                            .Where(a => !string.IsNullOrEmpty(a.Roles))
                            .Select(a => a.Roles);

                        if (roles.Any())
                        {
                            operation.Description = $"**[Required Roles: {string.Join(", ", roles)}]**\n\n{operation.Description}";
                        }
                        else
                        {
                            operation.Description = $"**[Requires Authentication]**\n\n{operation.Description}";
                        }
                    }
                }
                else
                {
                    operation.Security = new List<OpenApiSecurityRequirement>();
                }
            }

        }
    }
}
