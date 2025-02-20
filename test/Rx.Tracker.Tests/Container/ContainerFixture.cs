using DryIoc;
using FluentAssertions.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using ReactiveMarbles.Mvvm;
using Rocket.Surgery.Extensions.Testing;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Rx.Tracker.Container;
using Rx.Tracker.Mediation;
using Rx.Tracker.Tests.Features;
using System;

namespace Rx.Tracker.Tests.Container;

public sealed class ContainerFixture : ITestFixtureBuilder
{
    public static implicit operator DryIoc.Container(ContainerFixture fixture) => fixture.Build();

    public ContainerFixture WithRegistration(Action<IContainer> bootstrap) => this.With(ref _bootstrap, bootstrap);
    public ContainerFixture WithMocks() => this.With(ref _useContainerMocks, true);
    public IContainer AsInterface() => Build();

    private DryIoc.Container Build()
    {
        IContainer container = new DryIoc.Container(TrackerContainer.Rules);
        if (_useContainerMocks)
        {
            container = container
               .With(
                    rules => rules.WithCaptureContainerDisposeStackTrace()
                       .WithUndefinedTestDependenciesResolver(request => Substitute.For([request.ServiceType], []))
                       .WithConcreteTypeDynamicRegistrations((_, _) => true, Reuse.Transient)
                );
        }

        _bootstrap(container);

        container.RegisterMany([typeof(IMediator).GetAssembly()], Registrator.Interfaces);
        container.RegisterMany([typeof(ICqrs).GetAssembly()], Registrator.Interfaces, ifAlreadyRegistered: IfAlreadyRegistered.Replace);

        container.RegisterInstance(Substitute.For<IClock>());
        container.RegisterInstance(Substitute.For<ICoreRegistration>());
        container.RegisterInstance(CoreServicesStub.Instance());
        container.Register<ILoggerFactory, NullLoggerFactory>();

        return (DryIoc.Container)container;
    }

    private Action<IContainer> _bootstrap = _ => { };
    private bool _useContainerMocks;
}