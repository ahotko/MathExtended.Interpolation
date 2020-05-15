using Data.Annex.MathExtended.Interpolation;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MathExtended.Interpolation
{
    public abstract class InterpolationAbstract
    {
        protected bool Changed { get; set; }

        protected int PointsCount => Points.Count;

        protected List<Cartesian2D> Points { get; set; } = new List<Cartesian2D>();

        protected void Sort()
        {
            Points.Sort((a, b) => a.X.CompareTo(b.X));
        }

        public void Add(double ValueX, double ValueY)
        {
            Points.Add(new Cartesian2D(ValueX, ValueY));
            Changed = true;
        }

        public void Add(double[] ValuesX, double[] ValuesY)
        {
            if ((ValuesX.Length != ValuesY.Length) || (ValuesX.Length == 0) || (ValuesY.Length == 0))
                throw new ArgumentException("Lengths of X and Y coordinate values must be the same");
            for (int n = 0; n < ValuesX.Length; n++)
            {
                Points.Add(new Cartesian2D() { X = ValuesX[n], Y = ValuesY[n] });
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
