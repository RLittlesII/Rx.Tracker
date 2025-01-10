using DryIoc;
using MediatR;
using NSubstitute;
using Rocket.Surgery.Extensions.Testing;
using Rocket.Surgery.Extensions.Testing.Fixtures;
using Rx.Tracker.Mediation;
using System;

namespace Rx.Tracker.Tests.Container;

public sealed class ContainerFixture : ITestFixtureBuilder
{
    public static implicit operator DryIoc.Container(ContainerFixture fixture) => fixture.Build();

    public ContainerFixture WithRegistration(Action<DryIoc.Container> bootstrap) => this.With(ref _bootstrap, bootstrap);
    public ContainerFixture WithMocks() => this.With(ref _useContainerMocks, true);
    public IContainer AsInterface() => Build();

    private DryIoc.Container Build()
    {
        var container = new DryIoc.Container(
            Rules.Default.WithAutoConcreteTypeResolution()
               .WithoutThrowOnRegisteringDisposableTransient()
               .With(Made.Of(FactoryMethod.ConstructorWithResolvableArguments))
            // .WithDefaultIfAlreadyRegistered(IfAlreadyRegistered.Replace)
        );
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

        container.RegisterMany([typeof(IMediator).GetAssembly(), typeof(ICqrs).GetAssembly()], Registrator.Interfaces);

        return container;
    }

    private Action<DryIoc.Container> _bootstrap = _ => { };
    private bool _useContainerMocks;
}