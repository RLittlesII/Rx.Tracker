using System;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Rx.Tracker.Features.Schedule.ViewModels;
using Rx.Tracker.UI.Features.Components;

namespace Rx.Tracker.UI.Features.Schedule;

public class ScheduleScreen : ScreenBase<ScheduleViewModel>
{
    public ScheduleScreen()
    {
        Content = new VerticalStackLayout
        {
            Children =
            {
                new Label()
                   .CenterHorizontal()
                   .Bind(Label.TextProperty, static (ScheduleViewModel viewModel) => viewModel.CurrentState, convert: state => state.ToString())
                   .Bind(Label.TextColorProperty, static (ScheduleViewModel viewModel) => viewModel.CurrentState, convert: ScheduleStateColorConvert),
                new Button()
                   .Text("Add Medication")
                   .Center()
                   .Bind(Button.CommandProperty, (ScheduleViewModel viewModel) => viewModel.AddMedicineCommand),
            }
        };

        Color ScheduleStateColorConvert(ScheduleStateMachine.ScheduleState state) => state switch
        {
            ScheduleStateMachine.ScheduleState.Initial     => Colors.Gold,
            ScheduleStateMachine.ScheduleState.Busy        => Colors.MediumPurple,
            ScheduleStateMachine.ScheduleState.Loaded      => Colors.DarkOrange,
            ScheduleStateMachine.ScheduleState.DaySchedule => Colors.DarkOrange,
            ScheduleStateMachine.ScheduleState.Failed      => Colors.Red,
            var _                                          => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }
}