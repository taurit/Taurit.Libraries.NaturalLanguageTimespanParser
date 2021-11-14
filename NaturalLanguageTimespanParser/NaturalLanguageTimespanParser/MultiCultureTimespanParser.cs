using System.Globalization;
using NaturalLanguageTimespanParser.Parsers;

namespace NaturalLanguageTimespanParser;

public class MultiCultureTimespanParser : ITimespanParser
{
    private readonly IReadOnlyList<ICultureSpecificParser> _parsers;

    public MultiCultureTimespanParser(IEnumerable<CultureInfo> cultures)
    {
        if (cultures == null) throw new ArgumentNullException(nameof(cultures));
        var culturesAsList = cultures.ToList();

        if (!culturesAsList.Any())
            throw new ArgumentException("At least one culture must be specified");

        _parsers = culturesAsList.Select(ParserFactory.GetParserForCulture).ToList();
    }

    /// <inheritdoc />
    public TimespanParseResult Parse(string naturalLanguageTimeSpan)
    {
        foreach (var parser in _parsers)
        {
            var parseResult = parser.Parse(naturalLanguageTimeSpan);
            if (parseResult.Success) return parseResult;
        }

        return TimespanParseResult.CreateFailure();
    }
}