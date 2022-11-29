# DotEukali.QthCalculator
Simple class and extension methods for converting Maidenhead grid references to lat and longs and calculating distances between them.

Supports grid references up to 8 characters.

To use, call `new MaidenHead("{gridReference}")`, or `MaidenHead.Build("{gridReference}")`.

### Available `MaidenHead` extension methods:
A valid reference can be checked by calling `maidenHead.IsValid(bool strict)` - strict will enforce case.
`Latitude()`, `Longitude()` and `DistanceTo()` extension methods are available.  See `MaidenHeadExtensions` for more.
