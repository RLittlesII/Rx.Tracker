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
using static Rx.Tracker.Features.Medications.ViewModels.AddMedicineStateMachine;

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
    /// <param name="stateMachineFactory">The state machine factory.</param>
    public AddMedicineViewModel(INavigator navigator, ICqrs cqrs, ILoggerFactory loggerFactory, Func<AddMedicineStateMachine> stateMachineFactory)
        : base(navigator, cqrs, loggerFactory)
    {
        _stateMachine = stateMachineFactory.Invoke().DisposeWith(Garbage);
        var whenChanged =
            this.WhenChanged(
                    static viewModel => viewModel.SelectedName,
                    static viewModel => viewModel.SelectedDosage,
                    static viewModel => viewModel.SelectedRecurrence,
                    static viewModel => viewModel.SelectedTime,
                    static (name, dosage, recurrence, time) => (name, dosage, recurrence, time))
               .Publish()
               .RefCount();

        // NOTE: [rlittlesii: December 06, 2024] I would use Fluent Validation here, but my usecases dont warrant my normal approach.
        whenChanged
           .Where(ArePropertiesValid)
           .Select(static _ => new ScheduledMedication(MealRequirements.After, new Medication(), Recurrence.Daily, DateTimeOffset.MinValue))
           .WhereIsNotNull()
           .LogTrace(Logger, static medication => medication, "{ScheduledMedication}")
           .SelectMany(medication => _stateMachine.FireAsync(AddMedicineTrigger.Validated).ContinueWith(_ => medication))
           .Subscribe();

        AddCommand = RxCommand.Create<ScheduledMedication?>(
            ExecuteAdd,
            whenChanged.Select(ArePropertiesValid));

        _currentState =
            _stateMachine
               .Current
               .AsValue(_ => { }, _ => RaisePropertyChanged(nameof(CurrentState)), () => AddMedicineState.Initial)
               .DisposeWith(Garbage);

        ConfigureMachine();

        // TODO: [rlittlesii: December 03, 2024] Should this be somewhere else?!
        static bool ArePropertiesValid((string? Name, Dosage? Dosage, Recurrence? Recurrence, DateTimeOffset? Time) tuple) => tuple is
        {
            Name: not null,
            Dosage: not null,
            Recurrence: not null,
            Time: not null
        };

        async Task ExecuteAdd(ScheduledMedication? scheduledMedication)
        {
            ArgumentNullException.ThrowIfNull(scheduledMedication);
            await cqrs.Execute(AddMedicationToSchedule.Create(scheduledMedication));
        }
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
    public AddMedicineState CurrentState => _currentState.Value;

    /// <inheritdoc/>
    protected override async Task Initialize(ICqrs cqrs)
    {
        await _stateMachine.FireAsync(AddMedicineTrigger.Load);

        var result = await cqrs.Query(LoadMedication.Create());

        Medicine = new ObservableCollection<Medication>(result.Medicines);

        Dosages = new ObservableCollection<Dosage>(
            result.Medicines
               .SelectMany(medication => medication.Dosages)
               .GroupBy(dosage => dosage.Weight, dosage => dosage)
               .SelectMany(grouping => grouping.DistinctBy(dosage => (dosage.Quantity, dosage.Weight))));
        await _stateMachine.FireAsync(AddMedicineTrigger.Load);
    }

    private void ConfigureMachine()
    {
        _stateMachine
           .Configure(AddMedicineState.Initial)
           .Permit(AddMedicineTrigger.Load, AddMedicineState.Busy)
           .OnEntryAsync(transition => Initialize(Mediator));

        _stateMachine
           .Configure(AddMedicineState.Busy)
           .Permit(AddMedicineTrigger.Load, AddMedicineState.Loaded)
           .OnEntryAsync(transition => Task.CompletedTask);

        _stateMachine
           .Configure(AddMedicineState.Loaded)
           .Permit(AddMedicineTrigger.Validated, AddMedicineState.Valid)
           .OnEntryAsync(transition => Task.CompletedTask);

        _stateMachine
           .Configure(AddMedicineState.Valid)
           .OnEntryAsync(transition => Task.CompletedTask);
    }

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private readonly AddMedicineStateMachine _stateMachine;

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private readonly IValueBinder<AddMedicineState> _currentState;

    private string? _selectedName;
    private Dosage? _selectedDosage;
    private Recurrence? _selectedRecurrence;
    private DateTimeOffset? _selectedTime;
    private ObservableCollection<Dosage> _dosages = [];
    private ObservableCollection<Medication> _medicine = [];
}