// using static enables unqualified access to static members

using System;

namespace FunctionalProgramming.Chapter1.FunctionalFeatures
{
    public class Circle
    {
        public Circle(double radius) => Radius = radius;
        public double Radius { get; }
        
        // Expression bodied properties
        public double Circumference => Math.PI * 2 * Radius;

        public double Area
        {
            get
            {
                // local function
                double Square(double x) => Math.Pow(x, 2);
                return Math.PI * Square(Radius);
            }
        }

        // C# 7 Tuples with named elements
        public (double Circumference, double Area) Stats 
            => (Circumference, Area);
    }
}