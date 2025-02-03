using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;

namespace Rx.Tracker.UI.Features.Schedule;

public class ScheduleItem : ViewCell
{
    public ScheduleItem() => View = new StackLayout
    {
        Children =
        {
            new Label()
               .Text("Item")

            // .Bind(Label.TextProperty, (ScheduledMedication medication) => medication.Medication.Id)
        }
    };
}