using Domain.Enums;

namespace Domain.Extensions;

public static class DurationExtensions
{
    public static DateTimeOffset ToDateTimeOffset(this Duration duration)
    {
        return duration switch
        {
            Duration.Unspecified => DateTimeOffset.MinValue,
            Duration.Forever => DateTimeOffset.MaxValue,
            Duration.OneDay => DateTimeOffset.UtcNow.AddDays(1),
            Duration.OneWeak => DateTimeOffset.UtcNow.AddDays(7),
            Duration.OneMonth => DateTimeOffset.UtcNow.AddMonths(1),
            Duration.OneYear => DateTimeOffset.UtcNow.AddYears(1),
            _ => throw new ArgumentOutOfRangeException("Parameter duration is out of range")
        };
    }
}
