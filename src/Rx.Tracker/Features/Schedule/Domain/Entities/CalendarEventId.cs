namespace Rx.Tracker.Features.Schedule.Domain.Entities;

/// <inheritdoc />
public record CalendarEventId : Id
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarEventId"/> class.
    /// </summary>
    public CalendarEventId()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarEventId"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public CalendarEventId(string value)
        : base(value)
    {
    }

    public static implicit operator CalendarEventId(string value) => new(value);
}