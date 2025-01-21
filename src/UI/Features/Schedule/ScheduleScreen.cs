using System;
using System.Linq;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Plugin.Maui.Calendar.Controls;
using Plugin.Maui.Calendar.Enums;
using Rx.Tracker.Features.Schedule.ViewModels;
using Rx.Tracker.UI.Features.Components;

namespace Rx.Tracker.UI.Features.Schedule;

public class ScheduleScreen : ScreenBase<ScheduleViewModel>
{
    public ScheduleScreen()
    {
        _calendar = new Calendar // NOTE: [rlittlesii: January 20, 2025] Cannot inline dispose with because control doesn't implement IDisposable.
            {
                CalendarLayout = WeekLayout.Week,
                FirstDayOfWeek = DayOfWeek.Sunday,
                ShowYearPicker = false
            }
           .Bind(IsVisibleProperty, static (ScheduleViewModel viewModel) => viewModel.CurrentState, convert: IsNotInBusyState);
        Content = new VerticalStackLayout
        {
            Children =
            {
                new Label()
                   .CenterHorizontal()
                   .Bind(Label.TextProperty, static (ScheduleViewModel viewModel) => viewModel.CurrentState, convert: state => state.ToString())
                   .Bind(Label.TextColorProperty, static (ScheduleViewModel viewModel) => viewModel.CurrentState, convert: ScheduleStateColorConvert),
                _calendar,
                new Button()
                   .Text("Add Medication")
                   .Center()
                   .Bind(Button.CommandProperty, (ScheduleViewModel viewModel) => viewModel.AddMedicineCommand)
                   .Bind(IsVisibleProperty, static (ScheduleViewModel viewModel) => viewModel.CurrentState, convert: IsNotInBusyState),
            }
        };

        bool IsNotInBusyState(ScheduleStateMachine.ScheduleState state) => IsNotInState(state, ScheduleStateMachine.ScheduleState.Busy);

        bool IsNotInState(ScheduleStateMachine.ScheduleState state, params ScheduleStateMachine.ScheduleState[] states)
            => states.Any(scheduleState => scheduleState != state);

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

    protected override void Destroy() => _calendar.Dispose();

    private readonly Calendar _calendar;
}