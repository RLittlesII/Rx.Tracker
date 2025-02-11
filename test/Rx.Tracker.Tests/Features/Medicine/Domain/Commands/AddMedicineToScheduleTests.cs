using DryIoc;
using FluentAssertions;
using Rx.Tracker.Features;
using Rx.Tracker.Features.Medications.Domain.Commands;
using Rx.Tracker.Mediation;
using Rx.Tracker.Mediation.Commands;
using Rx.Tracker.Tests.Container;
using Rx.Tracker.Tests.Features.Schedule.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Medicine.Domain.Commands;

public class AddMedicationToScheduleTests
{
    [Fact]
    public async Task GivenCommandHandler_WhenHandle_ThenDoesNotThrow()
    {
        // Given
        AddMedicationToSchedule.CommandHandler sut = new AddMedicationToScheduleCommandHandlerFixture();

        // When
        var result = await Record.ExceptionAsync(() => sut.As<ICommandHandler<AddMedicationToSchedule.Command>>().Handle(AddMedicationToSchedule.Create(new UserId(), new ScheduledMedicationFixture()), CancellationToken.None));

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
        var result = await Record.ExceptionAsync(() => container.Resolve<ICqrs>().Execute(AddMedicationToSchedule.Create(new UserId(), new ScheduledMedicationFixture())));

        // Then
        result
           .Should()
           .BeNull();
    }
    [Fact]
    public async Task GivenContainer_WhenCqrsQuery_ThenReturnsResultType() =>
        // Given, When, Then
        await new ContainerFixture().WithMocks().AsInterface().Resolve<ICqrs>().Execute(AddMedicationToSchedule.Create(new UserId(), new ScheduledMedicationFixture()));
}