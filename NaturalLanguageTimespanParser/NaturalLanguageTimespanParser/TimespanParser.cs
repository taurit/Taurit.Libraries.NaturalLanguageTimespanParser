using System;
using JetBrains.Annotations;

namespace NaturalLanguageTimespanParser
{
    public class TimespanParser : ITimespanParser
    {
        [NotNull]
        public TimespanParseResult Parse([NotNull] string naturalLanguageTimeSpan)
        {
            if (naturalLanguageTimeSpan == null) throw new ArgumentNullException(nameof(naturalLanguageTimeSpan));

            return null;
        }
    }
}