using FluentAssertions;
using NodaTime.Extensions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Rx.Tracker.Features.Medications.Domain.Entities;
using Rx.Tracker.Features.Medications.Domain.Queries;
using Rx.Tracker.Features.Medications.ViewModels;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Mediation;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using static Rx.Tracker.Features.Medications.ViewModels.AddMedicineStateMachine;

namespace Rx.Tracker.Tests.Features.Medicine.ViewModels;

public partial class AddMedicineViewModelTests
{
    [Fact]
    public void WhenConstructed_ThenShouldBeInitialState()
    {
        // Given
        AddMedicineViewModel sut = new AddMedicineViewModelFixture();

        // When, Then
        sut
           .CurrentState
           .Should()
           .Be(AddMedicineState.Initial);
    }

    [Fact]
    public async Task GivenCqrs_WhenInitialize_ThenShouldBeInLoaded()
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadMedication.Query>()).Returns(Task.FromResult(LoadMedication.Create([new MedicationFixture()])));
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(cqrs);

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut
           .CurrentState
           .Should()
           .Be(AddMedicineState.Loaded);
    }

    [Fact]
    public async Task GivenQueryReturnsNull_WhenInitialize_ThenShouldBeInFailed()
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadMedication.Query>()).Returns(Task.FromResult<LoadMedication.Result>(null!));
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(cqrs);
        using var _ = sut.FailedInteraction.RegisterHandler(interaction => interaction.SetOutput(Unit.Default));

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut
           .CurrentState
           .Should()
           .Be(AddMedicineState.Failed);
    }

    [Fact]
    public async Task GivenQueryThrows_WhenInitialize_ThenShouldBeInFailed()
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadMedication.Query>()).ThrowsAsync(new Exception());
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(cqrs);

        // When
        _ = await Record.ExceptionAsync(async () => await sut.InitializeCommand.Execute(Unit.Default));

        // Then
        sut
           .CurrentState
           .Should()
           .Be(AddMedicineState.Failed);
    }

    [Fact]
    public async Task GivenSelectedPropertiesValid_WhenPropertiesChanged_ThenShouldBeValidState()
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadMedication.Query>()).Returns(Task.FromResult(LoadMedication.Create([new MedicationFixture()])));
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(cqrs);

        // When
        await sut.InitializeCommand.Execute(Unit.Default);
        sut.SelectedName = "Name";
        sut.SelectedDosage =  new DosageFixture();
        sut.SelectedRecurrence = Recurrence.Daily;
        sut.SelectedTime = DateTimeOffset.Now.ToOffsetDateTime();

        // Then
        sut
           .CurrentState
           .Should()
           .Be(AddMedicineState.Valid);
    }
}