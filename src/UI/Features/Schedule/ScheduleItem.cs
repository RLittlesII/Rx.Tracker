using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;

namespace Rx.Tracker.UI.Features.Schedule;

public class ScheduleItem : ContentView
{
    public ScheduleItem() => Content = new Grid
    {
        Children =
        {
            new Label()
               .Text("Item")
        }
    };
}