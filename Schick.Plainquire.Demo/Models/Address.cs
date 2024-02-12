﻿using Schick.Plainquire.Filter.Abstractions.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace Schick.Plainquire.Demo.Models;

/// <summary>
/// Address.
/// </summary>
[FilterEntity]
[SuppressMessage("ReSharper", "EntityFramework.ModelValidation.UnlimitedStringLength")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "Required by EF")]
public class Address
{
    /// <summary>
    /// Street.
    /// </summary>
    public string? Street { get; init; }

    /// <summary>
    /// City.
    /// </summary>
    public string? City { get; init; }
}