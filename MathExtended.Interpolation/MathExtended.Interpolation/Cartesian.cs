﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace MathExtended.Additional
{
    public class Cartesian2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Cartesian2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Cartesian2D() : this(0, 0) { }

        private bool NearlyEqual(double v1, double v2, double epsilon = 1E-5)
        {
            return Math.Abs(v1 - v2) < epsilon;
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
            return (X.GetHashCode() * 31) ^ (Y.GetHashCode() * 17);
        }

        public override string ToString()
        {
            return $"x = {X:N4}, y = {Y:N4}";
        }

        public static implicit operator PointF(Cartesian2D point)
        {
            return new PointF((float)point.X, (float)point.Y);
        }

        public static Cartesian2D operator +(Cartesian2D coordinates) => coordinates;

        public static Cartesian2D operator +(Cartesian2D coordinates, double length)
        {
            double r = Math.Sqrt(Math.Pow(coordinates.X, 2) + Math.Pow(coordinates.Y, 2)) + length;
            double theta = Math.Atan2(coordinates.Y, coordinates.X);

            return new Cartesian2D(r * Math.Cos(theta), r * Math.Sin(theta));
        }

        public static Cartesian2D operator +(double length, Cartesian2D coordinates)
        {
            return coordinates + length;
        }
    }

    static class PointFunctions
    {
        public static Tuple<int, int> FindIntervalIndex(double x, List<Cartesian2D> points)
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

        public static (Cartesian2D left, Cartesian2D right) FindInterval(double x, List<Cartesian2D> points)
        {
            if (x > points[points.Count - 1].X || x < points[0].X)
                return (null, null);
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
            return (points[_idxLeft], points[_idxRight]);
        }
    }
}
