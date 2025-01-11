using DryIoc;
using FluentAssertions;
using NSubstitute;
using Rx.Tracker.Features.Medications.Domain;
using Rx.Tracker.Features.Medications.Domain.Queries;
using Rx.Tracker.Mediation;
using Rx.Tracker.Tests.Container;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Medicine.Domain.Queries;

public class LoadMedicationTests
{
    [Fact]
    public async Task GivenQueryHandler_WhenHandle_ThenResultCorrectType()
    {
        // Given
        var sut = new LoadMedicationQueryHandlerFixture().AsInterface();

        // When
        var result = await sut.Handle(LoadMedication.Create(), CancellationToken.None);

        // Then
        result
           .Should()
           .BeOfType<LoadMedication.Result>();
    }

    [Fact]
    public async Task GivenQueryHandler_WhenHandle_ThenResultHasMedications()
    {
        // Given
        var client = Substitute.For<IMedicineApiClient>();
        client.Get().Returns([new MedicationFixture()]);
        var sut = new LoadMedicationQueryHandlerFixture().WithClient(client).AsInterface();

        // When
        var result = await sut.Handle(LoadMedication.Create(), CancellationToken.None);

        // Then
        result // NOTE: [rlittlesii: December 04, 2024] Dramatization.
           .Medications
           .Should()
           .NotBeNullOrEmpty()
           .And
           .Subject
           .Should()
           .ContainSingle();
    }

    [Fact]
    public async Task GivenContainer_WhenCqrsQuery_ThenReturnsResultType()
    {
        // Given, When
        var result = await new ContainerFixture().AsInterface().Resolve<ICqrs>().Query(LoadMedication.Create());

        // Then
        result
           .Should()
           .BeOfType<LoadMedication.Result>();
    }
}