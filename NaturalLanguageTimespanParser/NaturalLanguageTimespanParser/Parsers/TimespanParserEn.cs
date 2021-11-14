using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NaturalLanguageTimespanParser.Parsers
{
    /// <summary>
    ///     A parser for en-XX culture
    /// </summary>
    internal class TimespanParserEn : ICultureSpecificParser
    {
        /// <summary>
        ///     Regex to find patterns indicating time duration, for example: (1h 30 min)
        /// </summary>
        private static readonly Regex RegexFindTime = new Regex(
            @"(?<time1>[\d,\.]+)(\s*?)(?<unit1>h|hour|minute|minut|min|m)([s\s]|\z|\)|\]|}|"")((\s*?)(?<time2>[\d,\.]+)(\s*?)(?<unit2>minutes|minute|minut|min|m))?",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant);


        /// <inheritdoc />
        public TimespanParseResult Parse(string naturalLanguageTimeSpan)
        {
            if (naturalLanguageTimeSpan == null) throw new ArgumentNullException(nameof(naturalLanguageTimeSpan));

            // this is a simple regex-based implementation that I have imported from some of my old projects.
            // it's pretty limited and to add support for more natural language forms it needs to be implemented differently (however, tests for all the new supported scenarios should be added first)

            var totalMinutes = 0;
            var match = RegexFindTime.Match(naturalLanguageTimeSpan);

            if (!match.Success) return TimespanParseResult.CreateFailure();

            // make sure there is no another, conflicting duration defined in the same string - then we wouldn't know which one to choose
            var nextMatch = match.NextMatch();
            if (nextMatch.Success) return TimespanParseResult.CreateFailure();

            if (decimal.TryParse(match.Groups["time1"].Value, NumberStyles.Any, CultureInfo.InvariantCulture,
                out var quantity))
            {
                var unit = match.Groups["unit1"].Value;

                if (unit.StartsWith("h", StringComparison.OrdinalIgnoreCase))
                {
                    // assume that unit is hours
                    totalMinutes = (int) (quantity * 60m);

                    // there might be another part of the string specifying minutes in this case
                    if (decimal.TryParse(match.Groups["time2"].Value, NumberStyles.Any, CultureInfo.InvariantCulture,
                        out var quantityMinutesPart)) totalMinutes += (int) quantityMinutesPart;
                }
                else
                {
                    // assume that unit is minutes
                    totalMinutes = (int) quantity;
                }
            }

            return TimespanParseResult.CreateSuccess(TimeSpan.FromMinutes(totalMinutes));
        }
    }
}