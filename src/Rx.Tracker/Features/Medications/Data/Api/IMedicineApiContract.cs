using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Rx.Tracker.Features.Medicine.Data.Dto;

namespace Rx.Tracker.Features.Medicine.Data.Api;

/// <summary>
/// Interface representing the <see cref="MedicineDto"/> api contract.
/// </summary>
public interface IMedicineApiContract
{
    /// <summary>
    /// Get a set of <see cref="MedicineDto"/>.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Get("/medicines")]
    Task<IEnumerable<MedicineDto>> Get();
}