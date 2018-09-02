using System;

namespace NaturalLanguageTimespanParser
{
    public struct TimespanParseResult
    {
        private TimespanParseResult(bool success, TimeSpan duration)
        {
            Success = success;
            Duration = duration;
        }

        public bool Success { get; }
        public TimeSpan Duration { get; }

        public static TimespanParseResult CreateSuccess(TimeSpan duration)
        {
            return new TimespanParseResult(true, duration);
        }

        public static TimespanParseResult CreateFailure()
        {
            return new TimespanParseResult(false, TimeSpan.Zero);
        }
    }
}