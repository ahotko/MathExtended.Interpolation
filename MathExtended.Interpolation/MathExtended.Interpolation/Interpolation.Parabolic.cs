using MathExtended.Interpolation;
using System;
using System.Collections.Generic;

namespace Data.Annex.MathExtended.Interpolation
{
    public class Parabolic : InterpolationAbstract
    {
        private double _a;
        private double _b;
        private double _c;

        private void CalculateFactors()
        {
            if (Changed)
            {
                try
                {
                    double _D = Points[0].X * Points[0].X * Points[1].X + Points[0].X * Points[2].X * Points[2].X +
                        Points[1].X * Points[1].X * Points[2].X - Points[1].X * Points[2].X * Points[2].X -
                        Points[0].X * Points[0].X * Points[2].X - Points[0].X * Points[1].X * Points[1].X;
                    double _Da = Points[0].Y * Points[1].X + Points[0].X * Points[2].Y + Points[1].Y * Points[2].X -
                        Points[1].X * Points[2].Y - Points[0].Y * Points[2].X - Points[0].X * Points[1].Y;
                    double _Db = Points[0].X * Points[0].X * Points[1].Y + Points[0].Y * Points[2].X * Points[2].X +
                        Points[1].X * Points[1].X * Points[2].Y - Points[1].Y * Points[2].X * Points[2].X -
                        Points[0].X * Points[0].X * Points[2].Y - Points[0].Y * Points[1].X * Points[1].X;
                    double _Dc = Points[0].X * Points[0].X * Points[1].X * Points[2].Y + Points[0].X * Points[1].Y * Points[2].X * Points[2].X +
                        Points[0].Y * Points[1].X * Points[1].X * Points[2].X - Points[0].Y * Points[1].X * Points[2].X * Points[2].X -
                        Points[0].X * Points[0].X * Points[1].Y * Points[2].X - Points[0].X * Points[1].X * Points[1].X * Points[2].Y;
                    _a = _Da / _D;
                    _b = _Db / _D;
                    _c = _Dc / _D;
                    Changed = false;
                }
                catch (Exception)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
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

        public override double Interpolate(double X)
        {
            if (PointsCount != 3)
                throw new ArgumentException("Parabolic interpolation requires 3 points.");
            CalculateFactors();
            return _a * Math.Pow(X, 2) + _b * X + _c;
        }
    }

}
