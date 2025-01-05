using System;
using JetBrains.Annotations;
using NodaTime;
using NodaTime.Extensions;
using static NodaTime.Calendars.WeekYearRules;

namespace Rx.Tracker.Extensions;

/// <summary>
/// <see cref="NodaTime"/> extensions.
/// </summary>
public static class NodaTimeExtensions
{
    /// <summary>
    /// Gets a value indicating whether the <see cref="OffsetDateTime"/> is in the same week as the provided <see cref="LocalDate"/>.
    /// </summary>
    /// <param name="offsetDateTime">The offset date.</param>
    /// <param name="localDate">The local date.</param>
    /// <returns>Whether the offset is in the date.</returns>
    [Pure]
    public static bool IsInSameWeek(this OffsetDateTime offsetDateTime, LocalDate localDate)
    {
        var offsetWeekOfYear = Iso.GetWeekOfWeekYear(offsetDateTime.Date);
        var localDateWeekOfYear = Iso.GetWeekOfWeekYear(localDate);
        var isWeekOfYear = offsetWeekOfYear == localDateWeekOfYear;
        var isSameYear = offsetDateTime.Year == localDate.Year;
        return isWeekOfYear && isSameYear;
    }

    /// <summary>
    /// Gets the <see cref="LocalDate"/> from the <see cref="DateTime"/>.
    /// </summary>
    /// <param name="dateTime">The date.</param>
    /// <returns>The local date.</returns>
    [Pure]
    public static LocalDate ToLocalDate(this DateTime dateTime) => dateTime.ToLocalDateTime().Date;

    /// <summary>
    /// Gets the <see cref="LocalDate"/> from the <see cref="DateTime"/>.
    /// </summary>
    /// <param name="dateTime">The date.</param>
    /// <returns>The local date.</returns>
    [Pure]
    public static LocalDate ToLocalDate(this DateTimeOffset dateTime) => dateTime.DateTime.ToLocalDateTime().Date;
}