using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Annex.MathExtended.Interpolation
{
    public class Cosine
    {
        private bool _changed = true;
        private List<PointXY> _points = new List<PointXY>();
        private double[] _derivatives;

        private void Sort()
        {
            _points.Sort((a, b) => a.X.CompareTo(b.X));
        }

        public void Add(double ValueX, double ValueY)
        {
            _points.Add(new PointXY() { X = ValueX, Y = ValueY });
            _changed = true;
        }

        public void Add(double[] ValuesX, double[] ValuesY)
        {
            if ((ValuesX.Length != ValuesY.Length) || (ValuesX.Length == 0) || (ValuesY.Length == 0))
                throw new ArgumentException();
            for (int n = 0; n < ValuesX.Length; n++)
            {
                _points.Add(new PointXY() { X = ValuesX[n], Y = ValuesY[n] });
            }
            _changed = true;
        }

        public void Add(Dictionary<double, double> Values)
        {
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

        public Cosine()
        {
            Clear();
        }

        public Cosine(Dictionary<double, double> Values)
        {
            Clear();
            Add(Values);
        }

        public Cosine(double[] ValuesX, double[] ValuesY)
        {
            Clear();
            Add(ValuesX, ValuesY);
        }

        public double Interpolate(double X)
        {
            if (_points.Count < 2)
                throw new ArgumentOutOfRangeException("points", "Not enough data points.");
            double _result = 0.0;
            return _result;
        }
    }
}
