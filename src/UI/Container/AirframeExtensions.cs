using System;
using System.Collections.Generic;
using DryIoc;
using Microsoft.Maui.Hosting;
using ReactiveUI;
using Rocket.Surgery.Airframe.Exceptions;

namespace Rx.Tracker.UI.Container;

public static class AirframeExtensions
{
    public static MauiAppBuilder UseAirframe(this MauiAppBuilder builder, IContainer container, Action<IContainer> registrar)
    {
        registrar.Invoke(container);
        container.RegisterDelegate<IExceptionHandler>(
            resolverContext => new GlobalExceptionHandler(RxApp.MainThreadScheduler, resolverContext.Resolve<IEnumerable<IUnhandledExceptionHandler>>()),
            reuse: Reuse.Singleton);
        return builder;
    }

    public static MauiAppBuilder UseAirframe(this MauiAppBuilder builder, IContainer containerExtension, Action<AirframeBuilder> buildAirframe)
    {
        buildAirframe.Invoke(new AirframeBuilder(containerExtension, builder));
        return builder;
    }
}