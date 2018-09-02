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
            sut.Parse(null);

            // Assert
            Assert.Fail("This call line should not have been reached");
        }

        private TimespanParser CreateSystemUnderTest()
        {
            return new TimespanParser();
        }
    }
}