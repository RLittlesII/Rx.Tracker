using Rx.Tracker.Features.Schedule.Domain.Entities;

namespace Rx.Tracker.Features.Schedule.Data.Dto;

/// <summary>
/// Represents a calendar from storage.
/// </summary>
/// <param name="Id">The identifier.</param>
/// <param name="Name">The name.</param>
/// <param name="Color">The color as a hex value.</param>
/// <param name="IsReadOnly">A value indicating whether this instance is read only.</param>
public record CalendarDto(CalendarId Id, string Name, int Color, bool IsReadOnly);