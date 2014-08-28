using System;
using System.Threading.Tasks;

namespace Simplant
{
    internal static class Range
    {
        internal static void Start(long _start, long _stop, int _diapazon)
        {
            var start = _start;
            var stop = _stop;
            var diapazon = _diapazon;
            var obj = new Object();

            Parallel.For(start, stop,
                i =>
                {
                    lock (obj)
                    {
                        var newStart = start;
                        start = newStart + diapazon > stop ? stop : newStart + diapazon;

                        if (start != stop)
                            Task.Factory.StartNew(() => MainClassJoin.Start(newStart, start));
                    }
                });
        }
    }
}