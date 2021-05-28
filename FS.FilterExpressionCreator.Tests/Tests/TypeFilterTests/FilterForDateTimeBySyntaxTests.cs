﻿using FS.FilterExpressionCreator.Exceptions;
using FS.FilterExpressionCreator.Models;
using FS.FilterExpressionCreator.Tests.Attributes;
using FS.FilterExpressionCreator.Tests.Extensions;
using FS.FilterExpressionCreator.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FS.FilterExpressionCreator.Tests.Tests.TypeFilterTests
{
    [TestClass]
    public class FilterForDateTimeBySyntaxTests : TestBase<DateTime>
    {
        [DataTestMethod]
        [FilterTestDataSource(nameof(_testCases), nameof(TestModelFilterFunctions))]
        public void FilterForDateTimeBySyntax_WorksAsExpected(FilterTestCase<DateTime, DateTime> testCase, TestModelFilterFunc<DateTime> filterFunc)
            => testCase.Run(_testItems, filterFunc);

        private static readonly TestModel<DateTime>[] _testItems = {
            new() { ValueA = new DateTime(1900, 01, 01) },
            new() { ValueA = new DateTime(2000, 01, 01) },
            new() { ValueA = new DateTime(2010, 01, 01) },
            new() { ValueA = new DateTime(2010, 06, 01) },
            new() { ValueA = new DateTime(2010, 06, 15) },
            new() { ValueA = new DateTime(2010, 06, 15, 12, 0, 0) },
            new() { ValueA = new DateTime(2010, 06, 15, 12, 30, 0) },
            new() { ValueA = new DateTime(2010, 06, 15, 12, 30, 30) },
            new() { ValueA = new DateTime(2010, 06, 15, 12, 30, 31) },
            new() { ValueA = new DateTime(2010, 06, 15, 12, 31, 00) },
            new() { ValueA = new DateTime(2010, 06, 15, 13, 00, 00) },
            new() { ValueA = new DateTime(2010, 06, 16) },
            new() { ValueA = new DateTime(2010, 07, 01) },
            new() { ValueA = new DateTime(2011, 01, 01) },
            new() { ValueA = new DateTime(2020, 01, 01) },
        };

        private static readonly FilterTestCase<DateTime, DateTime>[] _testCases = {
            FilterTestCase.Create<DateTime>(1000, "null", _ => ALL, IgnoreParseExceptions),
            FilterTestCase.Create<DateTime>(1001, "=null", _ => ALL, IgnoreParseExceptions),
            FilterTestCase.Create<DateTime>(1002, "~2010", x => x >= new DateTime(2010, 01, 01) && x < new DateTime(2011, 01, 01)),
            FilterTestCase.Create<DateTime>(1003, "~2010-06", x => x >= new DateTime(2010, 06, 01) && x < new DateTime(2010, 07, 01)),
            FilterTestCase.Create<DateTime>(1004, "~2010-06-15", x => x >= new DateTime(2010, 06, 15) && x < new DateTime(2010, 06, 16)),
            FilterTestCase.Create<DateTime>(1005, "~2010-06-15T12", x => x >= new DateTime(2010, 06, 15, 12, 00, 00) && x < new DateTime(2010, 06, 15, 13, 00, 00)),
            FilterTestCase.Create<DateTime>(1006, "~2010-06-15T12:30", x => x >= new DateTime(2010, 06, 15, 12, 30, 00) && x < new DateTime(2010, 06, 15, 12, 31, 00)),
            FilterTestCase.Create<DateTime>(1007, "~2010 06 15 12 30", x => x >= new DateTime(2010, 06, 15, 12, 30, 00) && x < new DateTime(2010, 06, 15, 12, 31, 00)),
            FilterTestCase.Create<DateTime>(1008, "~2010/06/15/12/30", x => x >= new DateTime(2010, 06, 15, 12, 30, 00) && x < new DateTime(2010, 06, 15, 12, 31, 00)),
            FilterTestCase.Create<DateTime>(1009, "~01.06.2010", x => x >= new DateTime(2010, 01, 06) && x < new DateTime(2010, 01, 07), CultureEnUs),
            FilterTestCase.Create<DateTime>(1010, "~01.06.2010", x => x >= new DateTime(2010, 06, 01) && x < new DateTime(2010, 07, 01), CultureDeDe),
            FilterTestCase.Create<DateTime>(1011, "one-month-ago", x => x >= new DateTime(2010, 05, 16) && x < new DateTime(2010, 06, 16), new FilterConfiguration{ Now = () => new DateTime(2010, 06, 16)}),
            FilterTestCase.Create<DateTime>(1012, "june 1st_june-16th", x => x >= new DateTime(2010, 06, 01) && x < new DateTime(2010, 06, 16), new FilterConfiguration{ Now = () => new DateTime(2010, 06, 16)}),
            FilterTestCase.Create<DateTime>(1013, "2010-13-40", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1013, "2010-13-40_2020-01-01", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1014, "INVALID_june-16th", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1015, "june 1st_INVALID", new FilterExpressionCreationException("Unable to parse given filter value")),

            FilterTestCase.Create<DateTime>(1100, "null", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1101, "", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1102, "2010-01-01", x => x >= new DateTime(2010, 01, 01) && x < new DateTime(2011, 01, 01)),
            FilterTestCase.Create<DateTime>(1103, "2010-06-01", x => x >= new DateTime(2010, 06, 01) && x < new DateTime(2010, 07, 01)),
            FilterTestCase.Create<DateTime>(1104, "2010-06-15", x => x >= new DateTime(2010, 06, 15) && x < new DateTime(2010, 06, 16)),
            FilterTestCase.Create<DateTime>(1105, "2010-06-15T12:00:00", x => x >= new DateTime(2010, 06, 15, 12, 00, 00) && x < new DateTime(2010, 06, 15, 13, 00, 00)),
            FilterTestCase.Create<DateTime>(1106, "2010-06-15T12:30:00", x => x >= new DateTime(2010, 06, 15, 12, 30, 00) && x < new DateTime(2010, 06, 15, 12, 31, 00)),
            FilterTestCase.Create<DateTime>(1107, "2010-06-15T12:30:30", x => x >= new DateTime(2010, 06, 15, 12, 30, 30) && x < new DateTime(2010, 06, 15, 12, 30, 31)),
            FilterTestCase.Create<DateTime>(1108, "2100-01-01", _ => NONE),
            FilterTestCase.Create<DateTime>(1109, "2010-06-01_2010-06-15T12:31:00", x => x >= new DateTime(2010, 06, 01) && x < new DateTime(2010, 06, 15, 12, 31, 00)),

            FilterTestCase.Create<DateTime>(1200, "~null", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1201, "~", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1202, "~2010-01-01", x => x >= new DateTime(2010, 01, 01) && x < new DateTime(2011, 01, 01)),
            FilterTestCase.Create<DateTime>(1203, "~2010-06-01", x => x >= new DateTime(2010, 06, 01) && x < new DateTime(2010, 07, 01)),
            FilterTestCase.Create<DateTime>(1204, "~2010-06-15", x => x >= new DateTime(2010, 06, 15) && x < new DateTime(2010, 06, 16)),
            FilterTestCase.Create<DateTime>(1205, "~2010-06-15T12:00:00", x => x >= new DateTime(2010, 06, 15, 12, 00, 00) && x < new DateTime(2010, 06, 15, 13, 00, 00)),
            FilterTestCase.Create<DateTime>(1206, "~2010-06-15T12:30:00", x => x >= new DateTime(2010, 06, 15, 12, 30, 00) && x < new DateTime(2010, 06, 15, 12, 31, 00)),
            FilterTestCase.Create<DateTime>(1207, "~2010-06-15T12:30:30", x => x >= new DateTime(2010, 06, 15, 12, 30, 30) && x < new DateTime(2010, 06, 15, 12, 30, 31)),
            FilterTestCase.Create<DateTime>(1208, "~2100-01-01", _ => NONE),
            FilterTestCase.Create<DateTime>(1209, "2010-06-01_2010-06-15T12:31:00", x => x >= new DateTime(2010, 06, 01) && x < new DateTime(2010, 06, 15, 12, 31, 00)),

            FilterTestCase.Create<DateTime>(1300, "=null", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1301, "=", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1302, "=2010-01-01", x => x == new DateTime(2010, 01, 01)),
            FilterTestCase.Create<DateTime>(1303, "=2010-06-15T12:30:30", x => x == new DateTime(2010, 06, 15, 12, 30, 30)),
            FilterTestCase.Create<DateTime>(1304, "=2010-01-01_2020-01-01", x => x == new DateTime(2010, 01, 01)),

            FilterTestCase.Create<DateTime>(1400, "==null", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1401, "==", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1402, "==2010-01-01", x => x == new DateTime(2010, 01, 01)),
            FilterTestCase.Create<DateTime>(1403, "==2010-06-15T12:30:30", x => x == new DateTime(2010, 06, 15, 12, 30, 30)),
            FilterTestCase.Create<DateTime>(1404, "==2010-01-01_2020-01-01", x => x == new DateTime(2010, 01, 01)),

            FilterTestCase.Create<DateTime>(1500, "!null", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1501, "!", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1502, "!2010-01-01", x => x != new DateTime(2010, 01, 01)),
            FilterTestCase.Create<DateTime>(1503, "!2010-06-15T12:30:30", x => x != new DateTime(2010, 06, 15, 12, 30, 30)),
            FilterTestCase.Create<DateTime>(1504, "!2010-01-01_2020-01-01", x => x != new DateTime(2010, 01, 01)),

            FilterTestCase.Create<DateTime>(1600, "<null", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1601, "<", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1602, "<2010-01-01", x => x < new DateTime(2010, 01, 01)),
            FilterTestCase.Create<DateTime>(1603, "<2010-01-01_2020-01-01", x => x < new DateTime(2010, 01, 01)),

            FilterTestCase.Create<DateTime>(1700, "<=null", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1701, "<=", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1702, "<=2010-01-01", x => x <= new DateTime(2010, 01, 01)),
            FilterTestCase.Create<DateTime>(1703, "<=2010-01-01_2020-01-01", x => x <= new DateTime(2010, 01, 01)),

            FilterTestCase.Create<DateTime>(1800, ">null", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1801, ">", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1802, ">2010-01-01", x => x > new DateTime(2010, 01, 01)),
            FilterTestCase.Create<DateTime>(1803, ">2010-01-01_2020-01-01", x => x > new DateTime(2010, 01, 01)),

            FilterTestCase.Create<DateTime>(1900, ">=null", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1901, ">=", new FilterExpressionCreationException("Unable to parse given filter value")),
            FilterTestCase.Create<DateTime>(1902, ">=2010-01-01", x => x >= new DateTime(2010, 01, 01)),
            FilterTestCase.Create<DateTime>(1903, ">=2010-01-01_2020-01-01", x => x >= new DateTime(2010, 01, 01)),

            FilterTestCase.Create<DateTime>(2000, "ISNULL", new FilterExpressionCreationException("Filter operator 'IsNull' not allowed for property type 'System.DateTime'")),

            FilterTestCase.Create<DateTime>(2100, "NOTNULL", new FilterExpressionCreationException("Filter operator 'NotNull' not allowed for property type 'System.DateTime'")),
        };
    }
}