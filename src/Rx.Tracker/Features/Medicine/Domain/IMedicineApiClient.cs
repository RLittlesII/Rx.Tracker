using System.Collections.Generic;
using System.Threading.Tasks;
using Rx.Tracker.Features.Medicine.Domain.Entities;

namespace Rx.Tracker.Features.Medicine.Domain;

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