﻿using Data.Annex.MathExtended.Interpolation;
using Data.Annex.MathExtended.Matrices;
using System;
using System.Collections.Generic;

namespace Data.Annex.MathExtended.Regression
{
    public class PolynomialRegression
    {
        private bool _changed = true;
        private List<Cartesian2D> _points = new List<Cartesian2D>();
        private int _degree = 2;
        private List<double> _coefficients = new List<double>();

        private void Sort()
        {
            _points.Sort((a, b) => a.X.CompareTo(b.X));
        }

        private void Calculate()
        {
            if (_changed)
            {
                Sort();
                int _len = _points.Count;
                //poly degree cannot be higher than count of data points
                if (_degree > _len) _degree = _len;
                var _x = new Matrix(_len, _degree + 1);
                var _y = new Matrix(_len, 1);
                for (int m = 0; m < _len; m++)
                {
                    for (int n = 0; n <= _degree; n++)
                    {
                        _x[m + 1, n + 1] = Math.Pow(_points[m].X, n);
                    }
                    _y[m + 1, 1] = _points[m].Y;
                }
                var _xt = _x.Duplicate();
                _xt.Transpose();
                var _xtx = _xt * _x;
                _xtx.Inverse();
                var _xtxi = _xtx * _xt;
                var _r = _xtxi * _y;
                _coefficients.Clear();
                for (int n = 0; n <= _degree; n++)
                {
                    _coefficients.Add(_r[n + 1, 1]);
                }
                _changed = false;
            }
        }

        public int Degree
        {
            get { return _degree; }
            set
            {
                _degree = value;
                _changed = true;
            }
        }

        public void Add(double ValueX, double ValueY)
        {
            _points.Add(new Cartesian2D(ValueX, ValueY));
            _changed = true;
        }

        public void Add(double[] ValuesX, double[] ValuesY)
        {
            for (int n = 0; n < ValuesX.Length; n++)
            {
                _points.Add(new Cartesian2D(ValuesX[n], ValuesY[n]));
            }
            _changed = true;
        }

        public void Add(Dictionary<double, double> Values)
        {
            foreach (KeyValuePair<double, double> _pair in Values)
            {
                _points.Add(new Cartesian2D(_pair.Key, _pair.Value));
            }
            _changed = true;
        }

        public void Clear()
        {
            _points.Clear();
        }

        public PolynomialRegression()
        {
            Clear();
        }

        public PolynomialRegression(Dictionary<double, double> Values)
        {
            Clear();
            Add(Values);
        }

        public PolynomialRegression(double[] ValuesX, double[] ValuesY)
        {
            Clear();
            Add(ValuesX, ValuesY);
        }

        public PolynomialRegression(double X1, double Y1, double X2, double Y2)
        {
            Clear();
            Add(X1, Y1);
            Add(X2, Y2);
        }

        public double Value(double x)
        {
            if (_points.Count < 2)
                throw new ArgumentOutOfRangeException("points", "Not enough data points.");
            Calculate();
            double _result = 0.0;
            double _x = 1.0;
            foreach (double _coeff in _coefficients)
            {
                _result += _coeff * _x;
                _x *= x;
            }
            return _result;
        }
    }
}
