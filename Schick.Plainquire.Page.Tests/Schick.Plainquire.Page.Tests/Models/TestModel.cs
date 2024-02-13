﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Schick.Plainquire.Page.Tests.Models;

[ExcludeFromCodeCoverage]
[DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
public class TestModel<TValue> : IComparable
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public TValue? Value { get; init; }

    [ExcludeFromCodeCoverage]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? DebuggerDisplay => Value?.ToString();

    public TestModel() { }

    public TestModel(TValue? value)
        => Value = value;

    public int CompareTo(object? obj)
    {
        if (Value is IComparable comparable && obj is TestModel<TValue> other)
            return comparable.CompareTo(other.Value);

        return 0;
    }
}