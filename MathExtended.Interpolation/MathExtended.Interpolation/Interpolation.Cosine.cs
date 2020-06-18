using System;
using System.Collections.Generic;

namespace Data.Annex.MathExtended.Interpolation
{
    public class Cosine : InterpolationAbstract
    {
        public Cosine() : base() { }

        public Cosine(Dictionary<double, double> values) : base(values) { }

        public Cosine(double[] x, double[] y) : base(x, y) { }

        public Cosine(double x1, double y1, double x2, double y2) : base(x1, y1, x2, y2) { }

        public override double Interpolate(double X)
        {
            if (PointsCount < 2)
                throw new ArgumentException("Cosine Interpolation requires at least 2 points.");
            if (Changed) Sort();
            var _interval = PointFunctions.FindInterval(X, Points);
            var _left = _interval.Item1;
            var _right = _interval.Item2;
            if (_left == null || _right == null || _left.Equals(_right) || _left.X == _right.X)
                throw new ArgumentOutOfRangeException($"Wrong interpolation point (x = {X}).");
            double _deltaX = _right.X - _left.X;
            double _xt = (1.0 - Math.Cos((X - _left.X) / _deltaX * Math.PI)) / 2.0;
            return (_left.Y * (1.0 - _xt) + _right.Y * _xt);
        }
    }
}
