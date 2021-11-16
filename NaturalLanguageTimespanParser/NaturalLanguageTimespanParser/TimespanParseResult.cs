namespace NaturalLanguageTimespanParser;

public struct TimespanParseResult
{
    private TimespanParseResult(bool success, TimeSpan duration)
    {
        Success = success;
        Duration = duration;
    }

    public bool Success { get; }
    public TimeSpan Duration { get; }

    internal static TimespanParseResult CreateSuccess(TimeSpan duration)
    {
        return new TimespanParseResult(true, duration);
    }

    internal static TimespanParseResult CreateFailure()
    {
        return new TimespanParseResult(false, TimeSpan.Zero);
    }
}
