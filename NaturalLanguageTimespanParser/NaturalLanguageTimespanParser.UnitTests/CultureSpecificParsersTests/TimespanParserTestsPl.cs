using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaturalLanguageTimespanParser.UnitTests.CultureSpecificParsersTests
{
    [TestClass]
    public class TimespanParserTestsPl : TimespanParserTestsBase
    {
        [TestMethod]
        public void When_MinutesAreDefinedInFullFormInPolish_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var results5Min = ParseMany(sut, new List<string>
            {
                "Czas po polsku (5 minut) @home",
                "Czas po polsku (5 MINUT) @home",
                "Czas po polsku (5 MiNuT) @home",
                "Czas po polsku (5minut) @home"
            });

            // Assert
            AssertAllDurationsEqual(TimeSpan.FromMinutes(5), results5Min);
        }


        internal override ITimespanParser CreateSystemUnderTest()
        {
            var englishCulture = new CultureInfo("pl");
            return new TimespanParser(englishCulture);
        }
    }
}