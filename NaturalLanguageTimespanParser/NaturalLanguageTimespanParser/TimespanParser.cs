using System.Globalization;
using NaturalLanguageTimespanParser.Parsers;

namespace NaturalLanguageTimespanParser;

public class TimespanParser : ITimespanParser
{
    private readonly ICultureSpecificParser _parser;

    public TimespanParser(CultureInfo culture)
    {
        if (culture == null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        _parser = ParserFactory.GetParserForCulture(culture);
    }

    public TimespanParseResult Parse(string naturalLanguageTimeSpan)
    {
        return _parser.Parse(naturalLanguageTimeSpan);
    }
}
