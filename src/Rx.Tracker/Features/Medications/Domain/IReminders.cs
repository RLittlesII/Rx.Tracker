using System.Collections.Generic;
using System.Threading.Tasks;
using Rx.Tracker.Features.Medications.Domain.Entities;

namespace Rx.Tracker.Features.Medications.Domain;

/// <summary>
/// Interface that provides access to the reminders data storage.
/// </summary>
public interface IReminders
{
    /// <summary>
    /// Creates the medication reminder.
    /// </summary>
    /// <param name="reminder">The reminder.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task Create(MedicationReminder reminder);

    /// <summary>
    /// Reads the medication reminder with the provided <see cref="Id"/>.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    Task<MedicationReminder> Read(Id id);

    /// <summary>
    /// Reads all medication reminders.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    Task<IEnumerable<MedicationReminder>> Read();

    /// <summary>
    /// Updates the provided medication reminder.
    /// </summary>
    /// <param name="reminder">The reminder.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task Update(MedicationReminder reminder);

    /// <summary>
    /// Deletes the medication reminder with the provided <see cref="Id"/>.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task Delete(Id id);

    /// <summary>
    /// Deletes the medication reminder with the provided <see cref="Id"/>.
    /// </summary>
    /// <param name="reminder">The reminder.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task Delete(MedicationReminder reminder);
}