using FluentAssertions;
using NSubstitute;
using Rx.Tracker.Features.Medications.Domain;
using Rx.Tracker.Features.Medications.Domain.Entities;
using Rx.Tracker.Features.Medications.Domain.Queries;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Medicine.Domain.Queries;

public class LoadMedicationTests
{
    [Fact]
    public async Task GivenQueryHandler_WhenHandle_ThenResultCorrectType()
    {
        // Given
        LoadMedication.QueryHandler sut = new LoadMedicationQueryHandlerFixture();

        // When
        var result = await sut.Handle(LoadMedication.Create());

        // Then
        result
           .Should()
           .BeOfType<LoadMedication.Result>();
    }

    [Fact]
    public async Task GivenQueryHandler_WhenHandle_ThenResultHasDosages()
    {
        // Given
        var client = Substitute.For<IMedicineApiClient>();
        client.Get().Returns([new MedicationFixture()]);
        LoadMedication.QueryHandler sut = new LoadMedicationQueryHandlerFixture().WithClient(client);

        // When
        var result = await sut.Handle(LoadMedication.Create());

        // Then
        result // NOTE: [rlittlesii: December 04, 2024] Dramatization.
           .Dosages
           .Should()
           .NotBeNullOrEmpty()
           .And
           .Subject
           .Should()
           .ContainSingle();
    }
}