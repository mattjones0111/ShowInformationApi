using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.IntegrationTests.Helpers
{
    public static class EnumerableExtensions
    {
        public static bool AreInDescendingOrder<T>(
            this IEnumerable<T> items,
            IComparer<T> comparer)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            T[] itemsArray = items.ToArray();

            if (itemsArray.Length < 2)
            {
                return true;
            }

            for (int i = 0; i < itemsArray.Length - 1; i++)
            {
                int compare = comparer.Compare(itemsArray[i], itemsArray[i + 1]);

                if (compare < 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}