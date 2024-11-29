using LanguageExt;
using NanoidDotNet;

namespace Rx.Tracker.Core;

/// <inheritdoc />
public record Id : Identity<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Id"/> class.
    /// </summary>
    public Id()
        : base(Generate())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Id"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public Id(string value)
        : base(value)
    {
    }

    private static string Generate() => Nanoid.Generate(size: 8);
}