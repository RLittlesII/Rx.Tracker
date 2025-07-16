using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;

namespace Rx.Tracker;

/// <summary>
/// A disposable object.
/// </summary>
public abstract class DisposableObject : IDisposable
{
    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    /// <param name="disposing">A value indicating whether the object is being disposed of.</param>
    protected virtual void Dispose(bool disposing)
    {
    }

    /// <summary>
    /// Gets the garbage.
    /// </summary>
    protected CompositeDisposable Garbage { get; } = new();

    private void DisposeManaged(bool disposing)
    {
        if (disposing)
        {
            Dispose(disposing);
            Garbage.Dispose();
        }
    }

    /// <inheritdoc />
    [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "IDispose")]
    void IDisposable.Dispose()
    {
        DisposeManaged(true);
        GC.SuppressFinalize(this);
    }
}