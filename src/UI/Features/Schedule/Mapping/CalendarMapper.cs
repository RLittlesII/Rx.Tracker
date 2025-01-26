using Microsoft.Maui.Graphics;
using Plugin.Maui.CalendarStore;
using Riok.Mapperly.Abstractions;
using Rx.Tracker.Features.Schedule.Data.Dto;

namespace Rx.Tracker.UI.Features.Schedule.Mapping;

[Mapper]
public static partial class CalendarMapper
{
    [MapProperty(nameof(Calendar.Color), nameof(CalendarDto.Color), Use = nameof(ToArgb))]
    public static partial CalendarDto Map(Calendar request);

    [MapProperty(nameof(CalendarDto.Color), nameof(Calendar.Color), Use = nameof(FromArgb))]
    public static partial Calendar Map(CalendarDto request);

    private static int ToArgb(Color color) => color.ToInt();

    private static Color FromArgb(int color) => Color.FromInt(color);
}

// [Mapper]
// public static partial class CalendarEventMapper
// {
//     public static partial CalendarEventDto Map(CalendarEvent request);
//
//     public static partial CalendarEvent Map(CalendarEventDto request);
//
//     private static int ToArgb(Color color) => color.ToInt();
//
//     private static Color FromArgb(int color) => Color.FromInt(color);
// }