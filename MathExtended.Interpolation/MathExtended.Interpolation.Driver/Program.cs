using MathExtended.Additional;
using System;

namespace MathExtended.Interpolations.Driver
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

            var cosineInterpolation = new Cosine();
            cosineInterpolation.Add(1, 2);
            cosineInterpolation.Add(5, 8);
            cosineInterpolation.Add(7.7, 5);
            cosineInterpolation.Add(10, 15);
            cosineInterpolation.Add(11, 11.3);

            Console.WriteLine($"Value;Linear;Spline;Cosine");

            for (int n = 10; n < 111; n++)
            {
                double interpolatedValueLinear = interpolation.Linear(n/10.0);
                double interpolatedValueSpline = interpolation.Spline(n/10.0);
                double interpolatedValueCosine = interpolation.Cosine(n/10.0);


                if (n == 10 || n == 50 || n == 77 || n == 100 || n == 110)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"{n/10.0:N1};{interpolatedValueLinear:N3};{interpolatedValueSpline:N3};{interpolatedValueCosine:N3}");

                Console.ResetColor();
            }
        }

        private static void CartesianCoordinates()
        {
            var coordinates = new Cartesian2D(1, 1);
            coordinates += Math.Sqrt(2.0);

            Console.WriteLine(coordinates.ToString());
        }

        static void Main(string[] args)
        {
            SplineInterpolation();

            Console.WriteLine();

            CartesianCoordinates();

            Console.ReadKey();
        }
    }
}
