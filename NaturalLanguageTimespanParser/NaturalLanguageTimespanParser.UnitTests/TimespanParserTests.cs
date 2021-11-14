using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaturalLanguageTimespanParser.UnitTests;

[TestClass]
public class TimespanParserTests
{
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void When_NullIsPassedAsACulture_Expect_Exception()
    {
        // Arrange
        var sut = new TimespanParser(null!);

        // Act

        // Assert
        Assert.Fail("This call line should not have been reached");
    }
}