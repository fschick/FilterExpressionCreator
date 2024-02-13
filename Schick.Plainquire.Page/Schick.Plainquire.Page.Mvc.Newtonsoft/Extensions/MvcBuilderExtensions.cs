﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Schick.Plainquire.Page.Newtonsoft.Extensions;

namespace Schick.Plainquire.Page.Mvc.Newtonsoft.Extensions;

/// <summary>
/// Extensions to register entity page extensions to MVC
/// </summary>
public static class MvcBuilderExtensions
{
    /// <summary>
    /// Adds support to serialize/deserialize page related classes to MVC pipeline.
    /// </summary>
    /// <param name="mvcBuilder">The MVC builder.</param>
    /// <autogeneratedoc />
    public static IMvcBuilder AddPageNewtonsoftSupport(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.Services.Configure<MvcNewtonsoftJsonOptions>(options =>
        {
            options.SerializerSettings.Converters.AddPageNewtonsoftSupport();
        });

        return mvcBuilder;
    }
}