﻿using FS.FilterExpressionCreator.Demo.Extensions;
using FS.FilterExpressionCreator.Demo.Routing;
using FS.FilterExpressionCreator.Swashbuckle.Extensions;
using FS.SortQueryableCreator.Swashbuckle.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace FS.FilterExpressionCreator.Demo.Startup;

[SuppressMessage("ReSharper", "InconsistentNaming")]
internal static class OpenApi
{
    private const string OPEN_API_UI_ROUTE = "openapi/";
    private const string OPEN_API_SPEC = "openapi.json";

    internal static IApplicationBuilder RegisterOpenApiRoutes(this IApplicationBuilder applicationBuilder)
        => applicationBuilder
            .UseSwagger(c => c.RouteTemplate = $"{OPEN_API_UI_ROUTE}{{documentName}}/{OPEN_API_SPEC}")
            .UseSwaggerUI(c =>
            {
                c.RoutePrefix = OPEN_API_UI_ROUTE.Trim('/');
                c.SwaggerEndpoint($"{V1ApiController.API_VERSION}/{OPEN_API_SPEC}", $"API version {V1ApiController.API_VERSION}");
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableTryItOutByDefault();
                c.ConfigObject.AdditionalItems.Add("requestSnippetsEnabled", true);
                c.InjectStylesheet("/css/swagger-ui/custom.css");
            });

    internal static IServiceCollection RegisterOpenApiController(this IServiceCollection services)
    {
        var filterExpressionCreatorDoc = Path.Combine(AppContext.BaseDirectory, "FS.FilterExpressionCreator.xml");
        var sortCreatorDoc = Path.Combine(AppContext.BaseDirectory, "FS.SortQueryableCreator.xml");
        var filterExpressionCreatorDemoDoc = Path.Combine(AppContext.BaseDirectory, "FS.FilterExpressionCreator.Demo.xml");

        return services
            .AddSwaggerGenNewtonsoftSupport()
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc(V1ApiController.API_VERSION, new OpenApiInfo { Title = $"{AssemblyExtensions.GetProgramProduct()} API", Version = V1ApiController.API_VERSION });
                c.AddFilterExpressionSupport(filterExpressionCreatorDoc, filterExpressionCreatorDemoDoc);
                c.AddSortQueryableSupport(sortCreatorDoc, filterExpressionCreatorDemoDoc);
                c.IncludeXmlComments(filterExpressionCreatorDoc);
                c.IncludeXmlComments(sortCreatorDoc);
                c.IncludeXmlComments(filterExpressionCreatorDemoDoc);
            });
    }
}