using FluentAssertions;
using NodaTime.Extensions;
using NSubstitute;
using Rx.Tracker.Features.Medications.Domain.Commands;
using Rx.Tracker.Features.Medications.Domain.Queries;
using Rx.Tracker.Features.Medications.ViewModels;
using Rx.Tracker.Features.Schedule.Domain.Entities;
using Rx.Tracker.Mediation;
using Rx.Tracker.Tests.Features.Medicine.Domain.Entities;
using Rx.Tracker.Tests.Features.Schedule.Domain.Entities;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using static Rx.Tracker.Features.Medications.ViewModels.AddMedicineStateMachine;

namespace Rx.Tracker.Tests.Features.Medicine.ViewModels;

public partial class AddMedicineViewModelTests
{
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
    public async Task GivenValid_WhenCanCommandExecute_ThenCanExecute()
    {
        var result = false;
        var cqrs = Substitute.For<ICqrs>();
        cqrs.Execute(Arg.Any<AddMedicationToSchedule.Command>()).Returns(Task.CompletedTask);
        cqrs.Query(Arg.Any<LoadMedication.Query>()).Returns(Task.FromResult(LoadMedication.Create([new MedicationFixture()])));
        AddMedicineViewModel sut = new AddMedicineViewModelFixture().WithCqrs(cqrs);

        await sut.InitializeCommand.Execute(Unit.Default);

        // When
        using (sut.AddCommand.CanCommandExecute.Subscribe(canExecute => result = canExecute))
        {
            sut.SelectedDosage = new DosageFixture();
            sut.SelectedName = "Ibuprofen";
            sut.SelectedRecurrence = Recurrence.TwiceDaily;
            sut.SelectedTime = TimeSpan.Zero;
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
           .Be(AddMedicineTrigger.Complete);
    }
}