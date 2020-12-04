using System;
using System.IO;
using System.Reflection;

namespace Api.UnitTests.Helpers
{
    internal static class EmbeddedResource
    {
        public static string GetString(string embeddedResourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly
                .GetManifestResourceStream(embeddedResourceName))
            {
                if (stream == null)
                {
                    throw new Exception($"Resource {embeddedResourceName} not found.");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}