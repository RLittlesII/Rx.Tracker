using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ReactiveMarbles.Command;
using ReactiveMarbles.Extensions;
using ReactiveMarbles.Mvvm;
using ReactiveMarbles.PropertyChanged;
using Rx.Tracker.Extensions;
using Rx.Tracker.Features.Medications.Domain.Commands;
using Rx.Tracker.Features.Medications.Domain.Entities;
using Rx.Tracker.Features.Medications.Domain.Queries;
using Rx.Tracker.Mediation;
using Rx.Tracker.Navigation;

namespace Rx.Tracker.Features.Medications.ViewModels;

/// <summary>
/// The view model for adding medicine to the chart.
/// </summary>
public class AddMedicineViewModel : ViewModelBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddMedicineViewModel"/> class.
    /// </summary>
    /// <param name="navigator">The navigator.</param>
    /// <param name="cqrs">The cqrs mediator.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    public AddMedicineViewModel(INavigator navigator, ICqrs cqrs, ILoggerFactory loggerFactory)
        : base(navigator, cqrs, loggerFactory)
    {
        _stateMachine = new AddMedicineStateMachine().DisposeWith(Garbage);

        var whenChanged =
            this.WhenChanged(
                    static viewModel => viewModel.SelectedName,
                    static viewModel => viewModel.SelectedDosage,
                    static viewModel => viewModel.SelectedRecurrence,
                    static viewModel => viewModel.SelectedTime,
                    static (name, dosage, recurrence, time) => (name, dosage, recurrence, time))
               .Publish()
               .RefCount();

        whenChanged
           .Where(ArePropertiesValid)
           .Select(static _ => new ScheduledMedication(MealRequirements.After, new Medication(), Recurrence.Daily, DateTimeOffset.MinValue))
           .WhereIsNotNull()
           .LogTrace(Logger, static medication => medication, "{ScheduledMedication}")
           .SelectMany(medication => _stateMachine.FireAsync(AddMedicineStateMachine.AddMedicineTrigger.Validated).ContinueWith(_ => medication))
           .InvokeCommand(this, static viewModel => viewModel.AddCommand);

        AddCommand = RxCommand.Create<ScheduledMedication?>(
            ExecuteAdd,
            whenChanged.Select(ArePropertiesValid));

        async Task ExecuteAdd(ScheduledMedication? scheduledMedication)
        {
            ArgumentNullException.ThrowIfNull(scheduledMedication);
            await cqrs.Execute(AddMedicationToSchedule.Create(scheduledMedication));
        }

        // TODO: [rlittlesii: December 03, 2024] Should this be somewhere else?!
        static bool ArePropertiesValid((string? Name, Dosage? Dosage, Recurrence? Recurrence, DateTimeOffset? Time) tuple) => tuple is
        {
            Name: not null,
            Dosage: not null,
            Recurrence: not null,
            Time: not null
        };

        _currentState =
            _stateMachine
               .StateChanged
               .AsValue(_ => { }, _ => RaisePropertyChanged(nameof(CurrentState)), () => AddMedicineStateMachine.AddMedicineState.Initial)
               .DisposeWith(Garbage);

        ConfigureMachine();
    }

    /// <summary>
    /// Gets the add command.
    /// </summary>
    public RxCommand<ScheduledMedication?, Unit> AddCommand { get; }

    /// <summary>
    /// Gets or sets the list of medicine.
    /// </summary>
    public ObservableCollection<Medication> Medicine
    {
        get => _medicine;
        set => RaiseAndSetIfChanged(ref _medicine, value);
    }

    /// <summary>
    /// Gets or sets the selected name.
    /// </summary>
    public string? SelectedName
    {
        get => _selectedName;
        set => RaiseAndSetIfChanged(ref _selectedName, value);
    }

    /// <summary>
    /// Gets or sets the selected dosage.
    /// </summary>
    public Dosage? SelectedDosage
    {
        get => _selectedDosage;
        set => RaiseAndSetIfChanged(ref _selectedDosage, value);
    }

    /// <summary>
    /// Gets or sets the selected recurrence.
    /// </summary>
    public Recurrence? SelectedRecurrence
    {
        get => _selectedRecurrence;
        set => RaiseAndSetIfChanged(ref _selectedRecurrence, value);
    }

    /// <summary>
    /// Gets or sets the selected time.
    /// </summary>
    public DateTimeOffset? SelectedTime
    {
        get => _selectedTime;
        set => RaiseAndSetIfChanged(ref _selectedTime, value);
    }

    /// <summary>
    /// Gets or sets the dosages.
    /// </summary>
    public ObservableCollection<Dosage> Dosages
    {
        get => _dosages;
        set => RaiseAndSetIfChanged(ref _dosages, value);
    }

    /// <summary>
    /// Gets the current state of the machine.
    /// </summary>
    public AddMedicineStateMachine.AddMedicineState CurrentState => _currentState.Value;

    /// <inheritdoc/>
    protected override async Task Initialize(ICqrs cqrs)
    {
        await _stateMachine.FireAsync(AddMedicineStateMachine.AddMedicineTrigger.Load);

        var result = await cqrs.Query(LoadMedication.Create());

        Medicine = new ObservableCollection<Medication>(result.Medicines);
        Dosages = new ObservableCollection<Dosage>(
            result.Medicines
               .SelectMany(medication => medication.Dosages)
               .GroupBy(dosage => dosage.Weight, dosage => dosage)
               .SelectMany(grouping => grouping.DistinctBy(dosage => (dosage.Quantity, dosage.Weight))));
        await _stateMachine.FireAsync(AddMedicineStateMachine.AddMedicineTrigger.Load);
    }

    private void ConfigureMachine()
    {
        _stateMachine
           .Configure(AddMedicineStateMachine.AddMedicineState.Initial)
           .Permit(AddMedicineStateMachine.AddMedicineTrigger.Load, AddMedicineStateMachine.AddMedicineState.Busy)
           .OnEntryAsync(transition => Initialize(Mediator));

        _stateMachine
           .Configure(AddMedicineStateMachine.AddMedicineState.Busy)
           .Permit(AddMedicineStateMachine.AddMedicineTrigger.Load, AddMedicineStateMachine.AddMedicineState.Loaded)
           .OnEntryAsync(transition => Task.CompletedTask);

        _stateMachine
           .Configure(AddMedicineStateMachine.AddMedicineState.Loaded)
           .OnEntryAsync(transition => Task.CompletedTask);
    }

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private readonly AddMedicineStateMachine _stateMachine;

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private readonly ValueBinder<AddMedicineStateMachine.AddMedicineState> _currentState;

    private string? _selectedName;
    private Dosage? _selectedDosage;
    private Recurrence? _selectedRecurrence;
    private DateTimeOffset? _selectedTime;
    private ObservableCollection<Dosage> _dosages = [];
    private ObservableCollection<Medication> _medicine = [];
}