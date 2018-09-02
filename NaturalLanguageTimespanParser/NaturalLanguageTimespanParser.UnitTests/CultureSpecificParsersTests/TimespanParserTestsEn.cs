using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaturalLanguageTimespanParser.UnitTests.CultureSpecificParsersTests
{
    [TestClass]
    public class TimespanParserTestsEn : TimespanParserCultureSpecificTestBase
    {
        [TestMethod]
        public override void When_TimeInMinutesIsDefinedAsANumber_Expect_DurationCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var results45Min = ParseMany(sut, new List<string>
            {
                "@home Read for 45 minutes",
                "@home Read for 45 MINUTES",
                "@home Read for 45 MiNuTeS"
            });

            // Assert
            AssertAllDurationsEqual(TimeSpan.FromMinutes(45), results45Min);
        }

        internal override ITimespanParser CreateSystemUnderTest()
        {
            var englishCulture = new CultureInfo("en");
            return new TimespanParser(englishCulture);
        }
    }
}