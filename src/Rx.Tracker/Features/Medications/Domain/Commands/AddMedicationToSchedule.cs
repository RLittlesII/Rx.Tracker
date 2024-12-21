using System.Threading.Tasks;
using LanguageExt;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Mediation.Commands;

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
    public record Command(ScheduledMedication ScheduledMedication) : ICommand;

    /// <summary>
    /// The add medicine command handler.
    /// </summary>
    public class CommandHandler : ICommandHandler<Command>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler"/> class.
        /// </summary>
        /// <param name="reminders">The reminders.</param>
        public CommandHandler(IReminders reminders) => _reminders = reminders;

        /// <inheritdoc />
        public async Task<Unit> Handle(Command command) =>

            // TODO: [rlittlesii: November 29, 2024] Save to persisted storage
            // TODO: [rlittlesii: November 29, 2024] Save to calendars, or are calendars behind the persisted storage?!
            await _reminders.Create(command.ScheduledMedication.Id, command.ScheduledMedication.Medication, command.ScheduledMedication.ScheduledTime, command.ScheduledMedication.MealRequirement);

        private readonly IReminders _reminders;
    }

    /// <summary>
    /// Creates a <see cref="Command"/>.
    /// </summary>
    /// <param name="medication">The medication.</param>
    /// <returns>The command.</returns>
    public static Command Create(ScheduledMedication medication) => new(medication);
}