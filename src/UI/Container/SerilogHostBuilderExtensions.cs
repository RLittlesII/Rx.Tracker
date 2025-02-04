using DryIoc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Hosting;
using Serilog.Hosting;

namespace Rx.Tracker.UI.Container;

public static class SerilogHostBuilderExtensions
{
    public static MauiAppBuilder UseSerilog(this MauiAppBuilder builder, IContainer container, Serilog.ILogger logger)
    {
        container
           .RegisterInstance(logger);

        // container
        //    .RegisterDelegate<ILoggerFactory>(_ => new SerilogLoggerFactory(logger), ifAlreadyRegistered: IfAlreadyRegistered.Replace);
        builder
           .Services
           .AddSingleton<ILoggerFactory>(_ => new SerilogLoggerFactory(logger));

        return builder;
    }
}