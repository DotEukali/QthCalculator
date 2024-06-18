# DotEukali.QthCalculator Documentation
v2.0.0 is a complete breaking change to previous versions.  Instead of instantiating the `Maidenhead` class explicitly, there is a `MaidenheadCalculator` static class that exposes all available functionality. If a DI approach is preferred, `IMaidenheadCalculator` can be injected as a dependency, and registered as a singleton via `TryAddMaidenheadCalculator` The implementing class is a wrapper for the static class.
`Maidenhead` is now a readonly struct, instead of a class. This is potentially important to note as it is now a value type, not a reference type.

Existing extension methods for retrieving latitude and longitude values from a `Maidenhead` object remain, however the signatures have changed to incoporate three new optional enums:
`LatitudePoint` & `LongitudePoint` - used to request the latitude or longitude from an edge or middle of the Maidenhead area. Replaces the `getMiddle` boolean in previous versions.
`MaidenheadSize`: used to override the grid reference size. Replaces the `granularity` integer in previous versions.
