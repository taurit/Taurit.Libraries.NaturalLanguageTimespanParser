using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaturalLanguageTimespanParser.UnitTests.CultureSpecificParsersTests
{
    [TestClass]
    public class TimespanParserTestsPl : TimespanParserCultureSpecificTestBase
    {
        internal override ITimespanParser CreateSystemUnderTest()
        {
            var englishCulture = new CultureInfo("pl");
            return new TimespanParser(englishCulture);
        }

        /// <inheritdoc />
        public override void When_TimeInMinutesIsDefinedAsANumber_Expect_DurationCorrectlyParsed()
        {
            Assert.Inconclusive("TODO");
        }

        /// <inheritdoc />
        public override void When_TimeInMinutesIsDefinedInWords_Expect_DurationCorrectlyParsed()
        {
            Assert.Inconclusive("TODO");
        }
    }
}