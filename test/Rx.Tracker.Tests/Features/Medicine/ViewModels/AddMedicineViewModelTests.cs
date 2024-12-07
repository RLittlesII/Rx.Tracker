using FluentAssertions;
using NSubstitute;
using Rx.Tracker.Features.Medications.Domain.Commands;
using Rx.Tracker.Features.Medications.Domain.Entities;
using Rx.Tracker.Features.Medications.Domain.Queries;
using Rx.Tracker.Features.Medications.ViewModels;
using Rx.Tracker.Mediation;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Medicine.ViewModels;

public class AddMedicineViewModelTests
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
           .Be(AddMedicineStateMachine.AddMedicineState.Initial);
    }

    [Fact]
    public async Task GivenCqrs_WhenInitialize_ThenShouldHaveMedicine()
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadMedication.Query>()).Returns(Task.FromResult(LoadMedication.Create([new MedicationFixture()])));
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(cqrs);

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.Medicine.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GivenCqrs_WhenInitialize_ThenShouldHaveDosages()
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Query(Arg.Any<LoadMedication.Query>()).Returns(Task.FromResult(LoadMedication.Create([new MedicationFixture()])));
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(cqrs);

        // When
        await sut.InitializeCommand.Execute(Unit.Default);

        // Then
        sut.Dosages.Should().NotBeEmpty();
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
           .Be(AddMedicineStateMachine.AddMedicineState.Loaded);
    }

    [Fact]
    public void GivenValid_WhenCanCommandExecute_ThenCanExecute()
    {
        var result = false;
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Execute(Arg.Any<AddMedicationToSchedule.Command>()).Returns(Task.CompletedTask);
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(cqrs);

        // When
        using (sut.AddCommand.CanCommandExecute.Subscribe(canExecute => result = canExecute))
        {
            sut.SelectedDosage = new Dosage();
            sut.SelectedName = "Ibuprofen";
            sut.SelectedRecurrence = Recurrence.TwiceDaily;
            sut.SelectedTime = DateTimeOffset.UtcNow;
        }

        // Then
        result.Should().BeTrue();
    }

    [Fact]
    public void GivenNotValid_WhenCanCommandExecute_ThenCanNotExecute()
    {
        // Given
        var result = false;
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Execute(Arg.Any<AddMedicationToSchedule.Command>()).Returns(Task.CompletedTask);
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(cqrs);

        // When
        using (sut.AddCommand.CanCommandExecute.Subscribe(canExecute => result = canExecute))
        {
            // Then
            result.Should().BeFalse();
        }
    }

    [Fact]
    public async Task GivenNull_WhenAdd_ThenThrows()
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Execute(Arg.Any<AddMedicationToSchedule.Command>()).Returns(Task.CompletedTask);
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(cqrs);

        // When
        var result = await Record.ExceptionAsync(async () => await sut.AddCommand.Execute(null));

        // Then
        result
           .Should()
           .BeOfType<ArgumentNullException>();
    }

    [Fact]
    public async Task GivenScheduledMedication_WhenAdd_ThenCompletes()
    {
        // Given
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Execute(Arg.Any<AddMedicationToSchedule.Command>()).Returns(Task.CompletedTask);
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(cqrs);

        // When
        var result = await sut.AddCommand.Execute(new ScheduledMedicationFixture());

        // Then
        result
           .Should()
           .BeOfType<Unit>();
    }
}