using System;

namespace MathExtended.Interpolation.Wrapping
{
    public class Wrapping
    {
        public double Wrap(double x, double lowerLimit, double upperLimit)
        {
            return x - Math.Floor((x - lowerLimit) / (upperLimit - lowerLimit)) * (upperLimit - lowerLimit);
        }
    }
}
