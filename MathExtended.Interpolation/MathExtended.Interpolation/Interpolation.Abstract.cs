using System;
using System.Collections.Generic;
using System.Drawing;

namespace Data.Annex.MathExtended.Interpolation
{
    public abstract class InterpolationAbstract
    {
        protected bool Changed { get; set; }

        protected int PointsCount => Points.Count;

        protected List<Cartesian2D> Points { get; set; } = new List<Cartesian2D>();

        protected InterpolationAbstract()
        {
            Clear();
        }

        protected InterpolationAbstract(Dictionary<double, double> values)
        {
            Clear();
            Add(values);
        }

        protected InterpolationAbstract(double[] x, double[] y)
        {
            Clear();
            Add(x, y);
        }

        protected InterpolationAbstract(double x1, double y1, double x2, double y2)
        {
            Clear();
            Add(x1, y1);
            Add(x2, y2);
        }

        protected void Sort()
        {
            Points.Sort((a, b) => a.X.CompareTo(b.X));
        }

        public void Add(double x, double y)
        {
            Points.Add(new Cartesian2D(x, y));
            Changed = true;
        }

        public void Add(double[] x, double[] y)
        {
            if ((x.Length != y.Length) || (x.Length == 0) || (y.Length == 0))
                throw new ArgumentException("Lengths of X and Y coordinate values must be the same");
            for (int n = 0; n < x.Length; n++)
            {
                Points.Add(new Cartesian2D() { X = x[n], Y = y[n] });
            }
            Changed = true;
        }

        public void Add(Dictionary<double, double> values)
        {
            foreach (KeyValuePair<double, double> _pair in values)
            {
                Points.Add(new Cartesian2D(_pair.Key, _pair.Value));
            }
            Changed = true;
        }

        public void Add(IEnumerable<PointF> values)
        {
            foreach (PointF point in values)
            {
                Points.Add(new Cartesian2D(point.X, point.Y));
            }
            Changed = true;
        }

        public void Clear()
        {
            Points.Clear();
            Changed = true;
        }

        public abstract double Interpolate(double X);
    }
}
