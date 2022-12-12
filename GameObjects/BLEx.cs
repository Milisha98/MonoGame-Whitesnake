using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Whitesnake.Core;

namespace Whitesnake.GameObjects
{
    internal class BLEx : IVisibleObject
    {
        protected int Buffer { get => Global.RenderWidth; }
        private Random _rnd;

        public BLEx(CameraPoint cameraPoint)
        {
            CameraPoint = cameraPoint;

            _rnd = new Random();
            var angle = MathHelper.ToRadians(_rnd.Next(0, 359));
            MapPosition = GetOrigin(angle);
            angle = MathHelper.ToRadians(_rnd.Next(0, 359));
            var destination = GetOrigin(angle);
            Angle = CameraPoint.MapPosition.GetAngle(destination);
            Velocity = _rnd.Next(5, 25);
            var deltaX = (float)Math.Sin(Angle) * Velocity;
            var deltaY = (float)-Math.Cos(Angle) * Velocity;
            Movement = new Vector2(deltaX, deltaY);
        }

        private Vector2 GetOrigin(double angle)
        {

            var deltaX = (float)Math.Sin(angle) * Buffer;
            var deltaY = (float)-Math.Cos(angle) * Buffer;
            return CameraPoint.MapPosition + new Vector2(deltaX, deltaY);
        }

        public void Update(float delta)
        {
            // Move
            MapPosition += Movement * (delta / 100);

            // Check for Destruction
            UpdateMarkForDestruction();

            // Collision Coordinates
            var origin = new Vector2(27.5f, 38.5f);
            CollisionPoints = RelativeCollisionPoints.Select(r => r.Rotate(origin, Angle))
                                                     .Select(r => r.Move(MapPosition - origin))                         // Align Middle
                                                     .Select(r => r.Scale(0.5f))                                        // Scale
                                                     .ToList();

        }

        private void UpdateMarkForDestruction()
        {
            Distance = Math.Abs(MapPosition.GetDistance(CameraPoint.MapPosition));
            MarkedForDestroy = Distance > Global.ScreenWidth * 2;
        }



        public Vector2 MapPosition { get; set; } = Vector2.Zero;
        public Vector2 ScreenPosition { get; set; } = Vector2.Zero;
        public float Angle { get; set; } = 0;
        public float Velocity { get; set; }
        public Vector2 Movement { get; private set; }

        public float Distance { get; private set; }
        public bool MarkedForDestroy { get; private set; } = false;

        CameraPoint CameraPoint { get; set; }

        public List<Rectangle> CollisionPoints { get; private set; } = new List<Rectangle>();

        private List<Rectangle> RelativeCollisionPoints
        {
            get =>
                new List<Rectangle> { Helper.RectangleFromPoints(new Point(0, 0), new Point(55, 77)) };

        }
    }
}
