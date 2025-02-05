using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;
using Rx.Tracker.Features.Schedule.Domain.Entities;

namespace Rx.Tracker.UI.Features.Schedule;

public class ScheduleItem : ViewCell
{
    public ScheduleItem() => View = new StackLayout
    {
        Children =
        {
            new Label()
            .Bind(Label.TextProperty, (ScheduledMedication medication) => medication.Medication.Id)
        }
    };
}