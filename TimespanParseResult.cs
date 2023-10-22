namespace NaturalLanguageTimespanParser;

public record TimespanParseResult(bool Success, TimeSpan Duration)
{
    internal static TimespanParseResult CreateSuccess(TimeSpan duration)
    {
        return new TimespanParseResult(true, duration);
    }

    internal static TimespanParseResult CreateFailure()
    {
        return new TimespanParseResult(false, TimeSpan.Zero);
    }
}
