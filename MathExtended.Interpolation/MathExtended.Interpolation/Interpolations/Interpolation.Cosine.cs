using MathExtended.Additional;
using System;
using System.Collections.Generic;

namespace MathExtended.Interpolations
{
    public class Cosine : IInterpolation
    {
        private List<Cartesian2D> _points;

        public void Calculate(List<Cartesian2D> points)
        {
            _points = points;
            Check();
        }

        public bool Check()
        {
            if (_points.Count < 2)
                throw new ArgumentException("Linear interpolation requires at least 2 points.");
            return true;
        }

        public double Interpolate(double x)
        {
            var interval = PointFunctions.FindInterval(x, _points);
            if (interval.left is null || interval.right is null || interval.left.Equals(interval.right))
                throw new ArgumentOutOfRangeException($"Wrong interpolation point (x = {x}).");

            double deltaX = interval.right.X - interval.left.X;

            double _xt = (1.0 - Math.Cos((x - interval.left.X) / deltaX * Math.PI)) / 2.0;
            return (interval.left.Y * (1.0 - _xt) + interval.right.Y * _xt);
        }
    }
}
