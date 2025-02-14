using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NodaTime.Extensions;
using ReactiveMarbles.Command;
using ReactiveMarbles.Extensions;
using ReactiveMarbles.Mvvm;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using Rx.Tracker.Exceptions;
using Rx.Tracker.Extensions;
using Rx.Tracker.Features.Medications.Domain.Commands;
using Rx.Tracker.Features.Medications.Domain.Entities;
using Rx.Tracker.Features.Medications.Domain.Queries;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Interactions;
using Rx.Tracker.Mediation;
using Rx.Tracker.Navigation;
using Stateless;
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
        AddCommand = RxCommand.Create<ScheduledMedication?, AddMedicineTrigger>(
            ExecuteAdd,
            _stateMachine.Current.Select(state => state == AddMedicineState.Valid));

        // NOTE: [rlittlesii: January 13, 2025] Uncomment to demonstrate the exception handler.
        // BackCommand = RxCommand.Create(() => navigator.Back(1));
        BackCommand = RxCommand.Create(ExecuteBack);

        FailedInteraction = new Interaction<ToastMessage, Unit>();
        CompletedInteraction = new Interaction<ToastMessage, Unit>();

        AddCommand
           .Do(_ => { })
           .Select(trigger => _stateMachine.FireAsync(trigger).ConfigureAwait(true))
           .Subscribe();

        // NOTE: [rlittlesii: January 25, 2025] If this approach catches on, abstract it to the base
        BackCommand
           .Where(state => state != NavigationState.Succeeded)
           .LogTrace(Logger, state => state, "Navigation State: {State}")
           .Select(_ => _stateMachine.FireAsync(AddMedicineTrigger.Failure).ConfigureAwait(true))
           .Subscribe();

        _currentState =
            _stateMachine
               .Current
               .AsValue(_ => { }, _ => RaisePropertyChanged(nameof(CurrentState)), () => AddMedicineState.Initial)
               .DisposeWith(Garbage);

        // NOTE: [rlittlesii: December 06, 2024] I would use Fluent Validation here, but my usecases dont warrant my normal approach.
        _medication = this.WhenChanged(
                static viewModel => viewModel.SelectedName,
                static viewModel => viewModel.SelectedDosage,
                static viewModel => viewModel.SelectedRecurrence,
                static viewModel => viewModel.SelectedTime,
                static (name, dosage, recurrence, time) => (name, dosage, recurrence, time))
           .LogTrace(Logger, state => state, "State: {State}")
           .Where(ArePropertiesValid)

            // QUESTION: [rlittlesii: December 07, 2024] What were you thinking?!
           .Select(static _ => new ScheduledMedication(MealRequirements.None, new Medication(), Recurrence.Daily, DateTimeOffset.MinValue.ToOffsetDateTime()))
           .WhereIsNotNull()
           .LogTrace(Logger, static medication => medication, "{ScheduledMedication}")
           .SelectMany(medication => _stateMachine.FireAsync(AddMedicineTrigger.Validated).ContinueWith(_ => medication))
           .AsValue(_ => { }, _ => RaisePropertyChanged(nameof(Medication)), () => null!)
           .DisposeWith(Garbage);

        ConfigureMachine();

        // TODO: [rlittlesii: December 03, 2024] Should this be somewhere else?!
        static bool ArePropertiesValid((MedicationId? Name, Dosage? Dosage, Recurrence? Recurrence, TimeSpan? Time) tuple) => tuple is
        {
            Name: not null,
            Dosage: not null,
            Recurrence: not null,
            Time: not null
        };

        async Task<AddMedicineTrigger> ExecuteAdd(ScheduledMedication? scheduledMedication)
        {
            ArgumentNullException.ThrowIfNull(scheduledMedication);

            await _stateMachine.FireAsync(AddMedicineTrigger.Save);
            await cqrs.Execute(AddMedicationToSchedule.Create(new UserId(), scheduledMedication)); // TODO: [rlittlesii: January 25, 2025] Timeout?

            return AddMedicineTrigger.Complete;
        }
    }

    /// <summary>
    /// Gets the completed interaction.
    /// </summary>
    public Interaction<ToastMessage, Unit> CompletedInteraction { get; }

    /// <summary>
    /// Gets the failed interaction.
    /// </summary>
    public Interaction<ToastMessage, Unit> FailedInteraction { get; }

    /// <summary>
    /// Gets the add command.
    /// </summary>
    public RxCommand<ScheduledMedication?, AddMedicineTrigger> AddCommand { get; }

    /// <summary>
    /// Gets or sets the selected name.
    /// </summary>
    public MedicationId? SelectedName
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
    public TimeSpan? SelectedTime
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
    /// Gets or sets the names.
    /// </summary>
    public ObservableCollection<MedicationId> Names
    {
        get => _names;
        set => RaiseAndSetIfChanged(ref _names, value);
    }

    /// <summary>
    /// Gets the recurrences.
    /// </summary>
    public List<Recurrence> Recurrences { get; } = Enum.GetValues<Recurrence>().ToList();

    /// <summary>
    /// Gets the current state of the machine.
    /// </summary>
    public AddMedicineState CurrentState => _currentState.Value;

    /// <summary>
    /// Gets the back command.
    /// </summary>
    public RxCommand<Unit, NavigationState> BackCommand { get; }

    /// <summary>
    /// Gets the valid scheduled medication.
    /// </summary>
    public ScheduledMedication Medication => _medication.Value;

    /// <inheritdoc/>
    protected override async Task Initialize(ICqrs cqrs)
    {
        try
        {
            await _stateMachine.FireAsync(AddMedicineTrigger.Load);

            var result = await cqrs.Query(LoadMedication.Create());

            Names = new ObservableCollection<MedicationId>(result.Medications.Select(medication => medication.Id));
            Dosages = new ObservableCollection<Dosage>(result.Medications.Dosages);

            await _stateMachine.FireAsync(AddMedicineTrigger.Load);
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, InitializationException.MessageTemplate);
            await _stateMachine.FireAsync(AddMedicineTrigger.Failure);
        }
    }

    private Task<NavigationState> ExecuteBack() => Navigator.Dismiss();

    private void ConfigureMachine()
    {
        _stateMachine
           .Configure(AddMedicineState.Initial)
           .Permit(AddMedicineTrigger.Load, AddMedicineState.Busy)
           .Permit(AddMedicineTrigger.Failure, AddMedicineState.Failed)
           .OnEntry(LogEntry);

        _stateMachine
           .Configure(AddMedicineState.Busy)
           .Permit(AddMedicineTrigger.Load, AddMedicineState.Loaded)
           .Permit(AddMedicineTrigger.Failure, AddMedicineState.Failed)
           .OnEntry(LogEntry);

        _stateMachine
           .Configure(AddMedicineState.Loaded)
           .Permit(AddMedicineTrigger.Failure, AddMedicineState.Failed)
           .Permit(AddMedicineTrigger.Validated, AddMedicineState.Valid)
           .OnEntry(LogEntry);

        _stateMachine
           .Configure(AddMedicineState.Valid)
           .OnEntry(LogEntry);

        _stateMachine
           .Configure(AddMedicineState.Failed)
           .PermitReentry(AddMedicineTrigger.Failure)
           .OnEntry(LogEntry)
           .OnEntry(
                transition =>
                {
                    using var failed = FailedInteraction.Handle(new ToastMessage($"Trigger Failure: {transition}")).Subscribe();
                });

        _stateMachine.Configure(AddMedicineState.Completed)
           .OnEntry(LogEntry)
           .OnEntryAsync(
                async _ =>
                {
                    // using var completed = CompletedInteraction.Handle(new ToastMessage("The medication has been saved")).Subscribe();
                    await ExecuteBack();
                })
           .OnEntry(LogEntry);

        void LogEntry(StateMachine<AddMedicineState, AddMedicineTrigger>.Transition transition)
            => Logger.LogDebug("State Machine Transition: {@Transition}", transition);
    }

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private readonly AddMedicineStateMachine _stateMachine;

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private readonly IValueBinder<AddMedicineState> _currentState;

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private readonly IValueBinder<ScheduledMedication> _medication;

    private MedicationId? _selectedName;
    private Dosage? _selectedDosage;
    private Recurrence? _selectedRecurrence;
    private TimeSpan? _selectedTime;
    private ObservableCollection<Dosage> _dosages = [];
    private ObservableCollection<MedicationId> _names = [];
}