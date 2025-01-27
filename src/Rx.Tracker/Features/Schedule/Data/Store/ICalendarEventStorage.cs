using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rx.Tracker.Features.Schedule.Data.Dto;
using Rx.Tracker.Features.Schedule.Domain.Entities;

namespace Rx.Tracker.Features.Schedule.Data.Store;

public interface ICalendarEventStorage
{
    /// <summary>
    /// Creates a new event with the provided information in the specified calendar.
    /// </summary>
    /// <param name="calendarId">The unique identifier of the calendar to add the newly created event to.</param>
    /// <param name="title">The title of the event.</param>
    /// <param name="description">The description of the event.</param>
    /// <param name="location">The location of the event.</param>
    /// <param name="startDateTime">The start date and time for the event.</param>
    /// <param name="endDateTime">The end date and time for the event.</param>
    /// <param name="isAllDay">Indicates whether this event should be marked as an all-day event.</param>
    /// <param name="reminders">Reminders for this event.</param>
    /// <returns>The unique identifier of the newly created event.</returns>
    Task<CalendarEventId> Create(
        CalendarId calendarId,
        string title,
        string description,
        string location,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        bool isAllDay = false,
        ReminderDto[]? reminders = null);

    /// <summary>
    /// Creates a new event based on the provided <paramref name="calendarEvent"/> object.
    /// </summary>
    /// <param name="calendarEvent">The event object with the details to save to the calendar specified in this object.</param>
    /// <returns>The unique identifier of the newly created event.</returns>
    Task<CalendarEventId> Create(CalendarEventDto calendarEvent);

    /// <summary>
    /// Retrieves events from a specific calendar or all calendars from the device.
    /// </summary>
    /// <param name="calendarId">The calendar identifier to retrieve events for. If not provided, events will be retrieved for all calendars on the device.</param>
    /// <returns>A list of events from the calendars on the device.</returns>
    Task<IEnumerable<CalendarEventDto>> Read(CalendarId calendarId);

    /// <summary>
    /// Updates an existing event with the provided information.
    /// </summary>
    /// <param name="id">The unique identifier of the event to update.</param>
    /// <param name="title">The updated title for the event.</param>
    /// <param name="description">The updated description for the event.</param>
    /// <param name="location">The updated location for the event.</param>
    /// <param name="startDateTime">The updated start date and time for the event.</param>
    /// <param name="endDateTime">The updated end date and time for the event.</param>
    /// <param name="isAllDay">The updated value that indicates whether this event should be marked as an all-day event.</param>
    /// <param name="reminders">
    /// <para>Reminders for this event.</para>
    /// </param>
    /// <returns>A <see cref="Task"/> object with the current status of the asynchronous operation.</returns>
    Task Update(
        CalendarEventId id,
        string title,
        string description,
        string location,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        bool isAllDay,
        ReminderDto[]? reminders = null);

    /// <summary>
    /// Updates an event based on the provided <paramref name="event"/> object.
    /// </summary>
    /// <param name="event">The event object with the details to update the existing event with.</param>
    /// <returns>A <see cref="Task"/> object with the current status of the asynchronous operation.</returns>
    Task Update(CalendarEventDto @event);

    /// <summary>
    /// Deletes an event, specified by its unique ID, from the device calendar.
    /// </summary>
    /// <param name="id">The unique identifier of the event to be deleted.</param>
    /// <returns>A <see cref="Task"/> object with the current status of the asynchronous operation.</returns>
    Task Delete(CalendarEventId id);

    /// <summary>
    /// Deletes the given event from the device calendar.
    /// </summary>
    /// <param name="event">The event object that is to be deleted.</param>
    /// <returns>A <see cref="Task"/> object with the current status of the asynchronous operation.</returns>
    Task Delete(CalendarEventDto @event);
}