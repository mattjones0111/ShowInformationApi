using System;
using System.Collections.Generic;
using Api.Models;

namespace Api.IntegrationTests.Helpers
{
    /// <summary>
    ///    A comparer that compares <see cref="CastMember"></see> instances
    ///    by their Birthday property.
    /// </summary>
    /// <remarks>
    ///    If either of the Birthday properties have no value, then they are
    ///    compared alphabetically by name.
    /// </remarks>
    public sealed class CastMemberBirthdayComparer : IComparer<CastMember>
    {
        public int Compare(CastMember x, CastMember y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            if (ReferenceEquals(null, y))
            {
                return 1;
            }

            if (ReferenceEquals(null, x))
            {
                return -1;
            }

            if (x.Birthday.HasValue && y.Birthday.HasValue)
            {
                return x.Birthday.Value.CompareTo(y.Birthday.Value);
            }

            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
}
