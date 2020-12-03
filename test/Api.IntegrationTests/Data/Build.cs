using System;
using System.Collections.Generic;
using System.Linq;
using Api.IntegrationTests.Helpers;
using Api.Models;

namespace Api.IntegrationTests.Data
{
    internal static class Build
    {
        public static IEnumerable<Show> RandomShows(int count)
        {
            return Enumerable.Range(1, count)
                .Select(c => new Show
                {
                    Id = Any.PositiveInt32(),
                    Name = Any.String(),
                    Cast = Enumerable.Range(1, 5)
                        .Select(x => CastMember())
                        .ToArray()
                });
        }

        public static CastMember CastMember()
        {
            return new CastMember
            {
                Id = Any.PositiveInt32(),
                Name = Any.String(),
                Birthday = Any.Date(new DateTime(1970,1,1), new DateTime(2010, 1, 1))
            };
        }

        public static Show Show()
        {
            return new Show
            {
                Id = 1,
                Name = "The Jim Smith Show",
                Cast = new[] {new CastMember
                {
                    Id = 2,
                    Name = "Jim Smith",
                    Birthday = new DateTime(1977, 2, 12)
                }}
            };
        }

        public static Show InvalidShow_NoName()
        {
            return new Show
            {
                Id = 3,
                Name = string.Empty,
                Cast = new[] {new CastMember
                {
                    Id = 4,
                    Name = "Jim Smith",
                    Birthday = new DateTime(1977, 2, 12)
                }}
            };
        }

        public static Show InvalidShow_NoCastMemberName()
        {
            return new Show
            {
                Id = 3,
                Name = "The Jim Smith Show",
                Cast = new[] {new CastMember
                {
                    Id = 4,
                    Name = null,
                    Birthday = new DateTime(1977, 2, 12)
                }}
            };
        }
    }
}