using MathExtended.Additional;
using System;

namespace MathExtended.Interpolations.Driver
{
    class Program
    {
        private static void InterpolationSnippets()
        {
            var interpolation = new Interpolation();
            interpolation.Add(1, 2);
            interpolation.Add(5, 8);
            interpolation.Add(7.7, 5);
            interpolation.Add(10, 15);
            interpolation.Add(11, 11.3);

            Console.WriteLine($"Value;Linear;Spline;Cosine");

            for (int n = 10; n < 111; n++)
            {
                double interpolatedValueLinear = interpolation.Linear(n / 10.0);
                double interpolatedValueSpline = interpolation.Spline(n / 10.0);
                double interpolatedValueCosine = interpolation.Cosine(n / 10.0);


                if (n == 10 || n == 50 || n == 77 || n == 100 || n == 110)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.WriteLine($"{n / 10.0:N1};{interpolatedValueLinear:N3};{interpolatedValueSpline:N3};{interpolatedValueCosine:N3}");

                Console.ResetColor();
            }
        }

        private static void InterpolationSnippetsRandomPoints()
        {
            var interpolation = new Interpolation();

            var random = new Random();

            double startingX = random.NextDouble() * 10.0;
            double x = startingX;

            int xStartInterval = (int)Math.Ceiling(startingX * 10.0);
            int xEndInterval = (int)Math.Floor(x * 10.0) - 1;

            //add 10 random points
            for (int n = 0; n < 30; n++)
            {
                double y = random.NextDouble() * 50.0;
                interpolation.Add(x, y);

                xEndInterval = (int)Math.Floor(x * 10.0) - 1;
                x += random.NextDouble() * 10.0;
                x += 1.0; //ensure the horizontal distance, not needed, but because it's random values
            }

            Console.WriteLine($"Value;Linear;Spline;Cosine");

            for (int n = xStartInterval; n < xEndInterval; n++)
            {
                double interpolatedValueLinear = interpolation.Linear(n / 10.0);
                double interpolatedValueSpline = interpolation.Spline(n / 10.0);
                double interpolatedValueCosine = interpolation.Cosine(n / 10.0);

                Console.WriteLine($"{n / 10.0:N1};{interpolatedValueLinear:N3};{interpolatedValueSpline:N3};{interpolatedValueCosine:N3}");
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
            InterpolationSnippets();

            Console.WriteLine();

            InterpolationSnippetsRandomPoints();

            Console.WriteLine();

            CartesianCoordinates();

            Console.ReadKey();
        }
    }
}
