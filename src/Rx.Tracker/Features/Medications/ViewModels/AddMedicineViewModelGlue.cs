using Microsoft.Extensions.Logging;
using Rx.Tracker.Features.Medications.Domain;
using Rx.Tracker.Navigation;

namespace Rx.Tracker.Features.Medications.ViewModels;

/// <summary>
/// Represents a View Model holding everything together with Glue Code™.
/// </summary>
public class AddMedicineViewModelGlue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddMedicineViewModelGlue" /> class.
    /// </summary>
    /// <param name="navigator">The navigator.</param>
    /// <param name="client">The medicine api client.</param>
    /// <param name="reminders">The reminders.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    public AddMedicineViewModelGlue(
        INavigator navigator,
        IMedicineApiClient client,
        IReminders reminders,
        ILoggerFactory loggerFactory)
    {
    }
}