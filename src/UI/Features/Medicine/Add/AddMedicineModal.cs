using System;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Prism.Navigation;
using ReactiveMarbles.Extensions;
using ReactiveUI;
using Rx.Tracker.Features.Medications.ViewModels;
using Rx.Tracker.UI.Features.Components;

namespace Rx.Tracker.UI.Features.Medicine.Add;

public class AddMedicineModal : ScreenBase<AddMedicineViewModel>
{
    public AddMedicineModal()
    {
        Content = new Grid
            {
                ColumnDefinitions =
                    GridRowsColumns.Columns.Define(
                        (Column.Cancel, GridLength.Auto),
                        (Column.Save, GridLength.Auto)),
                RowDefinitions =
                    GridRowsColumns.Rows.Define(
                        (Row.CurrentState, 36),
                        (Row.Title, 36),
                        (Row.Medications, 36),
                        (Row.Dosage, 36),
                        (Row.Recurrence, 36),
                        (Row.Time, 36),
                        (Row.Buttons, 54)),
                Children =
                {
                    new Label()
                       .Row(Row.CurrentState)
                       .ColumnSpan(2)
                       .CenterHorizontal()
                       .Bind(Label.TextProperty, static (AddMedicineViewModel viewModel) => viewModel.CurrentState, convert: state => state.ToString())
                       .Bind(Label.TextColorProperty, static (AddMedicineViewModel viewModel) => viewModel.CurrentState, convert: MedicineStateColorConvert),
                    new Label()
                       .Text("Choose a Medication")
                       .Row(Row.Title)
                       .ColumnSpan(2)
                       .CenterHorizontal()
                       .Bind(IsVisibleProperty, static (AddMedicineViewModel viewModel) => viewModel.CurrentState, convert: IsNotInBusyState),
                    new Picker().Bind(
                            Picker.SelectedItemProperty,
                            static (AddMedicineViewModel viewModel) => viewModel.SelectedName,
                            static (viewModel, text) => viewModel.SelectedName = text)
                       .Bind(Picker.ItemsSourceProperty, (AddMedicineViewModel viewModel) => viewModel.Names)
                       .Row(Row.Medications)
                       .ColumnSpan(2)
                       .Bind(IsVisibleProperty, static (AddMedicineViewModel viewModel) => viewModel.CurrentState, convert: IsNotInBusyState),

                    // new Picker()
                    //    .Bind(Picker.ItemsSourceProperty, (AddMedicineViewModel viewModel) => viewModel.Dosages)
                    //    .Row(Row.Dosage)
                    //    .ColumnSpan(2),
                    // new RadioButton()
                    //    .Bind(Picker.ItemsSourceProperty, (AddMedicineViewModel viewModel) => viewModel.Dosages)
                    //    .Row(Row.Dosage)
                    //    .ColumnSpan(2),
                    new TimePicker()
                       .Row(Row.Time)
                       .ColumnSpan(2)
                       .Bind(IsVisibleProperty, static (AddMedicineViewModel viewModel) => viewModel.CurrentState, convert: IsNotInBusyState),
                    new Button()
                       .Text(nameof(Column.Cancel))
                       .Padding(8)
                       .Row(Row.Buttons)
                       .Column(Column.Cancel)
                       .CenterHorizontal()
                       .Width(128)
                       .Bind(IsVisibleProperty, static (AddMedicineViewModel viewModel) => viewModel.CurrentState, convert: IsNotInBusyState)
                       .Bind(Button.CommandProperty, (AddMedicineViewModel viewModel) => viewModel.BackCommand),
                    new Button()
                       .Text(nameof(Column.Save))
                       .Padding(8)
                       .Row(Row.Buttons)
                       .Column(Column.Save)
                       .CenterHorizontal()
                       .Width(128)
                       .Bind(IsVisibleProperty, static (AddMedicineViewModel viewModel) => viewModel.CurrentState, convert: IsNotInBusyState)
                       .Bind(Button.CommandProperty, (AddMedicineViewModel viewModel) => viewModel.BackCommand),
                    new ActivityIndicator()
                       .Center()
                       .Bind(
                            ActivityIndicator.ColorProperty,
                            static (AddMedicineViewModel viewModel) => viewModel.CurrentState,
                            convert: MedicineStateColorConvert)
                       .Bind(ActivityIndicator.IsRunningProperty, static (AddMedicineViewModel viewModel) => viewModel.CurrentState, convert: IsInBusyState)
                       .Row(Row.CurrentState)
                       .RowSpan(Enum.GetValues<Row>().Length + 1)
                       .Column(Column.Cancel),
                }
            }
           .Center();

        bool IsInBusyState(AddMedicineStateMachine.AddMedicineState state) => state is AddMedicineStateMachine.AddMedicineState.Busy;
        bool IsNotInBusyState(AddMedicineStateMachine.AddMedicineState state) => state is not AddMedicineStateMachine.AddMedicineState.Busy;

        Color MedicineStateColorConvert(AddMedicineStateMachine.AddMedicineState state) => state switch
        {
            AddMedicineStateMachine.AddMedicineState.Initial => Colors.Gold,
            AddMedicineStateMachine.AddMedicineState.Busy    => Colors.MediumPurple,
            AddMedicineStateMachine.AddMedicineState.Loaded  => Colors.DarkOrange,
            AddMedicineStateMachine.AddMedicineState.Failed  => Colors.Red,
            AddMedicineStateMachine.AddMedicineState.Valid   => Colors.Green,
            var _                                            => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    protected override Task Initialize(INavigationParameters parameters)
    {
        this.WhenActivated(
            disposable =>
                this.BindInteraction(ViewModel, model => model.FailedInteraction, context => Toast.Make(context.Input.Message, ToastDuration.Long).Show())
                   .DisposeWith(disposable));

        return Task.CompletedTask;
    }

    private enum Row
    {
        CurrentState,
        Title,
        Medications,
        Dosage,
        Recurrence,
        Time,
        Buttons
    }

    private enum Column
    {
        Cancel,
        Save
    }
}