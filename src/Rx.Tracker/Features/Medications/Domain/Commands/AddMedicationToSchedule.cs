using DynamicData;
using System.Threading.Tasks;
using Rx.Tracker.Features.Medicine.Domain.Entities;
using Rx.Tracker.Mediation.Commands;

namespace Rx.Tracker.Features.Medicine.Domain.Commands;

/// <summary>
/// The add medicine command definition.
/// </summary>
public static class AddMedicineToSchedule
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
        private readonly IMedicineCache _cache;
        private readonly IReminders _reminders;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandHandler"/> class.
        /// </summary>
        /// <param name="cache">The medicine cache.</param>
        /// <param name="reminders">The reminders.</param>
        public CommandHandler(IMedicineCache cache, IReminders reminders)
        {
            _cache = cache;
            _reminders = reminders;
        }

        /// <inheritdoc />
        public async Task Handle(Command command)
        {
            // TODO: [rlittlesii: November 29, 2024] Save to persisted storage
            // TODO: [rlittlesii: November 29, 2024] Save to calendars, or are calendars behind the persisted storage?!
            _cache.AddOrUpdate(command.ScheduledMedication.Medication);
            await _reminders.Create(new MedicationReminder());
        }
    }

    /// <summary>
    /// Creates a <see cref="Command"/>.
    /// </summary>
    /// <param name="medication">The medication.</param>
    /// <returns>The command.</returns>
    public static Command Create(ScheduledMedication medication) => new(medication);
}