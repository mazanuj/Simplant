using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Simplant
{
    internal static class Program
    {
        private static void Main()
        {
            var validList = new List<string>(Range.Start(0x7D466EEE56D4D000, 0x7D466EEE56D4E000, 500));

            using (var sw = new StreamWriter(DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss-ffff") + ".txt", true))
            {
                Parallel.ForEach(validList, (value) =>
                {
                    sw.WriteLine(value);
                });

                sw.Flush();
                sw.Close();
            }

            Console.ReadKey();
        }
    }
}