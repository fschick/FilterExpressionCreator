﻿using FS.FilterExpressionCreator.Abstractions.Models;
using FS.FilterExpressionCreator.Filters;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace FS.FilterExpressionCreator.Interfaces;

/// <summary>
/// Interceptor to provide custom filter logic for filtered properties.
/// </summary>
/// <autogeneratedoc />
public interface IPropertyFilterInterceptor
{
    /// <summary>
    /// Creates a property filter for given values. Called for every filtered property.
    /// </summary>
    /// <typeparam name="TEntity">The type of the filtered entity.</typeparam>
    /// <param name="propertyInfo">The filtered property belongs to <typeparamref name="TEntity"/>.</param>
    /// <param name="filters">The filter values.</param>
    /// <param name="configuration">The active filter configuration</param>
    /// <returns>An expression to used to filter the given property or <c>null</c> to use the default filter creator.</returns>
    public Expression<Func<TEntity, bool>>? CreatePropertyFilter<TEntity>(PropertyInfo propertyInfo, ValueFilter[] filters, FilterConfiguration configuration);
}