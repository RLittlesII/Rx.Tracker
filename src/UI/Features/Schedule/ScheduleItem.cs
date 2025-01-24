using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;
using Rx.Tracker.Features.Schedule.Domain.Entities;

namespace Rx.Tracker.UI.Features.Schedule;

public class ScheduleItem : ContentView
{
    public ScheduleItem() => Content = new StackLayout
    {
        Children =
        {
            new Label()
               .Bind(Label.TextProperty, (ScheduledMedication medication) => medication.Medication.Id)
        }
    };
}