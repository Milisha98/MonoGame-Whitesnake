using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Whitesnake.GameObjects;

namespace Whitesnake.Core
{

    internal static class Helper
    {
        internal static Vector2 Middle(this Texture2D t1) =>
            new Vector2(t1.Width / 2, t1.Height / 2);

        internal static Vector2 Middle(this Texture2D t1, Texture2D t2) =>
            new Vector2(t1.Width / 2, t1.Height / 2) -
            new Vector2(t2.Width / 2, t2.Height / 2);

        internal static Vector2 LocationToVector(this Rectangle rectangle) =>
            new Vector2(rectangle.Left, rectangle.Top);

        internal static Rectangle Move(this Rectangle rectangle, Vector2 move) =>
            new Rectangle((rectangle.Location.ToVector2() + move).ToPoint(), rectangle.Size);

        internal static Vector2 RotatePoint(this Vector2 pointToRotate, float angleInRadians, Vector2? centerPoint = null)
        {
            centerPoint = centerPoint ?? Vector2.Zero;
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Vector2
            {
                X =
                    (float)
                    (cosTheta * (pointToRotate.X - centerPoint.Value.X) -
                     sinTheta * (pointToRotate.Y - centerPoint.Value.Y) + centerPoint.Value.X),
                Y =
                    (float)
                    (sinTheta * (pointToRotate.X - centerPoint.Value.X) +
                     cosTheta * (pointToRotate.Y - centerPoint.Value.Y) + centerPoint.Value.Y)
            };
        }

        internal static Vector2 MapPositionToScreenPosition(this Vector2 mapPosition, Rectangle viewPort)
        {
            var viewPortMapTopLeft = viewPort.Location.ToVector2();
            var halfScreen = new Vector2(Global.RenderWidth / 2, Global.RenderHeight / 2);
            return mapPosition - viewPortMapTopLeft + halfScreen;
        }

        internal static int Clip(this int value, int min, int max) =>
            value < min
                ? min
                : value > max
                    ? max
                    : value;

        internal static float Clamp(this float value, float min, float max) =>
            value < min
                ? min
                : value > max
                    ? max
                    : value;

        internal static double Clip(this double value, double min, double max) =>
        value < min
            ? min
            : value > max
                ? max
                : value;

        internal static float NormalizeEasing(this float value, float min, float max)
        {
            value = value.Clamp(min, max);
            return value - min / max - min;
        }


        internal static float GetDistance(this Vector2 v1, Vector2 v2)
        {
            double x = (double)Math.Abs(v1.X - v2.X);
            double y = (double)Math.Abs(v1.Y - v2.Y);
            double result = Math.Sqrt((x * x) + (y * y));
            return (float)result;

        }

        internal static float GetAngle(this Vector2 a, Vector2 b)
        {
                double x = b.X - a.X;
                double y = b.Y - a.Y;
                return (float)Math.Atan2(y, x);
        }

    }

}