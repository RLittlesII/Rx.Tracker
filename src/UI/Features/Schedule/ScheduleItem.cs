using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;

namespace Rx.Tracker.UI.Features.Schedule;

public class ScheduleItem : ContentView
{
    public ScheduleItem() => Content = new StackLayout
    {
        Children =
        {
            new Label()
               .Text("Item")

            // .Bind(Label.TextProperty, (ScheduledMedication medication) => medication.Medication.Id)
        }
    };
}