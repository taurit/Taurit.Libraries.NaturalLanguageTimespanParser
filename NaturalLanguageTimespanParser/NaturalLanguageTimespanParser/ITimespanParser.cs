namespace NaturalLanguageTimespanParser;

public interface ITimespanParser
{
    TimespanParseResult Parse(string naturalLanguageTimeSpan);
}