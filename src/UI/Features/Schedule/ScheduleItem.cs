using CommunityToolkit.Maui.Markup;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Rx.Tracker.Features.Schedule.Domain.Entities;

namespace Rx.Tracker.UI.Features.Schedule;

public class ScheduleItem : ViewCell
{
    public ScheduleItem() => View = new Grid
    {
        Margin = new Thickness(12, 0),
        ColumnDefinitions =
            GridRowsColumns.Columns.Define(
                (Column.Name, GridLength.Auto),
                (Column.Space, GridLength.Star),
                (Column.Time, GridLength.Auto)),
        Children =
        {
            new Label()
               .Start()
               .Column(Column.Name)
               .Bind(Label.TextProperty, (ScheduledMedication medication) => medication.Medication.Id),
            new Label()
               .Column(Column.Time)
               .Bind(
                    Label.TextProperty,
                    (ScheduledMedication medication) => medication.ScheduledTime,
                    convert: time => time.TimeOfDay.ToTimeOnly().ToShortTimeString()),
        }
    };

    private enum Column
    {
        /// <summary>
        /// The name.
        /// </summary>
        Name,

        /// <summary>
        /// The final frontier.
        /// </summary>
        Space,

        /// <summary>
        /// The time.
        /// </summary>
        Time
    }
}