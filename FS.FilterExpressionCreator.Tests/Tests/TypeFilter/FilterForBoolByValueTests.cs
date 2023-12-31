﻿using FS.FilterExpressionCreator.Enums;
using FS.FilterExpressionCreator.Exceptions;
using FS.FilterExpressionCreator.Tests.Attributes;
using FS.FilterExpressionCreator.Tests.Extensions;
using FS.FilterExpressionCreator.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace FS.FilterExpressionCreator.Tests.Tests.TypeFilter;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[TestClass, ExcludeFromCodeCoverage]
public class FilterForBoolByValueTests : TestBase<bool>
{
    [DataTestMethod]
    [FilterTestDataSource(nameof(_testCases), nameof(TestModelFilterFunctions))]
    public void FilterForBoolByValue_WorksAsExpected(FilterTestCase<bool, bool> testCase, TestModelFilterFunc<bool> filterFunc)
        => testCase.Run(_testItems, filterFunc);

    private static readonly TestModel<bool>[] _testItems =
    [
        new() { ValueA = true },
        new() { ValueA = false }
    ];

    // ReSharper disable RedundantExplicitArrayCreation
    private static readonly FilterTestCase<bool, bool>[] _testCases =
    [
        FilterTestCase.Create(1100, FilterOperator.Default, [true], (bool x) => x),
        FilterTestCase.Create(1101, FilterOperator.Default, [false], (bool x) => !x),
        FilterTestCase.Create(1102, FilterOperator.Default, [true, false], (bool _) => ALL),

        FilterTestCase.Create(1200, FilterOperator.Contains, [false], new FilterExpressionCreationException("Filter operator 'Contains' not allowed for property type 'System.Boolean'")),

        FilterTestCase.Create(1300, FilterOperator.EqualCaseInsensitive, [true], (bool x) => x),
        FilterTestCase.Create(1301, FilterOperator.EqualCaseInsensitive, [false], (bool x) => !x),
        FilterTestCase.Create(1302, FilterOperator.EqualCaseInsensitive, [true, false], (bool _) => ALL),

        FilterTestCase.Create(1400, FilterOperator.EqualCaseSensitive, [true], (bool x) => x),
        FilterTestCase.Create(1401, FilterOperator.EqualCaseSensitive, [false], (bool x) => !x),
        FilterTestCase.Create(1402, FilterOperator.EqualCaseSensitive, [true, false], (bool _) => ALL),

        FilterTestCase.Create(1502, FilterOperator.NotEqual, [true], (bool x) => !x),
        FilterTestCase.Create(1503, FilterOperator.NotEqual, [false], (bool x) => x),
        FilterTestCase.Create(1504, FilterOperator.NotEqual, [true, false], (bool _) => ALL),

        FilterTestCase.Create(1600, FilterOperator.LessThan, [false], new FilterExpressionCreationException("Filter operator 'LessThan' not allowed for property type 'System.Boolean'")),

        FilterTestCase.Create(1700, FilterOperator.LessThanOrEqual, [false], new FilterExpressionCreationException("Filter operator 'LessThanOrEqual' not allowed for property type 'System.Boolean'")),

        FilterTestCase.Create(1800, FilterOperator.GreaterThan, [false], new FilterExpressionCreationException("Filter operator 'GreaterThan' not allowed for property type 'System.Boolean'")),

        FilterTestCase.Create(1900, FilterOperator.GreaterThanOrEqual, [false], new FilterExpressionCreationException("Filter operator 'GreaterThanOrEqual' not allowed for property type 'System.Boolean'")),

        FilterTestCase.Create(2000, FilterOperator.IsNull, new bool[] { default }, new FilterExpressionCreationException("Filter operator 'IsNull' not allowed for property type 'System.Boolean'")),

        FilterTestCase.Create(2100, FilterOperator.NotNull, new bool[] { default }, new FilterExpressionCreationException("Filter operator 'NotNull' not allowed for property type 'System.Boolean'"))
    ];
    // ReSharper restore RedundantExplicitArrayCreation
}