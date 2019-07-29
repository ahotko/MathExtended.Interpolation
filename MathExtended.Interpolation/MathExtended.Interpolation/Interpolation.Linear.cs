using MathExtended.Interpolation;
using System;
using System.Collections.Generic;

namespace Data.Annex.MathExtended.Interpolation
{
    public class Linear : InterpolationAbstract
    {
        public Linear()
        {
            Clear();
        }

        public Linear(Dictionary<double, double> Values)
        {
            Clear();
            Add(Values);
        }

        public Linear(double[] ValuesX, double[] ValuesY)
        {
            Clear();
            Add(ValuesX, ValuesY);
        }

        public Linear(double X1, double Y1, double X2, double Y2)
        {
            Clear();
            Add(X1, Y1);
            Add(X2, Y2);
        }

        public override double Interpolate(double X)
        {
            if (PointsCount < 2)
                throw new ArgumentException("Linear interpolation requires at least 2 points.");
            if (IsChanged) Sort();
            var _interval = PointFunctions.FindInterval(X, Points);
            var _left = _interval.Item1;
            var _right = _interval.Item2;
            //var _left = _points.Where(p => p.X <= X).LastOrDefault();
            //var _right = _points.Where(p => p.X > X).FirstOrDefault();
            if(_left == null || _right == null || _left.Equals(_right))
                throw new ArgumentOutOfRangeException($"Wrong interpolation point (x = {X}).");
            return (_left.Y + (((X - _left.X) / (_right.X - _left.X)) * (_right.Y - _left.Y)));
        }
    }
}
