using System;
using Api.Models;

namespace Api.IntegrationTests.Data
{
    internal static class Build
    {
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

        public static Show InvalidShow()
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
    }
}