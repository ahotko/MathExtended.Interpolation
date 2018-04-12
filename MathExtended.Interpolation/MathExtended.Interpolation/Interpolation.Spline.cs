using System;
using System.Collections.Generic;

namespace Data.Annex.MathExtended.Interpolation
{
    /// <summary>
    /// Description of Spline.
    /// </summary>
    public class Spline
    {
        private bool _changed = true;
        private List<Cartesian2D> _points = new List<Cartesian2D>();
        private double[] _derivatives;

        private void Sort()
        {
            _points.Sort((a, b) => a.X.CompareTo(b.X));
        }

        private void CalculateSpline()
        {
            if (_changed)
            {
                Sort();
                _derivatives = new double[_points.Count];
                var _upperTriangle = new double[_points.Count];
                //
                _derivatives[0] = 0.0;
                _upperTriangle[0] = 0.0;
                for (int n = 1; n < (_points.Count - 1); n++)
                {
                    try
                    {
                        double _leftXFraction = (_points[n].X - _points[n - 1].X) / (_points[n + 1].X - _points[n - 1].X);
                        double _tmp = _leftXFraction * _derivatives[n - 1] + 2.0;
                        _derivatives[n] = (_leftXFraction - 1.0) / _tmp;
                        _upperTriangle[n] =
                             (_points[n + 1].Y - _points[n].Y) /
                        (_points[n + 1].X - _points[n].X) -
                        (_points[n].Y - _points[n - 1].Y) /
                        (_points[n].X - _points[n - 1].X);
                        _upperTriangle[n] = (6.0 * _upperTriangle[n] / (_points[n + 1].X - _points[n - 1].X) - _leftXFraction * _upperTriangle[n - 1]) / _tmp;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                _derivatives[_points.Count - 1] = 0.0;
                for (int i = _points.Count - 2; i >= 0; i--)
                {
                    _derivatives[i] = _derivatives[i] * _derivatives[i + 1] + _upperTriangle[i];
                }
                //
                _changed = false;
            }
        }

        public void Add(double ValueX, double ValueY)
        {
            _points.Add(new Cartesian2D() { X = ValueX, Y = ValueY });
            _changed = true;
        }

        public void Add(double[] ValuesX, double[] ValuesY)
        {
            if ((ValuesX.Length != ValuesY.Length) || (ValuesX.Length == 0) || (ValuesY.Length == 0))
                throw new ArgumentException();
            for (int n = 0; n < ValuesX.Length; n++)
            {
                _points.Add(new Cartesian2D() { X = ValuesX[n], Y = ValuesY[n] });
            }
            _changed = true;
        }

        public void Add(Dictionary<double, double> Values)
        {
            foreach (KeyValuePair<double, double> _pair in Values)
            {
                _points.Add(new Cartesian2D() { X = _pair.Key, Y = _pair.Value });
            }
            _changed = true;
        }

        public void Clear()
        {
            _points.Clear();
            _changed = true;
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

        public double Interpolate(double X)
        {
            if (_points.Count < 3)
                throw new ArgumentOutOfRangeException("points", "Not enough data points.");
            CalculateSpline();
            var _interval = PointFunctions.FindIntervalIndex(X, _points);
            int _idxLeft = _interval.Item1;
            int _idxRight = _interval.Item2;
            //width of bracketing interval
            double _dX = _points[_idxRight].X - _points[_idxLeft].X;
            if (_dX == 0.0)
                throw new ArgumentOutOfRangeException("interval", "Two points have the same X value.");
            double _rightX = (_points[_idxRight].X - X) / _dX;
            double _leftX = (X - _points[_idxLeft].X) / _dX;
            double _result = _rightX * _points[_idxLeft].Y +
                             _leftX * _points[_idxRight].Y +
                             ((Math.Pow(_rightX, 3) - _rightX) * _derivatives[_idxLeft] +
                             (Math.Pow(_leftX, 3) - _leftX) * _derivatives[_idxRight]) *
                             Math.Pow(_dX, 2) / 6.0;
            return _result;
        }
    }
}
