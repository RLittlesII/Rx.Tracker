using NodaTime;

namespace Rx.Tracker.Features.Schedule.Data.Dto;

/// <summary>
/// Represents a reminder for an event.
/// </summary>
/// <param name="DateTime">The date time.</param>
public record ReminderDto(OffsetDateTime DateTime);