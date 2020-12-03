using System.Collections.Generic;
using Api.Models;

namespace Api.IntegrationTests.Helpers
{
    public class CastMemberBirthdayComparer : IComparer<CastMember>
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

            return x.Birthday.CompareTo(y.Birthday);
        }
    }
}