using FluentAssertions;
using NSubstitute;
using Rx.Tracker.Features.Schedule.Domain.Queries;
using Rx.Tracker.Features.Schedule.ViewModels;
using Rx.Tracker.Mediation;
using Rx.Tracker.Tests.Features.Schedule.Domain.Entities;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Schedule.ViewModels;

public partial class ScheduleViewModelTests
{
    [Fact]
    public void WhenConstructed_ThenCurrentStateShouldBeInitial()
    {
        // Given
        ScheduleViewModel sut = new ScheduleViewModelFixture();

        // When, Then
        sut.CurrentState.Should().Be(ScheduleStateMachine.ScheduleState.Initial);
    }

    [Fact]
    public async Task GivenNoResult_WhenInitialized_ThenCurrentStateShouldBeFailed()
    {
        // Given
        ScheduleViewModel sut = new ScheduleViewModelFixture();

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.CurrentState.Should().Be(ScheduleStateMachine.ScheduleState.Failed);
    }

    [Fact]
    public async Task GivenLoadScheduleResult_WhenInitialized_ThenCurrentStateShouldBeDaySchedule()
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadSchedule.Query>()).Returns(Task.FromResult(new LoadSchedule.Result(new MedicationScheduleFixture().WithEnumerable([]))));
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithCqrs(cqrs);

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.CurrentState.Should().Be(ScheduleStateMachine.ScheduleState.DaySchedule);
    }

    [Fact]
    public async Task GivenDayScheduleState_WhenAddMedication_ThenShouldReturnUnit()
    {
        // Given
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithStateMachineFactory(
            () => new ScheduleStateMachineFixture().WithInitialState(
                ScheduleStateMachine.ScheduleState.DaySchedule
            )
        );

        // When
        var result = await sut.AddMedicineCommand.Execute(Unit.Default);

        // Then
        result.Should().Be(Unit.Default);
    }

    [Fact]
    public async Task GivenDayScheduleState_WhenAddMedication_ThenShouldBeInDayScheduleState()
    {
        // Given
        ScheduleViewModel sut = new ScheduleViewModelFixture().WithStateMachineFactory(
            () => new ScheduleStateMachineFixture().WithInitialState(ScheduleStateMachine.ScheduleState.DaySchedule)
        );

        // When
        await sut.AddMedicineCommand.Execute(Unit.Default);

        // Then
        sut.CurrentState.Should().Be(ScheduleStateMachine.ScheduleState.DaySchedule);
    }
}