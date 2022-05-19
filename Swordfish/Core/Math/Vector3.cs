using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swordfish.Core.Math
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3((float x, float y, float z) vector)
        {
            X = vector.x;
            Y = vector.y;
            Z = vector.z;
        }

        public Vector3((int x, int y, int z) vector)
        {
            X = vector.x;
            Y = vector.y;
            Z = vector.z;
        }

        public static Vector3 operator +(Vector3 a) => a;
        public static Vector3 operator -(Vector3 a) => new Vector3(-a.X, -a.Y, -a.Z);

        public static Vector3 operator +(Vector3 a, Vector3 b)
            => new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static Vector3 operator -(Vector3 a, Vector3 b)
            => new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static Vector3 operator *(Vector3 a, float b)
            => new Vector3(a.X * b, a.Y * b, a.Z * b);

        public static Vector3 operator *(float b, Vector3 a)
            => new Vector3(a.X * b, a.Y * b, a.Z * b);

        public static Vector3 operator *(Vector3 a, Vector3 b)
            => new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

        public static Vector3 operator /(Vector3 a, float b)
            => new Vector3(a.X / b, a.Y / b, a.Z / b);

        public static Vector3 operator /(Vector3 a, Vector3 b)
            => new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        public static bool operator ==(Vector3 a, Vector3 b)
            => a.Equals(b);

        public static bool operator !=(Vector3 a, Vector3 b)
            => !(a == b);

        public static implicit operator OpenTK.Mathematics.Vector3(Vector3 v)
        {
            return new OpenTK.Mathematics.Vector3(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector3(OpenTK.Mathematics.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        /// <inheritdoc/>
        public override string ToString() => $"({X}, {Y}, {Z})";

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Vector3 && Equals((Vector3)obj);
        }

        /// <inheritdoc/>
        public bool Equals(Vector3 other)
        {
            return X == other.X &&
                Y == other.Y &&
                Z == other.Z;
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }

}
