namespace Rx.Tracker.Features.Schedule.Domain.Entities;

/// <inheritdoc />
public record CalendarId : Id
{
    public static implicit operator CalendarId(string value) => new(value);

    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarId" /> class.
    /// </summary>
    public CalendarId()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CalendarId" /> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public CalendarId(string value)
        : base(value)
    {
    }
}