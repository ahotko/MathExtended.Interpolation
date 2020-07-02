using Data.Annex.MathExtended.Interpolation;
using System;

namespace MathExtended.Interpolation.Driver
{
    class Program
    {
        private static void SplineInterpolation()
        {
            var splineInterpolation = new Spline();
            splineInterpolation.Add(1, 2);
            splineInterpolation.Add(5, 8);
            splineInterpolation.Add(7.7, 5);
            splineInterpolation.Add(10, 15);
            splineInterpolation.Add(11, 11.3);

            var linearInterpolation = new Linear();
            linearInterpolation.Add(1, 2);
            linearInterpolation.Add(5, 8);
            linearInterpolation.Add(7.7, 5);
            linearInterpolation.Add(10, 15);
            linearInterpolation.Add(11, 11.3);

            for (int n=10; n<111; n++)
            {
                double interpolatedValueLinear = linearInterpolation.Interpolate(n/10.0);
                double interpolatedValueSpline = splineInterpolation.Interpolate(n/10.0);


                if (n == 10 || n == 50 || n == 77 || n == 100 || n == 110)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"{n/10.0:N1};{interpolatedValueSpline:N3};{interpolatedValueLinear:N3}");

                Console.ResetColor();
            }
        }

        static void Main(string[] args)
        {
            SplineInterpolation();

            Console.ReadKey();
        }
    }
}
