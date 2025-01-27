using System.Collections.Generic;
using System.Threading.Tasks;
using Rx.Tracker.Features.Medications.Domain;
using Rx.Tracker.Features.Medications.Domain.Entities;

namespace Rx.Tracker.Features.Medications.Data.Api;

/// <summary>
/// Represents an api client to retrieve <see cref="Medication"/>.
/// </summary>
public class MedicineApiClient : IMedicineApiClient
{
    /// <inheritdoc/>
    public Task<IReadOnlyCollection<Medication>> Get() => Task.FromResult<IReadOnlyCollection<Medication>>(
    [
        new Medication(
            new MedicationId("Ibuprofen"),
            [Dosage.Milligrams(100), Dosage.Milligrams(300), Dosage.Milligrams(500)]),
        new Medication(
            new MedicationId("Advil"),
            [Dosage.Milligrams(100), Dosage.Milligrams(300), Dosage.Milligrams(500)]),
        new Medication(
            new MedicationId("Alieve"),
            [Dosage.Milligrams(100), Dosage.Milligrams(300), Dosage.Milligrams(500)]),
    ]);
}