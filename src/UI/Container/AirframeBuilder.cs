using DryIoc;
using Microsoft.Maui.Hosting;
using ReactiveUI;
using Rocket.Surgery.Airframe.Exceptions;
using System;
using System.Reactive.Concurrency;

namespace Rx.Tracker.UI.Container;

public class AirframeBuilder
{
    public AirframeBuilder AddGlobalExceptionHandler<TGlobalExceptionHandler>(Action<ExceptionHandlerRegistrar> register)
        where TGlobalExceptionHandler : IObserver<Exception>
    {
        _container.Register<IObserver<Exception>, TGlobalExceptionHandler>(Reuse.Singleton);
        register.Invoke(new ExceptionHandlerRegistrar(_container));
        _container.RegisterInstance<IScheduler>(RxApp.MainThreadScheduler, IfAlreadyRegistered.Replace);
        return this;
    }

    /// <summary>
    /// Gets the associated <see cref="MauiAppBuilder" />.
    /// </summary>
    public MauiAppBuilder MauiBuilder { get; }

    public class ExceptionHandlerRegistrar
    {
        public ExceptionHandlerRegistrar AddHandler<THandler>()
            where THandler : IUnhandledExceptionHandler => AddHandler<THandler>(
            registrar => registrar.Register<IUnhandledExceptionHandler, THandler>(
                Reuse.Transient,
                ifAlreadyRegistered: IfAlreadyRegistered.AppendNewImplementation));

        public ExceptionHandlerRegistrar(IContainer container) => _container = container;

        private ExceptionHandlerRegistrar AddHandler<THandler>(Action<IRegistrator> register)
            where THandler : IUnhandledExceptionHandler
        {
            register.Invoke(_container);
            return this;
        }

        private readonly IContainer _container;
    }

    internal AirframeBuilder(IContainer container, MauiAppBuilder builder)
    {
        _container = container;
        ArgumentNullException.ThrowIfNull(container);
        ArgumentNullException.ThrowIfNull(builder);
        MauiBuilder = builder;
    }

    private readonly IContainer _container;
}