﻿using Newtonsoft.Json;
using Schick.Plainquire.Filter.Newtonsoft.JsonConverters;
using System.Collections.Generic;

namespace Schick.Plainquire.Filter.Newtonsoft.Extensions;

/// <summary>
/// Extension methods for <see cref="JsonConverter"/>.
/// </summary>
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
}