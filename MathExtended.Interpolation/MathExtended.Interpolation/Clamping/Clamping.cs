namespace MathExtended.Interpolation.Clamping
{
    public abstract class Clamping
    {
        public double Clamp(double x, double lowerLimit = 0.0, double upperLimit = 1.0)
        {
            if (x < lowerLimit) return lowerLimit;
            if (x > upperLimit) return upperLimit;
            return x;
        }

    }
}
