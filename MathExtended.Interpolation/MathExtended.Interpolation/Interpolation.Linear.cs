using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Annex.MathExtended.Interpolation
{
    public class Linear
    {
        private bool _changed = true;
        private List<Cartesian2D> _points = new List<Cartesian2D>();

        public void Add(double ValueX, double ValueY)
        {
            //if (_points.Count == 2)
            //    throw new ArgumentException("Linear interpolation requires 2 points.");
            _points.Add(new Cartesian2D() { X = ValueX, Y = ValueY });
        }

        private void Sort()
        {
            _points.Sort((a, b) => a.X.CompareTo(b.X));
            _changed = false;
        }

        public void Add(double[] ValuesX, double[] ValuesY)
        {
            if ((ValuesX.Length != ValuesY.Length) || (ValuesX.Length != 2) || (ValuesY.Length != 2))
                throw new ArgumentException("Linear interpolation requires 2 points.");
            for (int n = 0; n < ValuesX.Length; n++)
            {
                _points.Add(new Cartesian2D() { X = ValuesX[n], Y = ValuesY[n] });
                _changed = true;
            }
        }

        public void Add(Dictionary<double, double> Values)
        {
            if (Values.Count != 2)
                throw new ArgumentException("Linear interpolation requires 2 points.");
            foreach (KeyValuePair<double, double> _pair in Values)
            {
                _points.Add(new Cartesian2D() { X = _pair.Key, Y = _pair.Value });
                _changed = true;
            }
        }

        public void Clear()
        {
            _points.Clear();
        }

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

        public double Interpolate(double X)
        {
            if (_points.Count < 2)
                throw new ArgumentException("Linear interpolation requires at least 2 points.");
            if (_changed) Sort();
            var _left = _points.Where(p => p.X <= X).LastOrDefault();
            var _right = _points.Where(p => p.X > X).FirstOrDefault();
            if(_left == null || _right == null || _left.Equals(_right))
                throw new ArgumentOutOfRangeException();
            return (_left.Y + (((X - _left.X) / (_right.X - _left.X)) * (_right.Y - _left.Y)));
        }
    }
}
