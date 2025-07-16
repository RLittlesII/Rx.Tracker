namespace Rx.Tracker.Features;

/// <inheritdoc />
public record UserId : Id
{
    public static implicit operator UserId(string value) => new(value);

    /// <summary>
    /// Initializes a new instance of the <see cref="UserId" /> class.
    /// </summary>
    public UserId()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserId" /> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public UserId(string value)
        : base(value)
    {
    }
}