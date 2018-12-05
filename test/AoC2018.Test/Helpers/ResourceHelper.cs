using System;
using System.IO;

namespace Advent_of_Code_2018.Test.Helpers
{
    public static class ResourceHelper
    {
        public static string GetInputStringAocDay(Type type) => GetInputString($"{type.Name}.txt");

        public static string GetInputString(string inputFileName)
        {
            var assembly = typeof(Day1).Assembly;
            var resourceName = $"{nameof(Advent_of_Code_2018)}.Input.{inputFileName}";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
