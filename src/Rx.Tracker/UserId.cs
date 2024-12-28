namespace Rx.Tracker;

/// <inheritdoc />
public record UserId : Id
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserId"/> class.
    /// </summary>
    public UserId()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserId"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public UserId(string value)
        : base(value)
    {
    }

    public static implicit operator UserId(string value) => new(value);
}