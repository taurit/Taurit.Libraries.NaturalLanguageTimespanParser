using System;
using System.Runtime.CompilerServices;
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
        public void When_MinutesAreDefinedInAbbreviatedForm_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult1 = sut.Parse("Review english lesson @home 20m");
            var parseResult2 = sut.Parse("Review english lesson @home 20M");
            var parseResult3 = sut.Parse("Review english lesson @home 20 m");
            var parseResult4 = sut.Parse("Review english lesson @home 20 M");

            // Assert
            Assert.IsTrue(parseResult1.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(20), parseResult1.Duration);

            Assert.IsTrue(parseResult3.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(20), parseResult2.Duration);

            Assert.IsTrue(parseResult3.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(20), parseResult3.Duration);

            Assert.IsTrue(parseResult4.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(20), parseResult4.Duration);
        }

        [TestMethod]
        public void When_MinutesAreDefinedInFullForm_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult1 = sut.Parse("@home Read for 45 minutes");
            var parseResult2 = sut.Parse("@home Read for 45 MINUTES");
            var parseResult3 = sut.Parse("@home Read for 45 MiNuTeS");

            // Assert
            Assert.IsTrue(parseResult1.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(45), parseResult1.Duration);

            Assert.IsTrue(parseResult2.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(45), parseResult2.Duration);

            Assert.IsTrue(parseResult3.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(45), parseResult3.Duration);
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
        public void When_HoursAreDefinedInAbbreviatedForm_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult1 = sut.Parse("Exercise @gym 1h");
            var parseResult2 = sut.Parse("Exercise @gym 1H");
            var parseResult3 = sut.Parse("Exercise @gym 1 H");
            var parseResult4 = sut.Parse("Exercise @gym 1 h");

            // Assert
            Assert.IsTrue(parseResult1.Success);
            Assert.AreEqual(TimeSpan.FromHours(1), parseResult1.Duration);

            Assert.IsTrue(parseResult2.Success);
            Assert.AreEqual(TimeSpan.FromHours(1), parseResult2.Duration);

            Assert.IsTrue(parseResult3.Success);
            Assert.AreEqual(TimeSpan.FromHours(1), parseResult3.Duration);

            Assert.IsTrue(parseResult4.Success);
            Assert.AreEqual(TimeSpan.FromHours(1), parseResult4.Duration);
        }

        [TestMethod]
        public void When_HourFractionIsDefinedInAbbreviatedForm_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult1 = sut.Parse("Exercise @gym 0.5h");
            var parseResult2 = sut.Parse("Exercise @gym 0.5H");
            var parseResult3 = sut.Parse("Exercise @gym 0.5 H");
            var parseResult4 = sut.Parse("Exercise @gym 0.5 h");

            // Assert
            Assert.IsTrue(parseResult1.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(30), parseResult1.Duration);

            Assert.IsTrue(parseResult2.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(30), parseResult2.Duration);

            Assert.IsTrue(parseResult3.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(30), parseResult3.Duration);

            Assert.IsTrue(parseResult4.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(30), parseResult4.Duration);
        }

        [TestMethod]
        public void When_HourFractionIsDefinedInAbbreviatedFormWithSemicolons_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult1 = sut.Parse("Exercise @gym 0.5h");
            var parseResult2 = sut.Parse("Exercise @gym 0.5H");
            var parseResult3 = sut.Parse("Exercise @gym 0.5 H");
            var parseResult4 = sut.Parse("Exercise @gym 0.5 h");

            // Assert
            Assert.IsTrue(parseResult1.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(30), parseResult1.Duration);

            Assert.IsTrue(parseResult2.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(30), parseResult2.Duration);

            Assert.IsTrue(parseResult3.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(30), parseResult3.Duration);

            Assert.IsTrue(parseResult4.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(30), parseResult4.Duration);
        }

        [TestMethod]
        public void When_BothHoursAndMinutesAreDefinedInAbbreviatedForm_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult1 = sut.Parse("Play drums (2h 30 m) @home");
            var parseResult2 = sut.Parse("Play drums (2 H 30 M) @home");
            var parseResult3 = sut.Parse("Play drums (2h 30 Min) @home");
            var parseResult4 = sut.Parse("Play drums (2H 30 min) @home");

            // Assert
            Assert.IsTrue(parseResult1.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(150), parseResult1.Duration);

            Assert.IsTrue(parseResult2.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(150), parseResult2.Duration);

            Assert.IsTrue(parseResult3.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(150), parseResult3.Duration);

            Assert.IsTrue(parseResult4.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(150), parseResult4.Duration);
        }

        [TestMethod]
        public void When_MinutesAreDefinedInFullFormInPolish_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult1 = sut.Parse("Czas po polsku (5 minut) @home");
            var parseResult2 = sut.Parse("Czas po polsku (5 MINUT) @home");
            var parseResult3 = sut.Parse("Czas po polsku (5 MiNuT) @home");
            var parseResult4 = sut.Parse("Czas po polsku (5minut) @home");

            // Assert
            Assert.IsTrue(parseResult1.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(5), parseResult1.Duration);

            Assert.IsTrue(parseResult2.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(5), parseResult2.Duration);

            Assert.IsTrue(parseResult3.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(5), parseResult3.Duration);

            Assert.IsTrue(parseResult4.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(5), parseResult4.Duration);
        }

        [TestMethod]
        public void When_TimeInMinutesIsDefinedInBrackets_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult1 = sut.Parse("Some string [5m] @home");
            var parseResult2 = sut.Parse("Some string (5m) @gym");
            var parseResult3 = sut.Parse("Some string {5m} @market");
            var parseResult4 = sut.Parse("Some string (5m) (different thing in a bracket)");

            // Assert
            Assert.IsTrue(parseResult1.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(5), parseResult1.Duration);

            Assert.IsTrue(parseResult2.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(5), parseResult2.Duration);

            Assert.IsTrue(parseResult3.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(5), parseResult3.Duration);

            Assert.IsTrue(parseResult4.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(5), parseResult4.Duration);
        }

        [TestMethod]
        public void When_TimeInMinutesIsDefinedInBracketsAtTheEndOfAString_Expect_DurationIsCorrectlyParsed()
        {
            // Arrange
            var sut = CreateSystemUnderTest();

            // Act
            var parseResult1 = sut.Parse("Some string [5m]");
            var parseResult2 = sut.Parse("Some string (5m)");
            var parseResult3 = sut.Parse("Some string {5m}");
            var parseResult4 = sut.Parse("Some string (different thing in a bracket) (5m)");

            // Assert
            Assert.IsTrue(parseResult1.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(5), parseResult1.Duration);

            Assert.IsTrue(parseResult2.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(5), parseResult2.Duration);

            Assert.IsTrue(parseResult3.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(5), parseResult3.Duration);

            Assert.IsTrue(parseResult4.Success);
            Assert.AreEqual(TimeSpan.FromMinutes(5), parseResult4.Duration);
        }


        private ITimespanParser CreateSystemUnderTest()
        {
            return new TimespanParser();
        }
    }
}