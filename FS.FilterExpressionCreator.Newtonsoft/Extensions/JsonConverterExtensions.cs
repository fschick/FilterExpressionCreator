﻿using FS.FilterExpressionCreator.Newtonsoft.JsonConverters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FS.FilterExpressionCreator.Newtonsoft.Extensions;

/// <summary>
/// Extension methods for <see cref="JsonConverter"/>.
/// </summary>
[Obsolete("Use 'Plainquire.Filter.Newtonsoft.JsonConverterExtensions' instead.")]
public static class JsonConverterExtensions
{
    /// <summary>
    /// List of Newtonsoft JSON converters required to serialize/deserialize filter expression related classes.
    /// </summary>
    public static readonly JsonConverter[] NewtonsoftConverters =
    [
        new ValueFilterConverter(),
        new EntityFilterConverter()
    ];

    /// <summary>
    /// Adds support to serialize/deserialize filter expression creator related classes.
    /// </summary>
    /// <param name="converters">The converters.</param>
    /// <autogeneratedoc />
    public static void AddFilterExpressionNewtonsoftSupport(this IList<JsonConverter> converters)
    {
        foreach (var converter in NewtonsoftConverters)
            converters.Add(converter);
    }

    /// <inheritdoc cref="AddFilterExpressionNewtonsoftSupport(IList{JsonConverter})" />
    [Obsolete("Use Plainquire.Filter.Newtonsoft.JsonConverterExtensions.AddFilterNewtonsoftSupport instead.")]
    public static void AddFilterExpressionsNewtonsoftSupport(this IList<JsonConverter> converters)
        => AddFilterExpressionNewtonsoftSupport(converters);
}