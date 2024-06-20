﻿using System;
using System.Collections.Generic;

namespace Plainquire.Filter.Abstractions;

/// <summary>
/// Represents a range of <typeparamref name="TType"/>.
/// </summary>
public class Range<TType> : IEquatable<Range<TType>>, IConvertible where TType : IComparable<TType>
{
    /// <summary>
    /// The start of range.
    /// </summary>
    public TType? Start { get; }

    /// <summary>
    /// The end of range.
    /// </summary>
    public TType? End { get; }

    /// <summary>
    /// Gets the distance between <see cref="Start"/> and <see cref="End"/>.
    /// </summary>
    /// <autogeneratedoc />
    public double Distance => ToDouble(End) - ToDouble(Start);

    /// <summary>
    /// Initializes a new instance of the <see cref="Range{TType}"/> class.
    /// </summary>
    /// <param name="start">The start of range.</param>
    /// <param name="end">The end of range.</param>
    public Range(TType? start, TType? end)
    {
        Start = start;
        End = end;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        var start = ToString(Start);
        var end = ToString(End);

        return $"{start}_{end}";

        //if (start != null && end != null)
        //    return $"{start}_{end}";
        //if (start != null)
        //    return start;
        //if (end != null)
        //    return end;
        //return string.Empty;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => Equals(obj as Range<TType>);

    /// <inheritdoc />
    public bool Equals(Range<TType>? other)
        => other != null && EqualityComparer<TType?>.Default.Equals(Start, other.Start) && EqualityComparer<TType?>.Default.Equals(End, other.End);

    /// <inheritdoc />
    public override int GetHashCode()
        => HashCode.Combine(Start, End);

    /// <summary>
    /// Indicates whether the <paramref name="val1"/> object is equal to <paramref name="val2"/> object.
    /// </summary>
    /// <param name="val1">The object to compare with <paramref name="val2"/>.</param>
    /// <param name="val2">The object to compare with <paramref name="val1"/>.</param>
    public static bool operator ==(Range<TType>? val1, Range<TType>? val2)
        => EqualityComparer<Range<TType>?>.Default.Equals(val1, val2);

    /// <summary>
    /// Indicates whether the <paramref name="val1"/> object is not equal to <paramref name="val2"/> object.
    /// </summary>
    /// <param name="val1">The object to compare with <paramref name="val2"/>.</param>
    /// <param name="val2">The object to compare with <paramref name="val1"/>.</param>
    public static bool operator !=(Range<TType>? val1, Range<TType>? val2)
        => !(val1 == val2);

    /// <summary>
    /// Indicates whether the range of <paramref name="val1"/> is lower than the range of <paramref name="val2"/> object.
    /// </summary>
    /// <param name="val1">The object to compare with <paramref name="val2"/>.</param>
    /// <param name="val2">The object to compare with <paramref name="val1"/>.</param>
    public static bool operator <(Range<TType> val1, Range<TType> val2)
        => ((IConvertible)val1).ToDecimal(null) < ((IConvertible)val2).ToDecimal(null);

    /// <summary>
    /// Indicates whether the range of <paramref name="val1"/> is greater than the range of <paramref name="val2"/> object.
    /// </summary>
    /// <param name="val1">The object to compare with <paramref name="val2"/>.</param>
    /// <param name="val2">The object to compare with <paramref name="val1"/>.</param>
    public static bool operator >(Range<TType> val1, Range<TType> val2)
        => ((IConvertible)val1).ToDecimal(null) > ((IConvertible)val2).ToDecimal(null);

    /// <summary>
    /// Indicates whether the range of <paramref name="val1"/> is lower than or equal to the range of <paramref name="val2"/> object.
    /// </summary>
    /// <param name="val1">The object to compare with <paramref name="val2"/>.</param>
    /// <param name="val2">The object to compare with <paramref name="val1"/>.</param>
    public static bool operator <=(Range<TType> val1, Range<TType> val2)
        => ((IConvertible)val1).ToDecimal(null) <= ((IConvertible)val2).ToDecimal(null);

    /// <summary>
    /// Indicates whether the range of <paramref name="val1"/> is greater than or equal to the range of <paramref name="val2"/> object.
    /// </summary>
    /// <param name="val1">The object to compare with <paramref name="val2"/>.</param>
    /// <param name="val2">The object to compare with <paramref name="val1"/>.</param>
    public static bool operator >=(Range<TType> val1, Range<TType> val2)
        => ((IConvertible)val1).ToDecimal(null) >= ((IConvertible)val2).ToDecimal(null);

    private static string? ToString(TType? value)
        => value switch
        {
            null => null,
            DateTime dateTime => dateTime.ToString("o"),
            DateTimeOffset dateTime => dateTime.ToString("o"),
            _ => value.ToString()
        };

    private static double ToDouble(TType? val)
        => val switch
        {
            null => 0,
            DateTime dateTime => dateTime.Ticks,
            DateTimeOffset dateTime => dateTime.Ticks,
            IConvertible convertible => convertible.ToDouble(null),
            _ => throw new InvalidOperationException($"The type {typeof(TType).Name} is not convertible to {nameof(Double)}")
        };

    #region IConvertible
    TypeCode IConvertible.GetTypeCode()
        => TypeCode.Object;

    bool IConvertible.ToBoolean(IFormatProvider provider)
        => throw new InvalidCastException($"Invalid cast from {nameof(Range)} to {nameof(Boolean)}");

    char IConvertible.ToChar(IFormatProvider provider)
        => throw new InvalidCastException($"Invalid cast from {nameof(Range)} to {nameof(Char)}");

    DateTime IConvertible.ToDateTime(IFormatProvider provider)
        => throw new InvalidCastException($"Invalid cast from {nameof(Range)} to {nameof(DateTime)}");

    string IConvertible.ToString(IFormatProvider provider)
        => this.ToString();

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
    {
        try
        {
            return ((IConvertible)((IConvertible)this).ToDouble(provider)).ToType(conversionType, provider);
        }
        catch (OverflowException)
        {
            throw new OverflowException($"Range was too large for a {conversionType.Name}");
        }
    }

    byte IConvertible.ToByte(IFormatProvider provider)
    {
        var result = ToDouble(End) - ToDouble(Start);
        if (result <= byte.MaxValue)
            return (byte)result;
        throw new OverflowException($"Range was too large for a {nameof(Byte)}");
    }

    decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
        try
        {
            return Convert.ToDecimal(ToDouble(End) - ToDouble(Start));
        }
        catch (OverflowException)
        {
            throw new OverflowException($"Range was too large for a {nameof(Decimal)}");
        }
    }

    double IConvertible.ToDouble(IFormatProvider provider)
        => ToDouble(End) - ToDouble(Start);

    short IConvertible.ToInt16(IFormatProvider provider)
    {
        var result = ToDouble(End) - ToDouble(Start);
        if (result <= short.MaxValue)
            return (short)result;
        throw new OverflowException($"Range was too large for a {nameof(Int16)}");
    }

    int IConvertible.ToInt32(IFormatProvider provider)
    {
        var result = ToDouble(End) - ToDouble(Start);
        if (result <= int.MaxValue)
            return (int)result;
        throw new OverflowException($"Range was too large for a {nameof(Int32)}");
    }

    long IConvertible.ToInt64(IFormatProvider provider)
    {
        var result = ToDouble(End) - ToDouble(Start);
        if (result <= long.MaxValue)
            return (long)result;
        throw new OverflowException($"Range was too large for a {nameof(Int64)}");
    }

    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
        var result = ToDouble(End) - ToDouble(Start);
        if (result <= sbyte.MaxValue)
            return (sbyte)result;
        throw new OverflowException($"Range was too large for a {nameof(SByte)}");
    }

    float IConvertible.ToSingle(IFormatProvider provider)
    {
        var result = ToDouble(End) - ToDouble(Start);
        if (result < float.MaxValue)
            return (float)result;
        throw new OverflowException($"Range was too large for a {nameof(Single)}");
    }

    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
        var result = ToDouble(End) - ToDouble(Start);
        if (result <= ushort.MaxValue)
            return (ushort)result;
        throw new OverflowException($"Range was too large for a {nameof(UInt16)}");
    }

    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
        var result = ToDouble(End) - ToDouble(Start);
        if (result <= uint.MaxValue)
            return (uint)result;
        throw new OverflowException($"Range was too large for a {nameof(UInt32)}");
    }

    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
        var result = ToDouble(End) - ToDouble(Start);
        if (result <= ulong.MaxValue)
            return (ulong)result;
        throw new OverflowException($"Range was too large for a {nameof(UInt64)}");
    }
    #endregion
}

/// <summary>
/// Utility class to create a <see cref="Range{TType}"/>.
/// </summary>
public static class Range
{
    /// <summary>
    /// Creates a <see cref="Range{TType}"/>.
    /// </summary>
    /// <typeparam name="TType">The type of the values represented by range.</typeparam>
    /// <param name="start">The start of range.</param>
    /// <param name="end">The end of range.</param>
    public static Range<TType> Create<TType>(TType? start, TType? end) where TType : IComparable<TType>
        => new(start, end);
}