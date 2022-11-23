﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
            return mapPosition - viewPortMapTopLeft - Global.CameraMiddle;
        }

        internal static int Clip(this int value, int min, int max) =>
            value < min
                ? min
                : value > max
                    ? max
                    : value;

        internal static float Clip(this float value, float min, float max) =>
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
            value = value.Clip(min, max);
            return value - min / max - min;
        }

    }

}