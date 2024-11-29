using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace Rx.Tracker.Features.Medicine.Data;

public interface IMedicineApiContract
{
    /// <summary>
    /// Get a set of <see cref="MedicineDto"/>.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    [Get("/medicines")]
    Task<IEnumerable<MedicineDto>> Get();
}