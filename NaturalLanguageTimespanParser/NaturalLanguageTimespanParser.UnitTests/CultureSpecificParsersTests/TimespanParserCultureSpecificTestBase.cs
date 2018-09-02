using System;
using System.Collections.Generic;
using System.Text;

namespace NaturalLanguageTimespanParser.UnitTests.CultureSpecificParsersTests
{
    public abstract class TimespanParserCultureSpecificTestBase : TimespanParserTestsBase
    {
        public abstract void When_TimeInMinutesIsDefinedAsANumber_Expect_DurationCorrectlyParsed();

        public abstract void When_TimeInMinutesIsDefinedInWords_Expect_DurationCorrectlyParsed();
    }
}
