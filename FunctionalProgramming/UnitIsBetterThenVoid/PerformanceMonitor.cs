using System;
using System.Diagnostics;
using Unit = System.ValueTuple;
namespace FunctionalProgramming.UnitIsBetterThenVoid
{
    public class PerformanceMonitor : IDisposable
    {
        private readonly Stopwatch _stopWatch;

        private PerformanceMonitor()
        {
            _stopWatch = new Stopwatch();
            _stopWatch.Start();
        }

        public static Unit Time(string op, Action f)
        {
            var func = f.ToFunc();
            return Time<Unit>(op, func);
        }
        
        public static T Time<T>(string op, Func<T> f)
        {
            using (var monitor = new PerformanceMonitor())
            {
                var result = f();

                var swElapsedMilliseconds = monitor.ElapsedMilliseconds;
            
                Console.WriteLine($"{op} took {swElapsedMilliseconds}ms");
                return result;                
            }
        }

        private long ElapsedMilliseconds => _stopWatch.ElapsedMilliseconds;

        public void Dispose()
        {
            _stopWatch.Stop();
        }
    }
}