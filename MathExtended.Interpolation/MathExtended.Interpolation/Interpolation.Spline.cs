using MathExtended.Interpolation;
using System;
using System.Collections.Generic;

namespace Data.Annex.MathExtended.Interpolation
{
    /// <summary>
    /// Description of Spline.
    /// </summary>
    public class Spline : InterpolationAbstract
    {
        private double[] _derivatives;


        private void CalculateSpline()
        {
            if (Changed)
            {
                Sort();
                _derivatives = new double[PointsCount];
                var _upperTriangle = new double[PointsCount];
                //
                _derivatives[0] = 0.0;
                _upperTriangle[0] = 0.0;
                for (int n = 1; n < (PointsCount - 1); n++)
                {
                    try
                    {
                        double _leftXFraction = (Points[n].X - Points[n - 1].X) / (Points[n + 1].X - Points[n - 1].X);
                        double _tmp = _leftXFraction * _derivatives[n - 1] + 2.0;
                        _derivatives[n] = (_leftXFraction - 1.0) / _tmp;
                        _upperTriangle[n] =
                            (Points[n + 1].Y - Points[n].Y) /
                            (Points[n + 1].X - Points[n].X) -
                            (Points[n].Y - Points[n - 1].Y) /
                            (Points[n].X - Points[n - 1].X);
                        _upperTriangle[n] = (6.0 * _upperTriangle[n] / (Points[n + 1].X - Points[n - 1].X) - _leftXFraction * _upperTriangle[n - 1]) / _tmp;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                _derivatives[PointsCount - 1] = 0.0;
                for (int i = PointsCount - 2; i >= 0; i--)
                {
                    _derivatives[i] = _derivatives[i] * _derivatives[i + 1] + _upperTriangle[i];
                }
                //
                Changed = false;
            }
        }

        public Spline()
        {
            Clear();
        }

        public Spline(Dictionary<double, double> Values)
        {
            Clear();
            Add(Values);
            CalculateSpline();
        }

        public Spline(double[] ValuesX, double[] ValuesY)
        {
            Clear();
            Add(ValuesX, ValuesY);
            CalculateSpline();
        }

        public override double Interpolate(double X)
        {
            if (PointsCount < 3)
                throw new ArgumentOutOfRangeException("points", "Not enough data points.");
            CalculateSpline();
            var _interval = PointFunctions.FindIntervalIndex(X, Points);
            int _idxLeft = _interval.Item1;
            int _idxRight = _interval.Item2;
            //width of bracketing interval
            double _dX = Points[_idxRight].X - Points[_idxLeft].X;
            if (_dX == 0.0)
                throw new ArgumentOutOfRangeException($"Two points have the same X value (x={X}).");
            double _rightX = (Points[_idxRight].X - X) / _dX;
            double _leftX = (X - Points[_idxLeft].X) / _dX;
            double _result = _rightX * Points[_idxLeft].Y +
                             _leftX * Points[_idxRight].Y +
                             ((Math.Pow(_rightX, 3) - _rightX) * _derivatives[_idxLeft] +
                             (Math.Pow(_leftX, 3) - _leftX) * _derivatives[_idxRight]) *
                             Math.Pow(_dX, 2) / 6.0;
            return _result;
        }
    }
}
