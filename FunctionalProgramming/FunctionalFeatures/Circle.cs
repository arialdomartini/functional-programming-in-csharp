// using static enables unqualified access to static members
using static System.Math;

namespace FunctionalProgramming.FunctionalFeatures
{
    public class Circle
    {
        public Circle(double radius) => Radius = radius;
        public double Radius { get; }
        
        // Expression bodied properties
        public double Circumference => PI * 2 * Radius;

        public double Area
        {
            get
            {
                // local function
                double Square(double x) => Pow(x, 2);
                return PI * Square(Radius);
            }
        }

        // C# 7 Tuples with named elements
        public (double Circumference, double Area) Stats 
            => (Circumference, Area);
    }
}