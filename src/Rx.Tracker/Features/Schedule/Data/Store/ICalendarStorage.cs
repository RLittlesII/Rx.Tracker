using Rx.Tracker.Features.Schedule.Data.Dto;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using System.Threading.Tasks;

namespace Rx.Tracker.Features.Schedule.Data.Store;

/// <summary>
/// Interface representing calendar storage.
/// </summary>
public interface ICalendarStorage
{
    /// <summary>
    /// Creates a new calendar.
    /// </summary>
    /// <param name="name">The name for the calendar to create.</param>
    /// <param name="color">The color to use for the calendar to create.</param>
    /// <returns>The unique identifier of the created calendar.</returns>
    Task<CalendarId> Create(string name, int color);

    /// <summary>
    /// Retrieves a specific calendar from the device.
    /// </summary>
    /// <param name="calendarId">The unique identifier of the calendar to retrieve.</param>
    /// <returns>A <see cref="CalendarDto" /> object that represents the requested calendar from the user's device.</returns>
    Task<CalendarDto> Read(CalendarId calendarId);

    /// <summary>
    /// Updates an existing calendar with the provided values.
    /// </summary>
    /// <param name="id">The unique identifier of the existing calendar.</param>
    /// <param name="name">The name.</param>
    /// <param name="color">The color .</param>
    /// <returns>A <see cref="Task" /> object with the current status of the asynchronous operation.</returns>
    Task Update(CalendarId id, string name, int color);

    /// <summary>
    /// Deletes a calendar, specified by its unique ID, from the device.
    /// </summary>
    /// <param name="calendarId">The unique identifier of the calendar to be deleted.</param>
    /// <returns>A <see cref="Task" /> object with the current status of the asynchronous operation.</returns>
    /// <remarks>If the calendar is part of a cloud service, the calendar might also be deleted from all other devices where the calendar is used.</remarks>
    Task Delete(CalendarId calendarId);

    /// <summary>
    /// Deletes the given calendar from the device.
    /// </summary>
    /// <param name="delete">The calendar object that is to be deleted.</param>
    /// <returns>A <see cref="Task" /> object with the current status of the asynchronous operation.</returns>
    /// <remarks>If the calendar is part of a cloud service, the calendar might also be deleted from all other devices where the calendar is used.</remarks>
    Task Delete(CalendarDto delete);
}