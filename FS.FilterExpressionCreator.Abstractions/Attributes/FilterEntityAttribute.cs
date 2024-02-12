﻿using System;

namespace FS.FilterExpressionCreator.Abstractions.Attributes;

/// <summary>
/// Entity filter expression related settings.
/// Implements <see cref="Attribute" />
/// </summary>
/// <seealso cref="Attribute" />
/// <autogeneratedoc />
[AttributeUsage(AttributeTargets.Class)]
public class FilterEntityAttribute : Attribute
{
    /// <summary>
    /// The default name for the query parameter used to sort entities.
    /// </summary>
    public const string DEFAULT_SORT_BY_PARAMETER_NAME = "orderBy";

    /// <summary>
    /// Specify the prefix to use when using <see><cref>EntityFilter{TEntity}</cref></see> or <see><cref>EntitySort{TEntity}</cref></see> from MVC controllers.
    /// Default is the name of the filtered class (e.g. for <c>EntityFilter&lt;Person&gt;</c> the prefix is <c>Person)</c>.
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// The name of the query parameter used to sort the entities.
    /// </summary>
    [Obsolete("Parameter name is taken from action parameter (e.g. GetOrders(EntitySort<Order> orderBy, ...) or FromQuery-attribute ([FromQuery(Name = \"customName\")]).")]
    public string SortByParameter { get; set; } = DEFAULT_SORT_BY_PARAMETER_NAME;
}