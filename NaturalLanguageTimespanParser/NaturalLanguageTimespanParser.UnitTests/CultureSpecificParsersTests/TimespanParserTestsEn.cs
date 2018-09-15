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
        
        [Ignore]
        [TestMethod]
        public override void When_TimeInMinutesIsDefinedInWords_Expect_DurationCorrectlyParsed()
        {
            // todo: something like https://www.programmingalgorithms.com/algorithm/words-to-numbers can be used

            // Arrange
            var sut = CreateSystemUnderTest();
            List<string> sentences = new List<string>()
            {
                "@home Read for zero minutes",
                "@home Read for one minute",
                "@home Read for two minutes",
                "@home Read for three minutes",
                "@home Read for four minutes",
                "@home Read for five minutes",
                "@home Read for six minutes",
                "@home Read for seven minutes",
                "@home Read for eight minutes",
                "@home Read for nine minutes",
            };

            // Act
            var results = sentences.Select(sut.Parse).ToList();
            
            // Assert
            for (int i = 0; i < sentences.Count; i++)
            {       
                Assert.Equals(TimeSpan.FromMinutes(i), results[i]);
            }
        }

        internal override ITimespanParser CreateSystemUnderTest()
        {
            var englishCulture = new CultureInfo("en");
            return new TimespanParser(englishCulture);
        }
    }
}