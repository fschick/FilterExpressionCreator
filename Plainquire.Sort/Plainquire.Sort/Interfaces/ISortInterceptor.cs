﻿using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Plainquire.Sort;

/// <summary>
/// Interceptor to provide custom sort logic for sorted properties.
/// </summary>
/// <autogeneratedoc />
public interface ISortInterceptor
{
    /// <summary>
    /// Default sort interceptor used when no other interceptor is provided.
    /// </summary>
    public static ISortInterceptor? Default { get; set; }

    /// <summary>
    /// Sorts the elements of a sequence according to the given <paramref name="sort"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the sorted entity.</typeparam>
    /// <param name="source">A sequence of values to sort.</param>
    /// <param name="sort">The sort order.</param>
    /// <returns>An <see cref="IOrderedEnumerable{TEntity}"/> with applied sort order or <c>null</c> to use the default sort order.</returns>
    [SuppressMessage("ReSharper", "ParameterTypeCanBeEnumerable.Global", Justification = "Required for functionallity.")]
    public IOrderedQueryable<TEntity>? OrderBy<TEntity>(IQueryable<TEntity> source, PropertySort sort);

    /// <summary>
    /// Performs a subsequent ordering of a sequence according to the given <paramref name="sort"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the sorted entity.</typeparam>
    /// <param name="source">A sequence of values to sort.</param>
    /// <param name="sort">The sort order.</param>
    /// <returns>An <see cref="IOrderedEnumerable{TEntity}"/> with applied sort order or <c>null</c> to use the default sort order.</returns>
    [SuppressMessage("ReSharper", "ParameterTypeCanBeEnumerable.Global", Justification = "Required for functionallity.")]
    public IOrderedQueryable<TEntity>? ThenBy<TEntity>(IOrderedQueryable<TEntity> source, PropertySort sort);
}