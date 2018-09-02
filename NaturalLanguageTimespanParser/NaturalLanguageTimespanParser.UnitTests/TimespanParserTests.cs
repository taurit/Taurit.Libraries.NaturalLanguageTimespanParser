using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaturalLanguageTimespanParser.UnitTests
{
    [TestClass]
    public class TimespanParserTests : TimespanParserTestsBase
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void When_NullIsPassedAsACulture_Expect_Exception()
        {
            // Arrange
            // ReSharper disable once AssignNullToNotNullAttribute
            var sut = new TimespanParser(null);

            // Act

            // Assert
            Assert.Fail("This call line should not have been reached");
        }
    }
}