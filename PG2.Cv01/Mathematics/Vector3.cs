using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG2.Mathematics
{
    public struct Vector3
    {
        #region Properties

        public Double X;
        public Double Y;
        public Double Z;

        public Double Length
        {
            get { return Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        public Vector3 Normalized
        {
            get
            {
                Double ilength = 1.0 / Math.Sqrt(X * X + Y * Y + Z * Z);
                return new Vector3(ilength * X, ilength * Y, ilength * Z);
            }
        }

        public static Vector3 Zero
        {
            get { return new Vector3(0, 0, 0); }
        }

        #endregion


        #region Init

        public Vector3(Double x, Double y, Double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion


        #region Object

        public override String ToString()
        {
            return "(" + X.ToString("F2") + "; " + Y.ToString("F2") + "; " + Z.ToString("F2") + ")";
        }

        #endregion


        #region Arithmetic Operations

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.X, -a.Y, -a.Z);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator *(Vector3 a, Double b)
        {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector3 operator *(Double a, Vector3 b)
        {
            return new Vector3(a * b.X, a * b.Y, a * b.Z);
        }

        // Dot Product
        public static Double operator *(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        // 3D Cross Product
        public static Vector3 operator %(Vector3 a, Vector3 b)
        {
            return new Vector3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
        }

        public static Vector3 Clamp(Vector3 v, Double min, Double max)
        {
            return new Vector3(
                (v.X < min) ? min : (v.X > max) ? max : v.X,
                (v.Y < min) ? min : (v.Y > max) ? max : v.Y,
                (v.Z < min) ? min : (v.Z > max) ? max : v.Z
            );
        }

        #endregion
    }
}
