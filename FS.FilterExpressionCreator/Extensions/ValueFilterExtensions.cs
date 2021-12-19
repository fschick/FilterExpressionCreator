﻿using FS.FilterExpressionCreator.Enums;
using FS.FilterExpressionCreator.Filters;
using System;
using System.Linq;

namespace FS.FilterExpressionCreator.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ValueFilter"/>.
    /// </summary>
    public static class ValueFilterExtensions
    {
        /// <summary>
        /// Humanizes the filter syntax.
        /// </summary>
        /// <typeparam name="TValue">The type of the filtered value.</typeparam>
        /// <param name="filterSyntax">The filter syntax.</param>
        /// <param name="valueName">Name of the value.</param>
        public static string HumanizeFilterSyntax<TValue>(this string filterSyntax, string valueName)
        {
            if (string.IsNullOrWhiteSpace(filterSyntax))
                return $"{valueName} is unfiltered";

            var filters = ValueFilterFactory
                .Create(filterSyntax)
                .GroupBy(x => x.Operator)
                .Select(filterGroup =>
                {
                    var filterOperator = filterGroup.Key;
                    var operatorName = filterOperator.GetOperatorName<TValue>();

                    if (filterOperator == FilterOperator.IsNull || filterOperator == FilterOperator.NotNull)
                        return $"{valueName} {operatorName}";

                    var filterValues = filterGroup.Select(x => x.Value).ToArray();
                    var valuesButLast = filterValues[..^1];
                    var prefixValueList = string.Join("', '", valuesButLast);
                    //var concatKey = filter.Operator == FilterOperator.NotEqual ? "nor" : "or";
                    var valueList = !string.IsNullOrEmpty(prefixValueList)
                        ? $"'{prefixValueList}' or '{filterValues[^1]}'"
                        : $"'{filterValues[^1]}'";

                    return $"{valueName} {operatorName} {valueList}";
                });

            return string.Join(" or ", filters);
        }

        private static string GetOperatorName<TValue>(this FilterOperator filterOperator)
        {
            filterOperator = filterOperator != FilterOperator.Default ? filterOperator : GetDefaultOperator<TValue>();
            return filterOperator switch
            {
                FilterOperator.Contains => "contains",
                FilterOperator.EqualCaseSensitive => "is (case sensitive)",
                FilterOperator.EqualCaseInsensitive => "is",
                FilterOperator.NotEqual => "is not",
                FilterOperator.LessThanOrEqual => "is less than or equal to",
                FilterOperator.LessThan => "is less than",
                FilterOperator.GreaterThanOrEqual => "is greater than or equal to",
                FilterOperator.GreaterThan => "is greater than",
                FilterOperator.IsNull => "is null",
                FilterOperator.NotNull => "is not null",
                _ => throw new ArgumentOutOfRangeException(nameof(filterOperator))
            };
        }

        private static FilterOperator GetDefaultOperator<TValue>()
            => typeof(TValue) == typeof(string)
                ? FilterOperator.Contains
                : FilterOperator.EqualCaseInsensitive;
    }
}
