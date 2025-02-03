using System.Threading.Tasks;
using Rx.Tracker.Features.Schedule.Domain;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Mediation.Commands;
using System.Reactive;
using System.Threading;

namespace Rx.Tracker.Features.Medications.Domain.Commands;

/// <summary>
/// The add medicine command definition.
/// </summary>
public static class AddMedicationToSchedule
{
    /// <summary>
    /// Add medicine command.
    /// </summary>
    /// <param name="ScheduledMedication">The scheduled medication.</param>
    public record Command(UserId User, ScheduledMedication ScheduledMedication) : ICommand;

    /// <summary>
    /// The add medicine command handler.
    /// </summary>
    public class CommandHandler : CommandHandlerBase<Command>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler"/> class.
        /// </summary>
        /// <param name="reminders">The reminders.</param>
        /// <param name="client">The client.</param>
        public CommandHandler(IReminders reminders, IMedicationScheduleApiClient client)
        {
            _reminders = reminders;
            _client = client;
        }

        /// <inheritdoc />
        protected override async Task<Unit> Handle(Command command, CancellationToken cancellationToken = default) =>

            // TODO: [rlittlesii: November 29, 2024] Save to persisted storage
            // TODO: [rlittlesii: November 29, 2024] Save to calendars, or are calendars behind the persisted storage?!
            await _client.Add(command).ContinueWith(_ => Unit.Default, cancellationToken);

        private readonly IReminders _reminders;
        private readonly IMedicationScheduleApiClient _client;
    }

    /// <summary>
    /// Creates a <see cref="Command"/>.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="medication">The medication.</param>
    /// <returns>The command.</returns>
    public static Command Create(UserId userId, ScheduledMedication medication) => new(userId, medication);
}