﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Annex.MathExtended.Interpolation
{
    class Cartesian2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Cartesian2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Cartesian2D() : this(0, 0) { }

        private bool NearlyEqual(double v1, double v2)
        {
            return Math.Abs(v1 - v2) < 0.00001;
        }

        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Cartesian2D p = (Cartesian2D)obj;
            return (NearlyEqual(X, p.X) && NearlyEqual(Y, p.Y));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    static class PointFunctions
    {
        public static Tuple<int,int> FindIntervalIndex(double x, List<Cartesian2D> points)
        {
            int _idxLeft = 0;
            int _idxRight = points.Count - 1;
            //bracket the X value using binary search
            while (_idxRight - _idxLeft > 1)
            {
                int i = (_idxLeft + _idxRight) / 2;
                if (points[i].X > x)
                    _idxRight = i;
                else
                    _idxLeft = i;
            }
            return new Tuple<int, int>(_idxLeft, _idxRight);
        }

        public static Tuple<Cartesian2D, Cartesian2D> FindInterval(double x, List<Cartesian2D> points)
        {
            if (x >= points[points.Count - 1].X || x <= points[0].X)
                return new Tuple<Cartesian2D, Cartesian2D>(null, null);
            int _idxLeft = 0;
            int _idxRight = points.Count - 1;
            //bracket the X value using binary search
            while (_idxRight - _idxLeft > 1)
            {
                int i = (_idxLeft + _idxRight) / 2;
                if (points[i].X > x)
                    _idxRight = i;
                else
                    _idxLeft = i;
            }
            return new Tuple<Cartesian2D, Cartesian2D>(points[_idxLeft], points[_idxRight]);
        }
    }
}
