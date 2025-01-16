using DryIoc;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Rx.Tracker.Container;
using Rx.Tracker.Features.Medications.Container;
using Rx.Tracker.Features.Medications.ViewModels;
using Rx.Tracker.Navigation;
using Rx.Tracker.Tests.Container;

namespace Rx.Tracker.Tests.Features.Medicine.ViewModels;

public partial class AddMedicineViewModelTests
{
    [Fact]
    public void GivenAddMedicineModule_WhenResolve_ThenShouldBeAddMedicineViewModel() =>
        // Given, When, Then
        new ContainerFixture()
           .WithRegistration(bootstrap =>
                {
                    bootstrap.Register<AddMedicineViewModel>();
                    bootstrap.RegisterInstance(Substitute.For<ILoggerFactory>());
                    bootstrap.RegisterInstance(Substitute.For<INavigator>());
                    bootstrap.RegisterModule<AddMedicineModule>();
                }
            )
           .AsInterface()
           .Resolve<AddMedicineViewModel>()
           .Should()
           .NotBeNull()
           .And
           .Subject
           .Should()
           .BeOfType<AddMedicineViewModel>();
}