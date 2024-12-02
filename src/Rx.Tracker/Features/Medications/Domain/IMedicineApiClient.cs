using System.Collections.Generic;
using System.Threading.Tasks;
using Rx.Tracker.Features.Medications.Domain.Entities;

namespace Rx.Tracker.Features.Medications.Domain;

/// <summary>
/// Interface representing a client for <see cref="Medication"/>.
/// </summary>
public interface IMedicineApiClient
{
    /// <summary>
    /// Get a collection of <see cref="Medication"/>.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task<IReadOnlyCollection<Medication>> Get();
}