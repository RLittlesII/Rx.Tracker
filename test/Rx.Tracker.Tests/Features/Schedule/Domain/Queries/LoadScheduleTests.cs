using FluentAssertions;
using Rx.Tracker.Features.Schedule.Domain.Queries;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Queries;

public class LoadScheduleTests
{
    [Fact]
    public void GivenQueryHandler_WhenHandle_ThenResultCorrectType()
    {
        // Given
        LoadSchedule.QueryHandler sut = new LoadScheduleQueryHandlerFixture();

        // When
        var result = sut.Handle(LoadSchedule.Create(new UserId()));

        // Then
        result
           .Should()
           .NotBeNull();
    }

    [Fact]
    public async Task GivenQueryHandler_WhenHandle_TheShouldNotThrow()
    {
        // Given
        LoadSchedule.QueryHandler sut = new LoadScheduleQueryHandlerFixture();

        // When
        var result = await Record.ExceptionAsync(async () => await sut.Handle(LoadSchedule.Create(new UserId())));

        // Then
        result
           .Should()
           .BeNull();
    }
}