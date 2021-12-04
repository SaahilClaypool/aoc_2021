namespace Aoc.Solutions
{
    public static class Fn
    {
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
}