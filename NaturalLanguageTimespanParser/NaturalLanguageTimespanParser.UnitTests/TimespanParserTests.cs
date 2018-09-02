using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaturalLanguageTimespanParser.UnitTests
{
    [TestClass]
    public class TimespanParserTests
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void When_NullIsPassedAsAnArgument_Expect_Exception()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            sut.Parse(null);

            // Assert
            Assert.Fail("This call line should not have been reached");
        }

        [TestMethod]
        public void When_MinutesAreDefined_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var results20Min = ParseMany(sut, new List<string>
            {
                "Review english lesson @home 20m",
                "Review english lesson @home 20M",
                "Review english lesson @home 20 m",
                "Review english lesson @home 20 M"
            });
            var results45Min = ParseMany(sut, new List<string>
            {
                "@home Read for 45 minutes",
                "@home Read for 45 MINUTES",
                "@home Read for 45 MiNuTeS"
            });

            // Assert
            AssertAllDurationsEqual(TimeSpan.FromMinutes(20), results20Min);
            AssertAllDurationsEqual(TimeSpan.FromMinutes(45), results45Min);
        }

        [TestMethod]
        public void When_TimeIsNotDefined_Expect_ParseDoesNotSucceed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult = sut.Parse("@home Read a book");

            // Assert
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(TimeSpan.Zero, parseResult.Duration);
        }

        [TestMethod]
        public void When_NumberIsProvidedWithoutUnit_Expect_ParseDoesNotSucceed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult = sut.Parse("Buy 3 bottles of milk @market");

            // Assert
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(TimeSpan.Zero, parseResult.Duration);
        }

        [TestMethod]
        public void When_MultipleNumbersAreProvidedWithoutUnit_Expect_ParseDoesNotSucceed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult = sut.Parse("123 45 6");

            // Assert
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(TimeSpan.Zero, parseResult.Duration);
        }

        [TestMethod]
        public void When_StringWitheNumbersIsProvidedWithoutUnit_Expect_ParseDoesNotSucceed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult = sut.Parse("Philippians 4:11 - 4:12");

            // Assert
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(TimeSpan.Zero, parseResult.Duration);
        }

        [TestMethod]
        public void When_HoursAreDefined_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var results1H = ParseMany(sut, new List<string>
            {
                "Exercise @gym 1h",
                "Exercise @gym 1H",
                "Exercise @gym 1 h",
                "Exercise @gym 1 H"
            });

            // Assert
            AssertAllDurationsEqual(TimeSpan.FromHours(1), results1H);
        }


        [TestMethod]
        public void When_HourFractionIsDefinedInAbbreviatedForm_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var results30Min = ParseMany(sut, new List<string>
            {
                "Exercise @gym 0.5h",
                "Exercise @gym 0.5H",
                "Exercise @gym 0.5 H",
                "Exercise @gym 0.5 h"
            });

            // Assert
            AssertAllDurationsEqual(TimeSpan.FromMinutes(30), results30Min);
        }

        [TestMethod]
        public void When_BothHoursAndMinutesAreDefinedInAbbreviatedForm_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var results150Min = ParseMany(sut, new List<string>
            {
                "Play drums (2h 30 m) @home",
                "Play drums (2 H 30 M) @home",
                "Play drums (2h 30 Min) @home",
                "Play drums (2H 30 min) @home"
            });

            // Assert
            AssertAllDurationsEqual(TimeSpan.FromMinutes(150), results150Min);
        }

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

        [TestMethod]
        public void When_TimeInMinutesIsDefinedInBrackets_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var results5Min = ParseMany(sut, new List<string>
            {
                "Some string [5m] @home",
                "Some string (5m) @gym",
                "Some string {5m} @market",
                "Some string (5m) (different thing in a bracket)",
                "Some string (5 M) test",
                "Some string (5 m) test",
                "Some string (5 min) test",
                "Some string (5 MIN) test",
                "Some string {5 M} test",
                "Some string [5 m} test",
                "Some string {5 min] test",
                "Some string [5 MIN) test"
            });

            // Assert
            AssertAllDurationsEqual(TimeSpan.FromMinutes(5), results5Min);
        }

        [TestMethod]
        public void When_TimeInMinutesIsDefinedInBracketsAtTheEndOfAString_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var results5Min = ParseMany(sut, new List<string>
            {
                "Some string [5m]",
                "Some string (5m)",
                "Some string {5m}",
                "Some string (5m)",
                "Some string (5 M)",
                "Some string (5 m)",
                "Some string (5 min)",
                "Some string (5 MIN)",
                "Some string {5 M}",
                "Some string [5 m}",
                "Some string {5 min]",
                "Some string [5 MIN)"
            });

            // Assert
            AssertAllDurationsEqual(TimeSpan.FromMinutes(5), results5Min);
        }


        private static ITimespanParser CreateSystemUnderTest()
        {
            return new TimespanParser();
        }

        private static TimespanParseResult[] ParseMany(ITimespanParser sut, IEnumerable<string> inputs)
        {
            return inputs.Select(sut.Parse).ToArray();
        }

        private static void AssertAllDurationsEqual(TimeSpan expected, IEnumerable<TimespanParseResult> actual)
        {
            foreach (var result in actual) Assert.AreEqual(expected, result.Duration);
        }
    }
}