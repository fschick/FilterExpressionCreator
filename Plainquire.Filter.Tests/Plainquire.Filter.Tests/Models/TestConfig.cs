﻿using Plainquire.Filter.Abstractions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Plainquire.Filter.Tests.Models;

[ExcludeFromCodeCoverage]
public static class TestConfig
{
    public static readonly FilterConfiguration IgnoreParseExceptions = new() { IgnoreParseExceptions = true };
    public static readonly FilterConfiguration FilterCultureEnUs = new() { CultureName = "en-Us" };
    public static readonly FilterConfiguration FilterCultureDeDe = new() { CultureName = "de-DE", BooleanMap = new Dictionary<string, bool> { ["NEIN"] = false, ["0"] = false, ["JA"] = true, ["1"] = true } };
}