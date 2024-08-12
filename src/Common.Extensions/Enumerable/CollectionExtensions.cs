namespace Common.Extensions.Enumerable;

public static class CollectionExtensions
{
    public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
    {
        foreach (var item in range)
        {
            collection.Add(item);
        }

        return collection;
    }
}