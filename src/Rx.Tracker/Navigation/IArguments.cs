using System.Collections.Generic;

namespace Rx.Tracker.Navigation;

/// <summary>
/// Represents basic non-typed arguments.
/// </summary>
public interface IArguments : IEnumerable<KeyValuePair<string, object>>
{
    /// <summary>
    /// Adds the specified key and value to the parameter collection.
    /// </summary>
    /// <param name="key">The key of the parameter to add.</param>
    /// <param name="value">The value of the parameter to add.</param>
    void Add(string key, object value);

    /// <summary>
    /// Determines whether the <see cref="IArguments"/> contains the specified <paramref name="key"/>.
    /// </summary>
    /// <param name="key">The key to search the parameters for existence.</param>
    /// <returns>true if the <see cref="IArguments"/> contains a parameter with the specified key; otherwise, false.</returns>
    bool ContainsKey(string key);

    /// <summary>
    /// Gets the number of parameters contained in the <see cref="IArguments"/>.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Gets a collection containing the keys in the <see cref="IArguments"/>.
    /// </summary>
    IEnumerable<string> Keys { get; }

    /// <summary>
    /// Gets the parameter associated with the specified <paramref name="key"/>.
    /// </summary>
    /// <typeparam name="T">The type of the parameter to get.</typeparam>
    /// <param name="key">The key of the parameter to find.</param>
    /// <returns>A matching value of <typeparamref name="T"/> if it exists.</returns>
    T GetValue<T>(string key);

    /// <summary>
    /// Gets the parameter associated with the specified <paramref name="key"/>.
    /// </summary>
    /// <typeparam name="T">The type of the parameter to get.</typeparam>
    /// <param name="key">The key of the parameter to find.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of all the values referenced by key.</returns>
    IEnumerable<T> GetValues<T>(string key);

    /// <summary>
    /// Gets the parameter associated with the specified <paramref name="key"/>.
    /// </summary>
    /// <typeparam name="T">The type of the parameter to get.</typeparam>
    /// <param name="key">The key of the parameter to get.</param>
    /// <param name="value">
    /// When this method returns, contains the parameter associated with the specified key,
    /// if the key is found; otherwise, the default value for the type of the value parameter.
    /// </param>
    /// <returns>true if the <see cref="IArguments"/> contains a parameter with the specified key; otherwise, false.</returns>
    bool TryGetValue<T>(string key, out T? value);

    /// <summary>
    /// Gets the parameter associated with the specified key (legacy).
    /// </summary>
    /// <param name="key">The key of the parameter to get.</param>
    /// <returns>A matching value if it exists.</returns>
    object this[string key] { get; }
}