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
        Content = new StackLayout
        {
            Children =
            {
                new Label()
                   .CenterHorizontal()
                   .Top()
                   .Bind(Label.TextProperty, static (ScheduleViewModel viewModel) => viewModel.CurrentState, convert: state => state.ToString())
                   .Bind(Label.TextColorProperty, static (ScheduleViewModel viewModel) => viewModel.CurrentState, convert: ScheduleStateColorConvert),

                new Calendar // NOTE: [rlittlesii: January 20, 2025] Cannot inline dispose with because control doesn't implement IDisposable.
                    {
                        CalendarLayout = WeekLayout.Week,
                        FirstDayOfWeek = DayOfWeek.Sunday,
                        ShowYearPicker = false,
                    }
                   .Bind(IsVisibleProperty, static (ScheduleViewModel viewModel) => viewModel.CurrentState, convert: IsNotInBusyState),
                new ListView
                    {
                        ItemTemplate = new DataTemplate(() => new ScheduleItem())
                    }
                   .Bind(ItemsView.ItemsSourceProperty, static (ScheduleViewModel viewModel) => viewModel.Today)
                   .Bind(IsVisibleProperty, static (ScheduleViewModel viewModel) => viewModel.CurrentState, convert: IsNotInBusyState),
                new Button()
                   .Text("Add Medication")
                   .Bottom()
                   .Margin(12)
                   .Height(56)
                   .Bind(Button.CommandProperty, (ScheduleViewModel viewModel) => viewModel.AddMedicineCommand)
                   .Bind(IsVisibleProperty, static (ScheduleViewModel viewModel) => viewModel.CurrentState, convert: IsNotInBusyState),
                new ActivityIndicator()
                   .Center()
                   .Bind(
                        ActivityIndicator.ColorProperty,
                        static (ScheduleViewModel viewModel) => viewModel.CurrentState,
                        convert: ScheduleStateColorConvert)
                   .Bind(ActivityIndicator.IsRunningProperty, static (ScheduleViewModel viewModel) => viewModel.CurrentState, convert: IsInBusyState),
            }
        };

        bool IsInBusyState(ScheduleStateMachine.ScheduleState state) => IsInState(state, ScheduleStateMachine.ScheduleState.Busy);
        bool IsNotInBusyState(ScheduleStateMachine.ScheduleState state) => IsNotInState(state, ScheduleStateMachine.ScheduleState.Busy);

        bool IsInState(ScheduleStateMachine.ScheduleState state, params ScheduleStateMachine.ScheduleState[] states)
            => states.Any(scheduleState => scheduleState == state);

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
}