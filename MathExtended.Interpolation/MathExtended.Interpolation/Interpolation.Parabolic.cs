using System;
using System.Collections.Generic;

namespace Data.Annex.MathExtended.Interpolation
{
    public class Parabolic
    {
        private class PointXY
        {
            public double X { get; set; }
            public double Y { get; set; }
        }

        private List<PointXY> _points = new List<PointXY>();
        private bool _changed = true;
        private double _a;
        private double _b;
        private double _c;

        private void CalculateFactors()
        {
            if (_changed)
            {
                try
                {
                    double _D = _points[0].X * _points[0].X * _points[1].X + _points[0].X * _points[2].X * _points[2].X +
                        _points[1].X * _points[1].X * _points[2].X - _points[1].X * _points[2].X * _points[2].X -
                        _points[0].X * _points[0].X * _points[2].X - _points[0].X * _points[1].X * _points[1].X;
                    double _Da = _points[0].Y * _points[1].X + _points[0].X * _points[2].Y + _points[1].Y * _points[2].X -
                        _points[1].X * _points[2].Y - _points[0].Y * _points[2].X - _points[0].X * _points[1].Y;
                    double _Db = _points[0].X * _points[0].X * _points[1].Y + _points[0].Y * _points[2].X * _points[2].X +
                        _points[1].X * _points[1].X * _points[2].Y - _points[1].Y * _points[2].X * _points[2].X -
                        _points[0].X * _points[0].X * _points[2].Y - _points[0].Y * _points[1].X * _points[1].X;
                    double _Dc = _points[0].X * _points[0].X * _points[1].X * _points[2].Y + _points[0].X * _points[1].Y * _points[2].X * _points[2].X +
                        _points[0].Y * _points[1].X * _points[1].X * _points[2].X - _points[0].Y * _points[1].X * _points[2].X * _points[2].X -
                        _points[0].X * _points[0].X * _points[1].Y * _points[2].X - _points[0].X * _points[1].X * _points[1].X * _points[2].Y;
                    _a = _Da / _D;
                    _b = _Db / _D;
                    _c = _Dc / _D;
                    _changed = false;
                }
                catch (Exception)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void Add(double ValueX, double ValueY)
        {
            if (_points.Count == 3)
                throw new ArgumentException("Parabolic interpolation requires 3 points.");
            _points.Add(new PointXY() { X = ValueX, Y = ValueY });
            _changed = true;
        }

        public void Add(double[] ValuesX, double[] ValuesY)
        {
            if ((ValuesX.Length != ValuesY.Length) || (ValuesX.Length != 3) || (ValuesY.Length != 3))
                throw new ArgumentException("Parabolic interpolation requires 3 points.");
            for (int n = 0; n < ValuesX.Length; n++)
            {
                _points.Add(new PointXY() { X = ValuesX[n], Y = ValuesY[n] });
            }
            _changed = true;
        }

        public void Add(Dictionary<double, double> Values)
        {
            if (Values.Count != 3)
                throw new ArgumentException("Parabolic interpolation requires 3 points.");
            foreach (KeyValuePair<double, double> _pair in Values)
            {
                _points.Add(new PointXY() { X = _pair.Key, Y = _pair.Value });
            }
            _changed = true;
        }

        public void Clear()
        {
            _points.Clear();
            _changed = true;
        }

        public Parabolic()
        {
            Clear();
        }

        public Parabolic(Dictionary<double, double> Values)
        {
            Clear();
            Add(Values);
        }

        public Parabolic(double[] ValuesX, double[] ValuesY)
        {
            Clear();
            Add(ValuesX, ValuesY);
        }

        public Parabolic(double X1, double Y1, double X2, double Y2, double X3, double Y3)
        {
            Clear();
            Add(X1, Y1);
            Add(X2, Y2);
            Add(X3, Y3);
        }

        public double Interpolate(double X)
        {
            if (_points.Count != 3)
                throw new ArgumentException("Parabolic interpolation requires 3 points.");
            CalculateFactors();
            return _a * Math.Pow(X, 2) + _b * X + _c;
        }
    }

}
