using DryIoc;
using FluentAssertions;
using NSubstitute;
using Rx.Tracker.Container;
using Rx.Tracker.Features.Schedule.Container;
using Rx.Tracker.Features.Schedule.ViewModels;
using Rx.Tracker.Navigation;
using Rx.Tracker.Tests.Container;

namespace Rx.Tracker.Tests.Features.Schedule.ViewModels;

public partial class ScheduleViewModelTests
{
    [Fact]
    public void GivenScheduleModule_WhenResolve_ThenShouldBeScheduleViewModel() =>
        // Given, When, Then
        new ContainerFixture()
           .WithRegistration(
                bootstrap =>
                {
                    bootstrap.Register<ScheduleViewModel>();
                    bootstrap.RegisterInstance(Substitute.For<INavigator>());
                    bootstrap.RegisterModule<ScheduleModule>();
                }
            )
           .AsInterface()
           .Resolve<ScheduleViewModel>()
           .Should()
           .NotBeNull()
           .And
           .Subject
           .Should()
           .BeOfType<ScheduleViewModel>();
}