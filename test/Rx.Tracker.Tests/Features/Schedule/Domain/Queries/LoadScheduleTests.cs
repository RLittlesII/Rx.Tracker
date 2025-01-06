using FluentAssertions;
using NodaTime;
using NSubstitute;
using Rx.Tracker.Features.Schedule.Data.Api;
using Rx.Tracker.Features.Schedule.Data.Dto;
using Rx.Tracker.Features.Schedule.Domain;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Rx.Tracker.Features.Schedule.Domain.Queries.LoadSchedule;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Queries;

public class LoadScheduleTests
{
    [Fact]
    public async Task GivenQueryHandler_WhenHandle_TheShouldNotThrow()
    {
        // Given
        QueryHandler sut = new LoadScheduleQueryHandlerFixture();

        // When
        var result = await Record.ExceptionAsync(async () => await sut.Handle(Create(new UserId(), new LocalDate())));

        // Then
        result
           .Should()
           .BeNull();
    }

    [Fact]
    public async Task GivenQueryHandler_WhenHandle_ThenResultCorrectType()
    {
        // Given
        var api = Substitute.For<IMedicationScheduleApiClient>();
        api.Get(Arg.Any<Query>()).Returns(Task.FromResult<IReadOnlyCollection<ScheduledMedication>>([]));
        QueryHandler sut = new LoadScheduleQueryHandlerFixture();

        // When
        var result = await sut.Handle(Create(new UserId(), new LocalDate()));

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
        QueryHandler sut = new LoadScheduleQueryHandlerFixture();

        // When
        var result = await sut.Handle(Create(new UserId(), new LocalDate()));

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
}