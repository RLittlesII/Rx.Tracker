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
using System.Threading.Tasks;

namespace Rx.Tracker.Tests.Features.Medicine.ViewModels;

public class AddMedicineViewModelTests
{
    [Fact]
    public void Given_When_Then()
    {
        // Given
        AddMedicineViewModel sut = new AddMedicineViewModelFixture();

        // When

        // Then
    }

    [Fact]
    public void Given_WhenInitialize_ThenShouldLoadMedicine()
    {
        // Given
        var mediator = Substitute.For<ICqrs>();
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(mediator);

        // When
        using var _ = sut.InitializeCommand.Execute(Unit.Default).Subscribe();

        // Then
        mediator.Received(1).Query(LoadMedication.Create());
    }

    [Fact]
    public void Given_WhenInitialize_ThenShouldHaveMedicine()
    {
        // Given
        var mediator = Substitute.For<ICqrs>();
        mediator.Query(Arg.Any<LoadMedication.Query>()).Returns(Task.FromResult(LoadMedication.Create([new MedicationFixture()])));
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(mediator);

        // When
        using var _ = sut.InitializeCommand.Execute(Unit.Default).Subscribe();

        // Then
        sut.Medicine.Should().NotBeEmpty();
    }

    [Fact]
    public void Given_WhenInitialize_ThenShouldHaveDosages()
    {
        // Given
        var mediator = Substitute.For<ICqrs>();
        mediator.Query(Arg.Any<LoadMedication.Query>()).Returns(Task.FromResult(LoadMedication.Create([new MedicationFixture()])));
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(mediator);

        // When
        using var _ = sut.InitializeCommand.Execute(Unit.Default).Subscribe();

        // Then
        sut.Dosages.Should().NotBeEmpty();
    }

    [Fact]
    public void Given_WhenAddMedicine_ThenShouldAddMedicineToSchedule()
    {
        // Given
        var selected = new Medication();
        var mediator = Substitute.For<ICqrs>();
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(mediator);

        // When
        sut.SelectedDosage = new Dosage();
        sut.SelectedName = "Ibuprofen";
        sut.SelectedRecurrence = Recurrence.TwiceDaily;
        sut.SelectedTime = DateTimeOffset.UtcNow;

        // Then
        mediator.Received(1).Execute(Arg.Any<AddMedicationToSchedule.Command>());
    }

    [Fact]
    public void GivenSelected_WhenAddMedicine_ThenShouldAddMedicineCanExecute()
    {
        var result = false;
        var mediator = Substitute.For<ICqrs>();
        mediator.Execute(Arg.Any<AddMedicationToSchedule.Command>()).Returns(Task.CompletedTask);
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(mediator);

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
    public void GivenSelectedNull_WhenAddMedicine_ThenShouldAddMedicineCanNotExecute()
    {
        // Given
        var result = false;
        var mediator = Substitute.For<ICqrs>();
        mediator.Execute(Arg.Any<AddMedicationToSchedule.Command>()).Returns(Task.CompletedTask);
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(mediator);

        // When
        using var _ = sut.AddCommand.CanCommandExecute.Subscribe(canExecute => result = canExecute);

        // Then
        result.Should().BeFalse();
    }
}