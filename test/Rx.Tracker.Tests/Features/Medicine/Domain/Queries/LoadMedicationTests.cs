using FluentAssertions;
using NSubstitute;
using Rx.Tracker.Features.Medications.Domain;
using Rx.Tracker.Features.Medications.Domain.Queries;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Medicine.Domain.Queries;

public class LoadMedicationTests
{
    [Fact]
    public async Task Given_WhenHandle_Then()
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
    public async Task Given_WhenHandle_ThenShouldCallGet()
    {
        // Given
        var client = Substitute.For<IMedicineApiClient>();
        LoadMedication.QueryHandler sut = new LoadMedicationQueryHandlerFixture().WithClient(client);

        // When
        await sut.Handle(LoadMedication.Create());

        // Then
        await client.Received(1).Get();
    }

    [Fact]
    public async Task Given_WhenHandle_ThenResultsNotEmpty()
    {
        // Given
        var client = Substitute.For<IMedicineApiClient>();
        client.Get().Returns([new MedicationFixture()]);
        LoadMedication.QueryHandler sut = new LoadMedicationQueryHandlerFixture().WithClient(client);

        // When
        var result = await sut.Handle(LoadMedication.Create());

        // Then
        result.Medicines.Should().NotBeNullOrEmpty();
    }
}