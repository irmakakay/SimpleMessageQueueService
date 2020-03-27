namespace MessageQueue.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static List<T> ToFlattenedList<T>(this IEnumerable<IEnumerable<T>> collection)
        {
            return collection.Where(_ => !_.IsNullOrEmpty()).SelectMany(_ => _).Where(_ => _ != null).ToList();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) =>
            collection == null || !collection.Any();

        public static IEnumerable<IReadOnlyCollection<T>> Partition<T>(this IEnumerable<T> source, int size)
        {
            if (size < 1) throw new InvalidOperationException("Partition size cannot be less than 1.");

            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var currentPage = new List<T>(size)
                    {
                        enumerator.Current
                    };

                    while (currentPage.Count < size && enumerator.MoveNext())
                    {
                        currentPage.Add(enumerator.Current);
                    }
                    yield return new ReadOnlyCollection<T>(currentPage);
                }
            }
        }
    }
}
