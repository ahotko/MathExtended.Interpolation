using System;

namespace MathExtended.Limiting
{
    public class Wrapping
    {
        public double Wrap(double x, double lowerLimit, double upperLimit)
        {
            return x - Math.Floor((x - lowerLimit) / (upperLimit - lowerLimit)) * (upperLimit - lowerLimit);
        }
    }
}
