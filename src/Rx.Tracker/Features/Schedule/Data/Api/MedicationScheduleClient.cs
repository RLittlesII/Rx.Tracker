using System.Linq;
using System.Threading.Tasks;
using LanguageExt;
using Rx.Tracker.Features.Medications.Domain.Commands;
using Rx.Tracker.Features.Schedule.Domain;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Features.Schedule.Domain.Queries;
using Rx.Tracker.Features.Schedule.Mapping;

namespace Rx.Tracker.Features.Schedule.Data.Api;

/// <inheritdoc />
public class MedicationScheduleClient : IMedicationScheduleApiClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MedicationScheduleClient"/> class.
    /// </summary>
    /// <param name="apiContract">The api contract.</param>
    public MedicationScheduleClient(IMedicationScheduleApiContract apiContract) => _apiContract = apiContract;

    /// <inheritdoc/>
    public Task Add(AddMedicationToSchedule.Command command) => _apiContract.Create(command.User, command);

    /// <inheritdoc/>
    public Task<MedicationSchedule> Get(LoadSchedule.Query query) =>
        _apiContract
           .Read(query.User, query)
           .Select(scheduledMedicine => scheduledMedicine.Select(medicine => ScheduleMapper.Map(medicine)))
           .Map(scheduledMedication => new MedicationSchedule(scheduledMedication, query.Date));

    private readonly IMedicationScheduleApiContract _apiContract;
}