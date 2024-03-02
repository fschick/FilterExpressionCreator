﻿using System;
using System.Globalization;

namespace Plainquire.Filter.Abstractions;

/// <summary>
/// Holds filter specific configuration.
/// </summary>
public class FilterConfiguration
{
    /// <summary>
    /// The culture to use for parsing.
    /// </summary>
    public CultureInfo CultureInfo { get; set; } = CultureInfo.CurrentCulture;

    /// <summary>
    /// A delegate returning the value for 'now' when parsing relative date/time values (e.g. one-week-ago). Defaults to <see cref="DateTimeOffset.Now"/>.
    /// </summary>
    public Func<DateTimeOffset> Now { get; set; } = () => DateTimeOffset.Now;

    // TODO: Think about to handle "Unable to parse given filter value"        
    /// <summary>
    /// Return <c>x => true</c> in case of any exception while parsing the value
    /// </summary>
    public bool IgnoreParseExceptions { get; set; }
}