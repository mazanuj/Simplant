using System;
using System.Threading.Tasks;

namespace Simplant
{
    internal static class Program
    {
        private static void Main()        
        {
            //MainClassJoin.Start(0x7D466EEE56D40200, 0x7D466EEE56D40300);

            //var t1 = new Task(() => MainClass.Start(0x7D46, 0x6EEE, 0x56D4, 0x0000, 0x7D46, 0x6EEE, 0x56D4, 0x5000));
            //var t2 = new Task(() => MainClass.Start(0x7D46, 0x6EEE, 0x56D4, 0x5000, 0x7D46, 0x6EEE, 0x56D5, 0x0000));
            //Task.Factory.StartNew(() => MainClassJoin.Start(0x7D466EEE56D40200, 0x7D466EEE56D40300));

            //t1.Start();
            //t2.Start();

            //var t3 = new Thread(t1.Start);
            //t3.Start();

            //var t4 = new Thread(t2.Start);
            //t4.Start();

            //Parallel.Invoke(
            //    () => MainClassJoin.Start(0x7D466EEE56D40200, 0x7D466EEE56D40300),
            //    () => MainClassJoin.Start(0x7D466EEE56D40300, 0x7D466EEE56D40400),
            //    () => MainClassJoin.Start(0x7D466EEE56D40400, 0x7D466EEE56D40500),
            //    () => MainClassJoin.Start(0x7D466EEE56D40500, 0x7D466EEE56D40600)
            //);

            Range.Start(0x7D466EEE56D40200, 0x7D466EEE56D40250, 30);

            Console.ReadKey();
        }
    }
}