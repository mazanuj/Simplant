using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simplant
{
    internal static class Range
    {
        /// <summary>
        /// Parallel queries
        /// </summary>
        /// <param name="_start">Start position</param>
        /// <param name="_stop">Stop position</param>
        /// <param name="_diapazon">Range of queries per process</param>
        internal static IEnumerable<string> Start(long _start, long _stop, int _diapazon)
        {
            var start = _start;
            var stop = _stop;
            var diapazon = _diapazon;
            var obj = new Object();
            var list = new List<string>();

            Parallel.For(start, stop,
                i =>
                {
                    lock (obj)
                    {
                        var newStart = start;
                        start = newStart + diapazon > stop ? stop : newStart + diapazon;

                        if (start != stop)
                            Task.Factory.StartNew(() => list.AddRange(MainClassJoin.Start(newStart, start)));
                    }
                });
            return list;
        }
    }
}