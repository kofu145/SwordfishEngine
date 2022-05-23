using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swordfish.Core.Math
{
    public class MathUtil
    {
        public static Vector2 QuadraticBezierCurve(float t, Vector2 p0, Vector2 p1, Vector2 p2) {

            return p1 + ((1 - t) * (1 - t)) * (p0 - p1) + (t * t) * (p2 - p1);

        }

        public static Vector2 CubicBezierCurve(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            return (1 - t) * QuadraticBezierCurve(t, p0, p1, p2) + t * QuadraticBezierCurve(t, p1, p2, p3);
        }

        public static Vector2 ExplicitCubicBezierCurve(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            return ((1 - t) * (1 - t) * (1 - t)) * p0 + 3 * ((1 - t) * (1 - t)) * t * p1 + 3 * ((1 - t) * (1 - t)) * p2 + (t * t * t) * p3;
        }

       public static float GetBezierArcLength(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            var chord = (p3 - p0).Length;
            var controlNet = (p0 - p1).Length + (p2 - p1).Length + (p3 - p2).Length;

            return (controlNet + chord) / 2;
        }

        public static float GetNormalizedPointFromInterval(float min, float max, float point)
        {
            return (point - min) / (max - min);
        }

        public static float GetPointFromNormalizedInterval(float min, float max, float point)
        {
            return point * (max - min) + min;
        }

    }
}
