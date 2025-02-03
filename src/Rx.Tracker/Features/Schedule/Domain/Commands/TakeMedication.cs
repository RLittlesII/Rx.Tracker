using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Rx.Tracker.Features.Medications.Domain;
using Rx.Tracker.Features.Medications.Domain.Entities;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Mediation.Commands;
using System.Reactive;
using System.Threading;

namespace Rx.Tracker.Features.Schedule.Domain.Commands;

/// <summary>
/// The take medicine command definition.
/// </summary>
public static class TakeMedication
{
    /// <summary>
    /// Take medicine command.
    /// </summary>
    /// <param name="Medication">The medication.</param>
    public record Command(Medication Medication) : ICommand;

    /// <summary>
    /// The take medicine command handler.
    /// </summary>
    public class CommandHandler : CommandHandlerBase<Command>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler"/> class.
        /// </summary>
        /// <param name="reminders">The reminders.</param>
        public CommandHandler(IReminders reminders) => _reminders = reminders;

        /// <inheritdoc />
        protected override Task<Unit> Handle(Command command, CancellationToken cancellationToken = default) =>

            // TODO: [rlittlesii: November 29, 2024] Save to persisted storage
            // TODO: [rlittlesii: November 29, 2024] Save to calendars, or are calendars behind the persisted storage?!
            Task.FromResult(Unit.Default);

        private readonly IReminders _reminders;
    }

    /// <summary>
    /// Creates a <see cref="Command"/>.
    /// </summary>
    /// <param name="medication">The medication.</param>
    /// <returns>The command.</returns>
    public static Command Create(ScheduledMedication medication) => new(medication.Medication);
}