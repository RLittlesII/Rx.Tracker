using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Maui.CalendarStore;
using Rx.Tracker.Features.Schedule.Data.Dto;
using Rx.Tracker.Features.Schedule.Data.Store;
using Rx.Tracker.Features.Schedule.Domain.Entities;

namespace Rx.Tracker.UI.Features.Schedule;

public class CalendarStorageFacade : ICalendarStorage, ICalendarEventStorage
{
    public CalendarStorageFacade(ICalendarStore calendarStore) => _calendarStore = calendarStore;

    Task<CalendarId> ICalendarStorage.Create(string name, int color) => throw new NotImplementedException();

    Task<CalendarEventId> ICalendarEventStorage.Create(
        CalendarId calendarId,
        string title,
        string description,
        string location,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        bool isAllDay,
        ReminderDto[]? reminders) => throw new NotImplementedException();

    Task<CalendarEventId> ICalendarEventStorage.Create(CalendarEventDto calendarEvent) => throw new NotImplementedException();

    Task<IEnumerable<CalendarEventDto>> ICalendarEventStorage.Read(CalendarId calendarId) => throw new NotImplementedException();

    Task ICalendarEventStorage.Update(
        CalendarEventId id,
        string title,
        string description,
        string location,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        bool isAllDay,
        ReminderDto[]? reminders) => throw new NotImplementedException();

    Task ICalendarEventStorage.Update(CalendarEventDto @event) => throw new NotImplementedException();

    Task ICalendarEventStorage.Delete(CalendarEventId id) => throw new NotImplementedException();

    Task ICalendarEventStorage.Delete(CalendarEventDto @event) => throw new NotImplementedException();

    Task<CalendarDto> ICalendarStorage.Read(CalendarId calendarId) => throw new NotImplementedException();

    Task ICalendarStorage.Update(CalendarId id, string name, int color) => throw new NotImplementedException();

    Task ICalendarStorage.Delete(CalendarId calendarId) => throw new NotImplementedException();

    Task ICalendarStorage.Delete(CalendarDto delete) => throw new NotImplementedException();

    private readonly ICalendarStore _calendarStore;
}