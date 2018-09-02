using JetBrains.Annotations;

namespace NaturalLanguageTimespanParser
{
    public interface ITimespanParser
    {
        TimespanParseResult Parse([NotNull] string naturalLanguageTimeSpan);
    }
}