using System;
using System.Globalization;

namespace NaturalLanguageTimespanParser.Parsers
{
    internal static class ParserFactory
    {
        internal static ICultureSpecificParser GetParserForCulture(CultureInfo culture)
        {
            if (culture.TwoLetterISOLanguageName == "en")
                return new TimespanParserEn();
            if (culture.TwoLetterISOLanguageName == "pl")
                return new TimespanParserPl();

            throw new ArgumentException(
                $"Culture with an ISO code {culture.TwoLetterISOLanguageName} was not recognized.");
        }
    }
}