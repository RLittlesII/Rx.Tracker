using System;
using Microsoft.Extensions.Logging;
using Serilog.Debugging;
using Serilog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Rx.Tracker.UI.Logging;

// public class SerilogLoggerFactory : ILoggerFactory
// {
//     public SerilogLoggerFactory(Serilog.ILogger logger, bool dispose = false) => _provider = new SerilogLoggerProvider(logger, dispose);
//
//     public ILogger CreateLogger(string categoryName) => _provider.CreateLogger(categoryName);
//
//     public void AddProvider(ILoggerProvider provider) =>
//
//         // Only Serilog provider is allowed!
//         SelfLog.WriteLine("Ignoring added logger provider {0}", provider);
//
//     public void Dispose()
//     {
//         Dispose(true);
//         GC.SuppressFinalize(this);
//     }
//
//     protected virtual void Dispose(bool disposing)
//     {
//         if (disposing)
//         {
//             _provider.Dispose();
//         }
//     }
//
//     private readonly SerilogLoggerProvider _provider;
// }