using FluentAssertions;
using System;
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
        var result = await Record.ExceptionAsync(async () => await sut.Handle(Create(new UserId(), DateTimeOffset.Now)));

        // Then
        result
           .Should()
           .BeNull();
    }

    [Fact]
    public async Task GivenQueryHandler_WhenHandle_ThenResultCorrectType()
    {
        // Given
        QueryHandler sut = new LoadScheduleQueryHandlerFixture();

        // When
        var result = await sut.Handle(Create(new UserId(), DateTimeOffset.Now));

        // Then
        result
           .Should()
           .NotBeNull()
           .And
           .Subject
           .Should()
           .BeOfType<Result>();
    }
}