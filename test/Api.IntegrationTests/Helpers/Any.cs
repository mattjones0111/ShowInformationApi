using System;
using System.Text;

namespace Api.IntegrationTests.Helpers
{
    internal static class Any
    {
        private static readonly Random Rnd = new Random();

        public static string String(int length = 12)
        {
            const char offset = 'a';
            const int lettersOffset = 26;

            StringBuilder builder = new StringBuilder(length);

            for (var i = 0; i < length; i++)
            {
                var @char = (char)Rnd.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return builder.ToString();
        }

        public static int PositiveInt32(int max = int.MaxValue)
        {
            return Rnd.Next(1, max);
        }

        public static DateTime Date(DateTime min, DateTime max)
        {
            int days = (int)(max - min).TotalDays;

            return min.AddDays(PositiveInt32(days));
        }
    }
}
