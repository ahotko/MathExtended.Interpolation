using MathExtended.Interpolation;
using System;
using System.Collections.Generic;

namespace Data.Annex.MathExtended.Interpolation
{
    public class Linear : InterpolationAbstract
    {
        public Linear() : base() { }

        public Linear(Dictionary<double, double> values) : base(values) { }

        public Linear(double[] x, double[] y) : base(x, y) { }

        public Linear(double x1, double y1, double x2, double y2) : base(x1, y1, x2, y2) { }

        public override double Interpolate(double X)
        {
            if (PointsCount < 2)
                throw new ArgumentException("Linear interpolation requires at least 2 points.");
            if (Changed) Sort();
            var _interval = PointFunctions.FindInterval(X, Points);
            var _left = _interval.Item1;
            var _right = _interval.Item2;
            if(_left == null || _right == null || _left.Equals(_right))
                throw new ArgumentOutOfRangeException($"Wrong interpolation point (x = {X}).");
            return (_left.Y + (((X - _left.X) / (_right.X - _left.X)) * (_right.Y - _left.Y)));
        }
    }
}
