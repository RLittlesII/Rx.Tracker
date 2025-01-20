using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace Rx.Tracker.UI.Navigation;

public record NavigatorArguments : IArguments
{
    public NavigatorArguments()
        : this(new NavigationParameters())
    {
    }

    public NavigatorArguments(INavigationParameters parameters) => _arguments.AddRange(parameters);

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => _arguments.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(string key, object value) => _arguments.Add(new KeyValuePair<string, object>(key, value));

    public bool ContainsKey(string key) => _arguments.Any(pair => pair.Key == key);

    public int Count => _arguments.Count;

    public IEnumerable<string> Keys => _arguments.Select(pair => pair.Key);

    public T GetValue<T>(string key) => (T)_arguments.First(pair => pair.Key == key).Value;

    public IEnumerable<T> GetValues<T>(string key) => _arguments.Select(pair => pair.Key == key).OfType<T>();

    public bool TryGetValue<T>(string key, out T? value)
    {
        if (ContainsKey(key))
        {
            value = GetValue<T>(key);
            return true;
        }

        value = default;
        return false;
    }

    public object this[string key]
    {
        get { return _arguments; }
    }

    private List<KeyValuePair<string, object>> _arguments = [];
}