using System;
using System.Reactive.Concurrency;
using DryIoc;
using Microsoft.Maui.Hosting;
using ReactiveUI;
using Rocket.Surgery.Airframe.Exceptions;

namespace Rx.Tracker.UI.Container;

public class AirframeBuilder
{
    private readonly IContainer _container;

    internal AirframeBuilder(IContainer container, MauiAppBuilder builder)
    {
        _container = container;
        ArgumentNullException.ThrowIfNull(container);
        ArgumentNullException.ThrowIfNull(builder);
        MauiBuilder = builder;
    }

    /// <summary>
    /// Gets the associated <see cref="MauiAppBuilder"/>.
    /// </summary>
    public MauiAppBuilder MauiBuilder { get; }

    public AirframeBuilder AddGlobalExceptionHandler<TGlobalExceptionHandler>()
        where TGlobalExceptionHandler : IObserver<Exception> => this;

    public AirframeBuilder AddGlobalExceptionHandler<TGlobalExceptionHandler>(Action<ExceptionHandlerRegistrar> register)
        where TGlobalExceptionHandler : IObserver<Exception>
    {
        _container.Register<IObserver<Exception>, TGlobalExceptionHandler>(Reuse.Singleton);
        register.Invoke(new ExceptionHandlerRegistrar(_container));
        _container.RegisterInstance<IScheduler>(RxApp.MainThreadScheduler, ifAlreadyRegistered: IfAlreadyRegistered.Replace);
        return this;
    }

    public class ExceptionHandlerRegistrar
    {
        public ExceptionHandlerRegistrar(IContainer container) => _container = container;

        public ExceptionHandlerRegistrar AddHandler<THandler>(Action<IRegistrator> register)
            where THandler : IUnhandledExceptionHandler
        {
            register.Invoke(_container);
            return this;
        }

        public ExceptionHandlerRegistrar AddHandler<THandler>()
            where THandler : IUnhandledExceptionHandler => AddHandler<THandler>(
            registrar => registrar.Register<IUnhandledExceptionHandler, THandler>(
                Reuse.Transient,
                ifAlreadyRegistered: IfAlreadyRegistered.AppendNewImplementation));

        private readonly IContainer _container;
    }
}