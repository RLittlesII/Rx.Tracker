using DryIoc;
using FluentAssertions;
using Rx.Tracker.Features.Schedule.Domain.Commands;
using Rx.Tracker.Mediation;
using Rx.Tracker.Mediation.Commands;
using Rx.Tracker.Tests.Container;
using Rx.Tracker.Tests.Features.Schedule.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Schedule.Domain.Commands;

public class TakeMedicationTests
{
    [Fact]
    public async Task GivenCommandHandler_WhenHandle_ThenDoesNotThrow()
    {
        // Given
        TakeMedication.CommandHandler sut = new TakeMedicationCommandHandlerFixture();

        // When
        var result = await Record.ExceptionAsync(
            () => sut.As<ICommandHandler<TakeMedication.Command>>().Handle(TakeMedication.Create(new ScheduledMedicationFixture()), CancellationToken.None)
        );

        // Then
        result
           .Should()
           .BeNull();
    }

    [Fact]
    public async Task GivenCqrs_WhenExecute_ThenDoesNotThrow()
    {
        // Given
        var container = new ContainerFixture().WithMocks().AsInterface();

        // When
        var result = await Record.ExceptionAsync(() => container.Resolve<ICqrs>().Execute(TakeMedication.Create(new ScheduledMedicationFixture())));

        // Then
        result
           .Should()
           .BeNull();
    }
}