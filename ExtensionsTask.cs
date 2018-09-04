using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public static class ExtensionsTask
    {
        /// <summary>
        /// Медиана списка из нечетного количества элементов — 
        ///         это серединный элемент списка после сортировки.
        /// Медиана списка из четного количества элементов — 
        ///         среднее арифметическое двух серединных элементов списка после сортировки.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Если последовательность не содержит элементов</exception>
        public static double Median(this IEnumerable<double> items)
        {
            var v = items.ToList();
            var n = v.Count;
            if (0 == n) throw new InvalidOperationException();
            v.Sort();
            return (0 == n % 2) ? v.Skip(n / 2 - 1).Take(2).Average() : v[n / 2];
        }

        /// <returns>
        /// Возвращает последовательность, состоящую из пар соседних элементов.
        /// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
        /// </returns>
        public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
        {
            var it = items.GetEnumerator();
            if (!it.MoveNext()) yield break;
            for (; ; )
            {
                var prev = it.Current;
                if (!it.MoveNext()) yield break;
                yield return new Tuple<T, T>(prev, it.Current);
            }
        }
    }
}

