using Rx.Tracker.Features.Medications.Domain.Queries;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Medicine.Domain.Queries;

public class LoadMedicationTests
{
    [Fact]
    public async Task Given_When_Then()
    {
        // Given
        LoadMedication.QueryHandler sut = new LoadMedicationQueryHandlerFixture();

        // When
        await sut.Handle(LoadMedication.Create());

        // Then
    }
}