using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaturalLanguageTimespanParser.UnitTests
{
    public class TimespanParserTestsBase
    {
        protected static TimespanParseResult[] ParseMany(ITimespanParser sut, IEnumerable<string> inputs)
        {
            return inputs.Select(sut.Parse).ToArray();
        }

        protected static void AssertAllDurationsEqual(TimeSpan expected, IEnumerable<TimespanParseResult> actual)
        {
            foreach (var result in actual) Assert.AreEqual(expected, result.Duration);
        }
    }
}