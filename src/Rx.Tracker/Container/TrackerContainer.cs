using DryIoc;

namespace Rx.Tracker.Container;

/// <summary>
/// Contains the application <see cref="IContainer" />.
/// </summary>
public static class TrackerContainer
{
    /// <summary>
    /// Gets the <see cref="Rules" /> for the <see cref="IContainer" />.
    /// </summary>
    public static Rules Rules => GenerateRules();

    private static Rules GenerateRules()
    {
        var @default = Rules.Default.WithAutoConcreteTypeResolution()
           .WithoutThrowOnRegisteringDisposableTransient()
           .With(Made.Of(FactoryMethod.ConstructorWithResolvableArguments));
        return @default;
    }
}