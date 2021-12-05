namespace Aoc.Solutions;
public static class Fn
{
    public static void Dump<T>(this T @this, bool indented = true) =>
        Console.WriteLine(@this.ToJson(indented));

    public static void DumpDict<K, V>(this Dictionary<K, V> @this, bool indented = true) =>
        Console.WriteLine(@this.Select(kvp => $"[{kvp.Key}]: {kvp.Value}").Join("\n"));
    public static string ToJson<T>(this T @this, bool indented = true) =>
        System.Text.Json.JsonSerializer.Serialize(@this, options: new() { WriteIndented = indented });
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

public class DefaultDict<TKey, TValue> : Dictionary<TKey, TValue> where TValue : new()
{
    public new TValue this[TKey key]
    {
        get
        {
            TValue val;
            if (!TryGetValue(key, out val))
            {
                val = new TValue();
                Add(key, val);
            }
            return val;
        }
        set { base[key] = value; }
    }
}