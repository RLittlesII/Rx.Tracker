using NodaTime;
using Rx.Tracker.Features.Schedule.Domain.Entities;

namespace Rx.Tracker.Features.Schedule.Data.Dto;

/// <summary>
/// Represents a calendar event for storage.
/// </summary>
/// <param name="Id">The identifier of the calendar.</param>
/// <param name="Title">The event title.</param>
/// <param name="Description">The calendar description.</param>
/// <param name="Location">The event location.</param>
/// <param name="StartDate">The event start date and time.</param>
/// <param name="EndDate">The event end date and time.</param>
public record CalendarEventDto(
    CalendarEventId Id,
    CalendarId CalendarId,
    string Title,
    string Description,
    string Location,
    OffsetDateTime StartDate,
    OffsetDateTime EndDate,
    ReminderDto[]? Reminders);