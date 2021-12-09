namespace Aoc.Solutions;
public static class Fn
{
    public static void Dump<T>(this T @this, bool indented = true) =>
        Console.WriteLine(@this.ToJson(indented));

    public static T Clone<T>(this T @this) =>
        System.Text.Json.JsonSerializer.Deserialize<T>(@this.ToJson());

    public static void DumpDict<K, V>(this Dictionary<K, V> @this, bool indented = true) =>
        Console.WriteLine(@this.Select(kvp => $"[{kvp.Key}]: {kvp.Value}").Join("\n"));

    public static string ToJson<T>(this T @this, bool indented = true) =>
        System.Text.Json.JsonSerializer.Serialize(@this, options: new() { WriteIndented = indented });

    public static IEnumerable<(T Val, int Idx)> Indexed<T>(this IEnumerable<T> @this) => @this.Select((v, i) => (v, i));

    public static IEnumerable<List<T>> SplitAt<T>(this IEnumerable<T> @this, Func<T, bool> split)
    {
        var cur = new List<T>();
        foreach (var l in @this)
        {
            if (split(l))
            {
                yield return cur;
                cur = new();
            }
            else
            {
                cur.Add(l);
            }
        }
        if (cur.Any())
        {
            yield return cur;
        }
    }
}

public class DefaultDict<K, V> : Dictionary<K, V>
{
    private readonly Func<V> _default = () => default;
    public DefaultDict()
    { }

    /// <summary>
    /// Provide a function to generate a default value when not present
    /// </summary>
    /// <param name="defaultValueGenerator">e.g. () => new List()</param>
    public DefaultDict(Func<V> defaultValueGenerator)
    {
        _default = defaultValueGenerator;
    }
    public new V this[K key]
    {
        get
        {
            if (!TryGetValue(key, out V val))
            {
                val = _default();
                Add(key, val);
            }
            return val;
        }
        set { base[key] = value; }
    }
}