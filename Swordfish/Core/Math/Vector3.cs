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

        public static implicit operator OpenTK.Mathematics.Vector3(Vector3 v)
        {
            return new OpenTK.Mathematics.Vector3(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector3(OpenTK.Mathematics.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public override string ToString() => $"({X}, {Y}, {Z})";

    }
}
