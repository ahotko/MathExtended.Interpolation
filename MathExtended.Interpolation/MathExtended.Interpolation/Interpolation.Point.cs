using System;
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
}
