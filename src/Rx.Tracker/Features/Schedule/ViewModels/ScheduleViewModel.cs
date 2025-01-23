using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using Microsoft.Extensions.Logging;
using ReactiveMarbles.Command;
using ReactiveMarbles.Extensions;
using ReactiveMarbles.Mvvm;
using ReactiveMarbles.PropertyChanged;
using Rx.Tracker.Exceptions;
using Rx.Tracker.Extensions;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Features.Schedule.Domain.Queries;
using Rx.Tracker.Mediation;
using Rx.Tracker.Navigation;
using Stateless;
using static Rx.Tracker.Features.Schedule.ViewModels.ScheduleStateMachine;

namespace Rx.Tracker.Features.Schedule.ViewModels;

/// <inheritdoc />
public class ScheduleViewModel : ViewModelBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScheduleViewModel"/> class.
    /// </summary>
    /// <param name="navigator">The navigator.</param>
    /// <param name="cqrs">The cqrs mediator.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    /// <param name="stateMachineFactory">The state machine factory.</param>
    public ScheduleViewModel(INavigator navigator, ICqrs cqrs, ILoggerFactory loggerFactory, Func<ScheduleStateMachine> stateMachineFactory)
        : base(navigator, cqrs, loggerFactory)
    {
        AddMedicineCommand = RxCommand.Create(ExecuteAddMedicine);
        _stateMachine = stateMachineFactory.Invoke().DisposeWith(Garbage);
        _currentState =
            _stateMachine
               .Current
               .AsValue(_ => { }, _ => RaisePropertyChanged(nameof(CurrentState)), () => ScheduleState.Initial)
               .DisposeWith(Garbage);

        this.WhenChanged(viewModel => viewModel.MedicationSchedule)
           .WhereIsNotNull()
           .LogTrace(Logger, schedule => schedule!.ScheduleId, "Medication Schedule: {ScheduleId}")
           .SelectMany(schedule => schedule!.Connect().LogTrace(Logger, "Ref"))
           .LogTrace(Logger, "Preparing to Filter")
           .Filter(scheduledMedication => scheduledMedication.ScheduledTime.Date == DateTimeOffset.UtcNow.ToLocalDate())
           .Bind(out _schedule)
           .Subscribe()
           .DisposeWith(Garbage);

        ConfigureMachine(_stateMachine);
    }

    private Task ExecuteAddMedicine() => _stateMachine.FireAsync(ScheduleTrigger.Add);

    /// <summary>
    /// Gets the add medicine command.
    /// </summary>
    public RxCommand<Unit, Unit> AddMedicineCommand { get; }

    /// <summary>
    /// Gets the current state of the machine.
    /// </summary>
    public ScheduleState CurrentState => _currentState.Value;

    /// <summary>
    /// Gets scheduled medications.
    /// </summary>
    public ReadOnlyObservableCollection<ScheduledMedication> Schedule => _schedule;

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
            await _stateMachine.FireAsync(ScheduleTrigger.Load);

            var result = await cqrs.Query(LoadSchedule.Create(new UserId("Id"), default));
            MedicationSchedule = result.Schedule;
            await _stateMachine.FireAsync(ScheduleTrigger.Load);
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, InitializationException.MessageTemplate);
            await _stateMachine.FireAsync(ScheduleTrigger.Failure);
        }
    }

    private void ConfigureMachine(ScheduleStateMachine stateMachine)
    {
        stateMachine
           .Configure(ScheduleState.Initial)
           .Permit(ScheduleTrigger.Load, ScheduleState.Busy)
           .Permit(ScheduleTrigger.Failure, ScheduleState.Failed)
           .OnEntry(LogEntry);

        stateMachine
           .Configure(ScheduleState.Busy)
           .Permit(ScheduleTrigger.Load, ScheduleState.DaySchedule)
           .Permit(ScheduleTrigger.Failure, ScheduleState.Failed)
           .OnEntry(LogEntry);

        stateMachine
           .Configure(ScheduleState.DaySchedule)
           .Permit(ScheduleTrigger.Failure, ScheduleState.Failed)
           .InternalTransitionAsync(ScheduleTrigger.Add, _ => Navigator.Modal<Routes>(routes => routes.AddMedicine))
           .OnEntry(LogEntry);

        void LogEntry(StateMachine<ScheduleState, ScheduleTrigger>.Transition transition)
            => Logger.LogDebug("State Machine Transition: {@Transition}", transition);
    }

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private readonly ScheduleStateMachine _stateMachine;

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private readonly IValueBinder<ScheduleState> _currentState;

    private readonly ReadOnlyObservableCollection<ScheduledMedication> _schedule;

    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "DisposeWith")]
    private MedicationSchedule? _medicationSchedule;
}