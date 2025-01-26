using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;
using NodaTime;
using Plugin.Maui.CalendarStore;
using Rx.Tracker.Features.Schedule.Data.Dto;
using Rx.Tracker.Features.Schedule.Data.Store;
using Rx.Tracker.Features.Schedule.Domain.Entities;

namespace Rx.Tracker.UI.Features.Schedule;

public class CalendarStorageFacade : ICalendarStorage, ICalendarEventStorage
{
    public CalendarStorageFacade(ICalendarStore calendarStore) => _calendarStore = calendarStore;

    Task<CalendarId> ICalendarStorage.Create(CalendarDto calendar) => _calendarStore.CreateCalendar(calendar.Name, Color.FromInt(calendar.Color)).ContinueWith(task => new CalendarId(task.Result));

    Task<CalendarEventId> ICalendarEventStorage.Create(
        CalendarId calendarId,
        string title,
        string description,
        string location,
        OffsetDateTime startDateTime,
        OffsetDateTime endDateTime,
        bool isAllDay,
        ReminderDto[]? reminders) => _calendarStore.CreateEvent(
        calendarId.ToString(),
        title,
        description,
        location,
        startDateTime.ToDateTimeOffset(),
        endDateTime.ToDateTimeOffset(),
        isAllDay).ContinueWith(task => new CalendarEventId(task.Result));

    Task<IReadOnlyList<CalendarEventDto>> ICalendarEventStorage.Read(CalendarId calendarId) => throw new System.NotImplementedException();

    Task<CalendarEventDto> ICalendarEventStorage.Read(CalendarEventId eventId) => throw new System.NotImplementedException();

    Task<CalendarEventId> ICalendarEventStorage.Create(CalendarEventDto calendarEvent) => throw new System.NotImplementedException();

    Task ICalendarEventStorage.UpdateEvent(
        CalendarEventId eventId,
        string title,
        string description,
        string location,
        OffsetDateTime startDateTime,
        OffsetDateTime endDateTime,
        bool isAllDay,
        ReminderDto[]? reminders) => _calendarStore.UpdateEvent(
        eventId.ToString(),
        title,
        description,
        location,
        startDateTime.ToDateTimeOffset(),
        endDateTime.ToDateTimeOffset(),
        isAllDay);

    Task ICalendarEventStorage.UpdateEvent(CalendarEventDto eventToUpdate) => throw new System.NotImplementedException();

    Task ICalendarEventStorage.DeleteEvent(CalendarEventId eventId) => throw new System.NotImplementedException();

    Task ICalendarEventStorage.DeleteEvent(CalendarEventDto eventToDelete) => throw new System.NotImplementedException();

    Task<CalendarDto> ICalendarStorage.Read(CalendarId calendarId) => throw new System.NotImplementedException();

    Task ICalendarStorage.Update(CalendarDto calendar) => throw new System.NotImplementedException();

    Task ICalendarStorage.Delete(CalendarId calendarId) => throw new System.NotImplementedException();

    Task ICalendarStorage.Delete(CalendarDto calendarToDelete) => throw new System.NotImplementedException();

    private readonly ICalendarStore _calendarStore;
}