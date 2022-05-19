using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Swordfish.Core.Math
{
    public struct Vector2
    {
        public float X;
        public float Y;
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2((float x, float y) vector)
        {
            X = vector.x;
            Y = vector.y;
        }

        public Vector2((int x, int y) vector)
        {
            X = vector.x;
            Y = vector.y;
        }

        public static Vector2 operator +(Vector2 a) => a;
        public static Vector2 operator -(Vector2 a) => new Vector2(-a.X, -a.Y);

        public static Vector2 operator +(Vector2 a, Vector2 b)
            => new Vector2(a.X + b.X, a.Y + b.Y);

        public static Vector2 operator -(Vector2 a, Vector2 b)
            => new Vector2(a.X - b.X, a.Y - b.Y);

        public static Vector2 operator *(Vector2 a, float b)
            => new Vector2(a.X * b, a.Y * b);

        public static Vector2 operator *(float b, Vector2 a)
            => new Vector2(a.X * b, a.Y * b);

        public static Vector2 operator *(Vector2 a, Vector2 b)
            => new Vector2(a.X * b.X, a.Y * b.Y);

        public static Vector2 operator /(Vector2 a, float b)
            => new Vector2(a.X / b, a.Y / b);

        public static Vector2 operator /(Vector2 a, Vector2 b)
            => new Vector2(a.X / b.X, a.Y / b.Y);

        public static bool operator ==(Vector2 a, Vector2 b)
            => a.Equals(b);

        public static bool operator !=(Vector2 a, Vector2 b)
            => !(a == b);

        public static implicit operator OpenTK.Mathematics.Vector2(Vector2 v)
        {
            return new OpenTK.Mathematics.Vector2(v.X, v.Y);
        }

        public static explicit operator Vector2(OpenTK.Mathematics.Vector2 v)
        {
            return new Vector2(v.X, v.Y);
        }

        /// <inheritdoc/>
        public override string ToString() => $"({X}, {Y})";

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is Vector3 && Equals((Vector3)obj);
        }

        /// <inheritdoc/>
        public bool Equals(Vector3 other)
        {
            return X == other.X &&
                Y == other.Y;
        }
        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

    }
}
