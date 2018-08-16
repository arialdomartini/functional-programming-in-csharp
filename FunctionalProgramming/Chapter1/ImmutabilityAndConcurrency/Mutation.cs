using System.Linq;
using System.Threading.Tasks;

namespace FunctionalProgramming.Chapter1.ImmutabilityAndConcurrency
{
    public class MutatingStateFromConcurrentProcesses
    {
        public (int, int) RunMutatingState()
        {
            // All the numbers from +10,000 to -10,000
            // Their sum should be 0
            var numbers = Enumerable.Range(-10_000, 10_000 * 2 + 1).Reverse().ToList();
            var result1 = -1;
            var result2 = -1;
            void Task1() => result1 = numbers.Sum();
            void Task2()
            {
                numbers.Sort();
                result2 = numbers.Sum();
            }

            Parallel.Invoke(Task1, Task2);
            return (result1, result2);
        }


        public (int, int) RunWithoutMutatingState()
        {
            var numbers = Enumerable.Range(-10_000, 10_000 * 2 + 1).Reverse().ToList();
            var result1 = -1;
            var result2 = -1;
            void Task1() => result1 = numbers.Sum();
            void Task2() => result2 = numbers.OrderBy(n => n).Sum();

            Parallel.Invoke(Task1, Task2);
            return (result1, result2);
        }
    }
}