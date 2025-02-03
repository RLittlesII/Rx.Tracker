using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using Microsoft.Extensions.Logging;
using ReactiveMarbles.Command;
using ReactiveMarbles.Extensions;
using ReactiveMarbles.Mvvm;
using ReactiveMarbles.PropertyChanged;
using ReactiveUI;
using Rx.Tracker.Exceptions;
using Rx.Tracker.Extensions;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Features.Schedule.Domain.Queries;
using Rx.Tracker.Mediation;
using Rx.Tracker.Navigation;
using Stateless;
using static Rx.Tracker.Extensions.DynamicDataExtensions;

namespace Rx.Tracker.Features.Schedule.ViewModels;

/// <inheritdoc />
public class ScheduleViewModel : ViewModelBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScheduleViewModel"/> class.
    /// </summary>
    /// <param name="navigator">The navigator.</param>
    /// <param name="cqrs">The cqrs mediator.</param>
    /// <param name="coreServices">The core services.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    /// <param name="stateMachineFactory">The state machine factory.</param>
    public ScheduleViewModel(
        INavigator navigator,
        ICqrs cqrs,
        ICoreServices coreServices,
        ILoggerFactory loggerFactory,
        Func<ScheduleStateMachine> stateMachineFactory)
        : base(navigator, cqrs, loggerFactory)
    {
        AddMedicineCommand = RxCommand.Create(ExecuteAddMedicine);
        _stateMachine = stateMachineFactory.Invoke().DisposeWith(Garbage);
        _currentState =
            _stateMachine
               .Current
               .AsValue(_ => { }, _ => RaisePropertyChanged(nameof(CurrentState)), () => ScheduleStateMachine.ScheduleState.Initial)
               .DisposeWith(Garbage);

        var today = coreServices.Clock.GetCurrentInstant().ToDateTimeOffset().ToLocalDate();

        var medicationScheduleChanged =
            this.WhenChanged(viewModel => viewModel.MedicationSchedule)
               .WhereIsNotNull()
               .LogTrace(Logger, schedule => schedule!.ScheduleId, "Medication Schedule: {ScheduleId}")
               .SelectMany(schedule => schedule!.DisposeWith(Garbage).Connect().LogTrace(Logger, "Ref"))
               .LogTrace(Logger, "Preparing to Filter")
               .Filter(medication => medication.ScheduledTime.Date == today)
               .RefCount();

        medicationScheduleChanged
           .Group(group => group.ScheduledTime)
           .Transform(x => new DaySchedule(x))
           .Bind(out _schedule, options: EagerBindingOptions)
           .Subscribe(_ => { }, exception => Logger.LogError(exception, string.Empty))
           .DisposeWith(Garbage);

        medicationScheduleChanged
           .LogTrace(Logger, "Filtered")
           .Bind(out _scheduledMedications, options: EagerBindingOptions)
           .Subscribe(_ => { }, exception => Logger.LogError(exception, string.Empty))
           .DisposeWith(Garbage);

        // medicationScheduleChanged
        //    .CountChanged()
        //    .LogTrace(Logger, x => x.Count, "Current Count: {@Count}")
        //    .Subscribe()
        //    .DisposeWith(Garbage);
        NavigatedTo
           .Skip(1)
           .Select(_ => Initialize(Mediator))
           .Subscribe();

        ConfigureMachine(_stateMachine);
    }

    private Task ExecuteAddMedicine() => _stateMachine.FireAsync(ScheduleStateMachine.ScheduleTrigger.Add);

    /// <summary>
    /// Gets the add medicine command.
    /// </summary>
    public RxCommand<Unit, Unit> AddMedicineCommand { get; }

    /// <summary>
    /// Gets the current state of the machine.
    /// </summary>
    public ScheduleStateMachine.ScheduleState CurrentState => _currentState.Value;

    /// <summary>
    /// Gets scheduled medications.
    /// </summary>
    public ReadOnlyObservableCollection<DaySchedule> Schedule => _schedule;

    /// <summary>
    /// Gets today's schedule.
    /// </summary>
    public ReadOnlyObservableCollection<ScheduledMedication> ScheduledMedications => _scheduledMedications;

    /// <summary>
    /// Gets the medication schedule.
    /// </summary>
    public MedicationSchedule? MedicationSchedule
    {
        get => _medicationSchedule;
        private set => RaiseAndSetIfChanged(ref _medicationSchedule, value);
    }

    /// <inheritdoc/>
    protected override async Task Initialize(ICqrs cqrs)
    {
        try
        {
            await _stateMachine.FireAsync(ScheduleStateMachine.ScheduleTrigger.Load);

            var result = await cqrs.Query(LoadSchedule.Create(new UserId("Id"), default));
            MedicationSchedule = result.Schedule;
            await _stateMachine.FireAsync(ScheduleStateMachine.ScheduleTrigger.Load);
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, InitializationException.MessageTemplate);
            await _stateMachine.FireAsync(ScheduleStateMachine.ScheduleTrigger.Failure);
        }
    }

    private void ConfigureMachine(ScheduleStateMachine stateMachine)
    {
        stateMachine
           .Configure(ScheduleStateMachine.ScheduleState.Initial)
           .Permit(ScheduleStateMachine.ScheduleTrigger.Load, ScheduleStateMachine.ScheduleState.Busy)
           .Permit(ScheduleStateMachine.ScheduleTrigger.Failure, ScheduleStateMachine.ScheduleState.Failed)
           .OnEntry(LogEntry);

        stateMachine
           .Configure(ScheduleStateMachine.ScheduleState.Busy)
           .Permit(ScheduleStateMachine.ScheduleTrigger.Load, ScheduleStateMachine.ScheduleState.DaySchedule)
           .Permit(ScheduleStateMachine.ScheduleTrigger.Failure, ScheduleStateMachine.ScheduleState.Failed)
           .OnEntry(LogEntry);

        stateMachine
           .Configure(ScheduleStateMachine.ScheduleState.DaySchedule)
           .Permit(ScheduleStateMachine.ScheduleTrigger.Load, ScheduleStateMachine.ScheduleState.Busy)
           .Permit(ScheduleStateMachine.ScheduleTrigger.Failure, ScheduleStateMachine.ScheduleState.Failed)
           .InternalTransitionAsync(ScheduleStateMachine.ScheduleTrigger.Add, _ => Navigator.Modal<Routes>(routes => routes.AddMedicine))
           .OnEntry(LogEntry);

        void LogEntry(StateMachine<ScheduleStateMachine.ScheduleState, ScheduleStateMachine.ScheduleTrigger>.Transition transition)
            => Logger.LogDebug("State Machine Transition: {@Transition}", transition);
    }

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private readonly ScheduleStateMachine _stateMachine;

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private readonly IValueBinder<ScheduleStateMachine.ScheduleState> _currentState;

    private readonly ReadOnlyObservableCollection<DaySchedule> _schedule;

    private readonly ReadOnlyObservableCollection<ScheduledMedication> _scheduledMedications;

    private readonly BindingOptions _eagerBinding = new(1);

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private MedicationSchedule? _medicationSchedule;
}