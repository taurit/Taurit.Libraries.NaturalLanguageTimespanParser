using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaturalLanguageTimespanParser.UnitTests;

[TestClass]
public class TimespanParserTests
{
    [DataTestMethod]
    [DataRow("Review english lesson @home 20m", 20)]
    [DataRow("Review english lesson @home 20M", 20)]
    [DataRow("Review english lesson @home 20 m", 20)]
    [DataRow("Review english lesson @home 20 M", 20)]
    public void When_MinutesAreDefinedAsAbbreviation_Expect_DurationIsCorrectlyParsed(string input,
        int expectedOutputMinutes)
    {
        // Arrange
        var sut = new TimespanParser();

        // Act
        var result = sut.Parse(input);

        // Assert
        result.Duration.Should().Be(TimeSpan.FromMinutes(expectedOutputMinutes));
    }


    [DataTestMethod]
    [DataRow("Some string [5m]")]
    [DataRow("Some string (5m)")]
    [DataRow("Some string {5m}")]
    [DataRow("Some string (5 M)")]
    [DataRow("Some string (5 m)")]
    [DataRow("Some string (5 min)")]
    [DataRow("Some string (5 MIN)")]
    [DataRow("Some string {5 M}")]
    [DataRow("Some string [5 m]")]
    [DataRow("Some string {5 min]")]
    [DataRow("Some string [5 MIN]")]
    [DataRow("lalala ccccccc. M xxxxxxx (5 min)")]
    [DataRow("lalala ccccccc. h xxxxxxx (5 min)")]
    public void When_TimeInMinutesIsDefinedInBracketsAtTheEndOfAString_Expect_DurationIsCorrectlyParsed(string input)
    {
        // Arrange
        var sut = new TimespanParser();

        // Act
        var result = sut.Parse(input);

        // Assert
        result.Duration.Should().Be(TimeSpan.FromMinutes(5));
    }


    [DataTestMethod]
    [DataRow("@home Read a book")]
    [DataRow("lalala ccccccc. M xxxxxxx")]
    public void When_TimeIsNotDefined_Expect_ParseDoesNotSucceed(string content)
    {
        // Arrange
        var sut = new TimespanParser();

        // Act
        var parseResult = sut.Parse(content);

        // Assert
        Assert.IsFalse(parseResult.Success);
        Assert.AreEqual(TimeSpan.Zero, parseResult.Duration);
    }

    [TestMethod]
    public void When_NumberIsProvidedWithoutUnit_Expect_ParseDoesNotSucceed()
    {
        // Arrange
        var sut = new TimespanParser();

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
        var sut = new TimespanParser();

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
        var sut = new TimespanParser();

        // Act
        var parseResult = sut.Parse("Philippians 4:11 - 4:12");

        // Assert
        Assert.IsFalse(parseResult.Success);
        Assert.AreEqual(TimeSpan.Zero, parseResult.Duration);
    }

    [DataTestMethod]
    [DataRow("Exercise @gym 1h")]
    [DataRow("Exercise @gym 1H")]
    [DataRow("Exercise @gym 1 h")]
    [DataRow("Exercise @gym 1 H")]
    public void When_HoursAreDefinedAsAbbreviations_Expect_DurationIsCorrectlyParsed(string input)
    {
        // Arrange
        var sut = new TimespanParser();

        // Act
        var result = sut.Parse(input);

        // Assert
        result.Duration.Should().Be(TimeSpan.FromHours(1));
    }

    [DataTestMethod]
    [DataRow("Exercise @gym 0.5h")]
    [DataRow("Exercise @gym 0.5H")]
    [DataRow("Exercise @gym 0.5 H")]
    [DataRow("Exercise @gym 0.5 h")]
    public void When_HourFractionsAreDefinedInAbbreviatedForm_Expect_DurationIsCorrectlyParsed(string input)
    {
        // Arrange
        var sut = new TimespanParser();

        // Act
        var result = sut.Parse(input);

        // Assert
        result.Duration.Should().Be(TimeSpan.FromMinutes(30));
    }

    [DataTestMethod]
    [DataRow("Exercise @gym 3,5h", 210)]
    [DataRow("Exercise @gym 3,5H", 210)]
    [DataRow("Exercise @gym 3,5 H", 210)]
    [DataRow("Exercise @gym 3,5 h", 210)]
    public void When_HourFractionsAreDefinedInAbbreviatedFormWithComma_Expect_DurationIsCorrectlyParsed(string input,
        int expectedOutputMinutes)
    {
        // Arrange
        var sut = new TimespanParser();

        // Act
        var result = sut.Parse(input);

        // Assert
        result.Should().Be(expectedOutputMinutes);
    }

    [DataTestMethod]
    [DataRow("Play drums (2h 30 m) @home")]
    [DataRow("Play drums (2 H 30 M) @home")]
    [DataRow("Play drums (2h 30 Min) @home")]
    [DataRow("Play drums (2H 30 min) @home")]
    public void When_BothHoursAndMinutesAreDefinedInAbbreviatedForm_Expect_DurationIsCorrectlyParsed(string input)
    {
        // Arrange
        var sut = new TimespanParser();

        // Act
        var result = sut.Parse(input);

        // Assert
        result.Duration.Should().Be(TimeSpan.FromMinutes(150));
    }

    [DataTestMethod]
    [DataRow("Some string [5m] @home")]
    [DataRow("Some string (5m) @gym")]
    [DataRow("Some string {5m} @market")]
    [DataRow("Some string (5m) (different thing in a bracket)")]
    [DataRow("Some string (5 M) test")]
    [DataRow("Some string (5 m) test")]
    [DataRow("Some string (5 min) test")]
    [DataRow("Some string (5 MIN) test")]
    [DataRow("Some string {5 M} test")]
    [DataRow("Some string [5 m} test")]
    [DataRow("Some string {5 min] test")]
    [DataRow("Some string [5 MIN) test")]
    public void When_TimeInMinutesIsDefinedInBrackets_Expect_DurationIsCorrectlyParsed(string input)
    {
        // Arrange
        var sut = new TimespanParser();

        // Act
        var result = sut.Parse(input);

        // Assert
        result.Duration.Should().Be(TimeSpan.FromMinutes(5));
    }

    [TestMethod]
    public void When_TwoConflictingTimeStampsAreFound_Expect_ResultIsFailure()
    {
        // Arrange
        var sut = new TimespanParser();

        // Act
        var parseResult = sut.Parse("Plan actions to prepare for upcoming exam in 16 hours total (30 minutes)");

        // Assert
        Assert.IsFalse(parseResult.Success,
            "Two conflicting time stamps were provided, so the result should not be possible to obtain");
    }


    [DataTestMethod]
    [DataRow("@home Read for 45 minutes")]
    [DataRow("@home Read for 45 MINUTES")]
    [DataRow("@home Read for 45 MiNuTeS")]
    public void When_TimeInMinutesIsDefinedAsANumber_Expect_DurationCorrectlyParsed(string input)
    {
        // Arrange
        var sut = new TimespanParser();

        // Act
        var result = sut.Parse(input);

        // Assert
        result.Duration.Should().Be(TimeSpan.FromMinutes(45));
    }
}
