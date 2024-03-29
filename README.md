# Taurit.Libraries.NaturalLanguageTimespanParser

## About

This library allows to convert a time span expressed in a **natural language** (in English) into the **machine-friendly representation** (a .NET TimeSpan object).

## Examples

```cs
var timespanParser = new TimespanParser();

// example 1
TimespanParseResult result1 = timespanParser.Parse("Review english lesson @home 20m");
// result1.Success == true
// result1.Duration == {00:20:00}

TimespanParseResult result2 = timespanParser.Parse("@home Read for 45 MINUTES");
// result2.Success == true
// result2.Duration == {00:45:00}

TimespanParseResult result3 = timespanParser.Parse("@home Read for seven minutes");
// result3.Success == true
// result3.Duration == {00:07:00}

TimespanParseResult result4 = timespanParser.Parse("Play drums (2h 30 Min) @home");
// result4.Success == true
// result4.Duration == {02:30:00}
```

More examples are available in unit tests files in the source.