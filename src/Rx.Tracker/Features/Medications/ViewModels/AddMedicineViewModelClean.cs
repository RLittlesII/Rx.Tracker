using Microsoft.Extensions.Logging;
using Rx.Tracker.Features.Medications.Domain.Commands;
using Rx.Tracker.Features.Medications.Domain.Queries;
using Rx.Tracker.Navigation;

namespace Rx.Tracker.Features.Medications.ViewModels;

/// <summary>
/// Represents a View Model with Clean Architecture.
/// </summary>
public class AddMedicineViewModelClean
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddMedicineViewModelClean"/> class.
    /// </summary>
    /// <param name="navigator">The navigator.</param>
    /// <param name="loadMedication">The load medication handler.</param>
    /// <param name="addMedication">The add medication handler.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    public AddMedicineViewModelClean(
        INavigator navigator,
        LoadMedication.QueryHandler loadMedication,
        AddMedicationToSchedule.CommandHandler addMedication,
        ILoggerFactory loggerFactory)
    {
    }
}