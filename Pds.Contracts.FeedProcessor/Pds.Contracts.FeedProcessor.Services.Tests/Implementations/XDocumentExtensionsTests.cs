using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pds.Contracts.FeedProcessor.Services.Implementations;
using System;
using System.Xml.Linq;

namespace Pds.Contracts.FeedProcessor.Services.Tests.Implementations
{
    [TestClass, TestCategory("Unit")]
    public class XDocumentExtensionsTests
    {
        [TestMethod, TestCategory("Unit")]
        public void LowerCaseAllElementNames_XDocument_NullArgument_ThrowsException()
        {
            // Arrange
            XDocument document = null;

            // Act
            Action act = () => document.LowerCaseAllElementNames();

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod, TestCategory("Unit")]
        public void LowerCaseAllElementNames_XDocument_ConvertsCurrentElement()
        {
            // Arrange
            string expectedString = @"<root>
  <childofroot>Some Mixed Case Value</childofroot>
</root>";
            XDocument document = XDocument.Parse("<Root><childOfRoot>Some Mixed Case Value</childOfRoot></Root>");

            // Act
            document.LowerCaseAllElementNames();

            var result = document.ToString();

            // Assert
            string phrase = "Some Mixed Case Value";
            result.Should().Contain(phrase);
            CompareStringsBySubstringExcludingElementTextAndWhitespace(expectedString, result, phrase, '>');
        }

        [TestMethod, TestCategory("Unit")]
        public void LowerCaseAllElementNames_XElement_NullArgument_ThrowsException()
        {
            // Arrange
            XElement element = null;

            // Act
            Action act = () => element.LowerCaseAllElementNames();

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod, TestCategory("Unit")]
        public void LowerCaseAllElementNames_XElement_ConvertsCurrentElement()
        {
            // Arrange
            string expectedString = @"<root>
  <childofroot>Some Mixed Case Value</childofroot>
</root>";
            XDocument document = XDocument.Parse("<Root><childOfRoot>Some Mixed Case Value</childOfRoot></Root>");

            // Act
            document.Element("Root").LowerCaseAllElementNames();

            // Assert
            string phrase = "Some Mixed Case Value";
            document.Element("root").ToString().Should().Contain(phrase);
            CompareStringsBySubstringExcludingElementTextAndWhitespace(expectedString, document.Element("root").ToString(), phrase, '>');
        }

        private void CompareStringsBySubstringExcludingElementTextAndWhitespace(string expectedString, string resultString, string excludedString, char separator)
        {
            string trimmedExpected = expectedString.Remove(expectedString.IndexOf(excludedString), excludedString.Length);
            string trimmedResult = resultString.Remove(resultString.IndexOf(excludedString), excludedString.Length);
            string[] expectedSubstrings = trimmedExpected.Split(separator);
            string[] resultSubstrings = trimmedResult.Split(separator);

            for (int i = 0; i < expectedSubstrings.Length; i++)
            {
                resultSubstrings[i].Trim().Should().Be(expectedSubstrings[i].Trim());
            }
        }
    }
}
