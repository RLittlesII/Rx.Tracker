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
        /// <inheritdoc />
        public Task Handle(Command command) => Task.CompletedTask;
    }

    /// <summary>
    /// Creates a <see cref="Command"/>.
    /// </summary>
    /// <param name="medication">The medication.</param>
    /// <returns>The command.</returns>
    public static Command Create(ScheduledMedication medication) => new(medication);
}