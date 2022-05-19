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
        public static Vector2 Zero { get { return new Vector2(0, 0); } private set { } }
        public static Vector2 Up { get { return new Vector2(0, 1); } private set { } }
        public static Vector2 Down { get { return new Vector2(0, -1); } private set { } }
        public static Vector2 Left { get { return new Vector2(-1, 0); } private set { } }
        public static Vector2 Right { get { return new Vector2(1, 0); } private set { } }
        public float Length => MathF.Sqrt((X * X) + (Y * Y));

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
        /// <summary>
        /// Doesn't work, makes things NaN for some reason. Will fix later.
        /// </summary>
        public void Normalize()
        {
            var scale = 1.0f / Length;
            Console.WriteLine(scale);
            X *= scale;
            Y *= scale;
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
            return obj is Vector2 && Equals((Vector2)obj);
        }

        /// <inheritdoc/>
        public bool Equals(Vector2 other)
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
