﻿using Schick.Plainquire.Sort.Abstractions.Configurations;
using Schick.Plainquire.Sort.Sorts;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Schick.Plainquire.Sort.Interfaces;

/// <summary>
/// Interceptor to provide custom sort logic for sorted properties.
/// </summary>
/// <autogeneratedoc />
public interface ISortInterceptor
{
    /// <summary>
    /// Sorts the elements of a sequence according to the given <paramref name="sort"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the sorted entity.</typeparam>
    /// <param name="source">A sequence of values to sort.</param>
    /// <param name="sort">The sort order.</param>
    /// <param name="configuration">The active sort order configuration</param>
    /// <returns>An <see cref="IOrderedEnumerable{TEntity}"/> with applied sort order or <c>null</c> to use the default sort order.</returns>
    [SuppressMessage("ReSharper", "ParameterTypeCanBeEnumerable.Global", Justification = "Required for functionallity.")]
    public IOrderedQueryable<TEntity>? OrderBy<TEntity>(IQueryable<TEntity> source, PropertySort sort, SortConfiguration? configuration = null);

    /// <summary>
    /// Performs a subsequent ordering of a sequence according to the given <paramref name="sort"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the sorted entity.</typeparam>
    /// <param name="source">A sequence of values to sort.</param>
    /// <param name="sort">The sort order.</param>
    /// <param name="configuration">The active sort order configuration</param>
    /// <returns>An <see cref="IOrderedEnumerable{TEntity}"/> with applied sort order or <c>null</c> to use the default sort order.</returns>
    [SuppressMessage("ReSharper", "ParameterTypeCanBeEnumerable.Global", Justification = "Required for functionallity.")]
    public IOrderedQueryable<TEntity>? ThenBy<TEntity>(IOrderedQueryable<TEntity> source, PropertySort sort, SortConfiguration? configuration = null);
}