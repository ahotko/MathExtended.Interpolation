using Data.Annex.MathExtended.Interpolation;
using System;
using System.Collections.Generic;

namespace MathExtended.Interpolation
{
    public abstract class InterpolationAbstract
    {
        private List<Data.Annex.MathExtended.Interpolation.Cartesian2D> _points = new List<Cartesian2D>();
        private bool _changed = true;

        public bool IsChanged { get => _changed; set => _changed = value; }

        public int PointsCount => _points.Count;

        public List<Cartesian2D> Points => _points;

        public void Sort()
        {
            _points.Sort((a, b) => a.X.CompareTo(b.X));
        }

        public void Add(double ValueX, double ValueY)
        {
            _points.Add(new Cartesian2D(ValueX, ValueY));
            _changed = true;
        }

        public void Add(double[] ValuesX, double[] ValuesY)
        {
            if ((ValuesX.Length != ValuesY.Length) || (ValuesX.Length == 0) || (ValuesY.Length == 0))
                throw new ArgumentException("Lengths of X and Y coordinate values must be the same");
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
                _points.Add(new Cartesian2D(_pair.Key, _pair.Value));
            }
            _changed = true;
        }

        public void Clear()
        {
            _points.Clear();
            _changed = true;
        }

        public abstract double Interpolate(double X);
    }
}
