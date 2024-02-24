using System;

public record BenchResult<T>(Timespan Ellapsed, T Result);