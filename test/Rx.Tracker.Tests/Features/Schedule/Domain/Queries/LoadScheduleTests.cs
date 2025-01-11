using DryIoc;
using FluentAssertions;
using NodaTime;
using NSubstitute;
using Rx.Tracker.Features.Schedule.Domain;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Mediation;
using Rx.Tracker.Tests.Container;
using Rx.Tracker.Tests.Features.Schedule.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using static Rx.Tracker.Features.Schedule.Domain.Queries.LoadSchedule;
using Arg = NSubstitute.Arg;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Queries;

public class LoadScheduleTests
{
    [Fact]
    public async Task GivenQueryHandler_WhenHandle_TheShouldNotThrow()
    {
        // Given
        var sut = new LoadScheduleQueryHandlerFixture().AsInterface();

        // When
        var result = await Record.ExceptionAsync(async () => await sut.Handle(Create(new UserId(), new LocalDate()), CancellationToken.None));

        // Then
        result
           .Should()
           .BeNull();
    }

    [Fact]
    public async Task GivenQueryHandler_WhenHandle_ThenResultCorrectType()
    {
        // Given
        var client = Substitute.For<IMedicationScheduleApiClient>();
        client.Get(Arg.Any<Query>()).Returns(Task.FromResult<MedicationSchedule>(new MedicationScheduleFixture().WithEnumerable([])));
        var sut = new LoadScheduleQueryHandlerFixture().WithClient(client).AsInterface();

        // When
        var result = await sut.Handle(Create(new UserId(), new LocalDate()), CancellationToken.None);

        // Then
        result
           .Should()
           .NotBeNull()
           .And
           .Subject
           .Should()
           .BeOfType<Result>();
    }

    [Fact]
    public async Task GivenQueryHandler_WhenHandle_ThenResultHasMedicationSchedule()
    {
        // Given
        var client = Substitute.For<IMedicationScheduleApiClient>();
        client.Get(Arg.Any<Query>()).Returns(Task.FromResult<MedicationSchedule>(new MedicationScheduleFixture().WithEnumerable([])));
        var sut = new LoadScheduleQueryHandlerFixture().WithClient(client).AsInterface();

        // When
        var result = await sut.Handle(Create(new UserId(), new LocalDate()), CancellationToken.None);

        // Then
        result
           .Schedule
           .Should()
           .NotBeNull()
           .And
           .Subject
           .Should()
           .BeOfType<MedicationSchedule>();
    }

    [Fact]
    public async Task GivenContainer_WhenCqrsQuery_ThenReturnsResultType()
    {
        // Given, When
        var result = await new ContainerFixture().AsInterface().Resolve<ICqrs>().Query(Create(new UserId(), new LocalDate()));

        // Then
        result
           .Should()
           .BeOfType<Result>();
    }
}