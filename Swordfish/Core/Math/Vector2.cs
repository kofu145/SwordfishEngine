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

        public static implicit operator OpenTK.Mathematics.Vector2(Vector2 v)
        {
            return new OpenTK.Mathematics.Vector2(v.X, v.Y);
        }

        public static explicit operator Vector2(OpenTK.Mathematics.Vector2 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public override string ToString() => $"({X}, {Y})";

    }
}
