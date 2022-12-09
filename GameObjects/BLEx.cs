using Microsoft.Xna.Framework;
using System;
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

            // TODO: Check for Collisions

            // Check for Destruction
            UpdateMarkForDestruction();
        }

        private void UpdateMarkForDestruction()
        {
            var distance = MapPosition.GetDistance(CameraPoint.MapPosition);

            MarkedForDestroy = Distance > Global.ScreenWidth * 2;
        }



    public Vector2 MapPosition { get; set; } = Vector2.Zero;
        public Vector2 ScreenPosition { get; set; } = Vector2.Zero;
        public float Angle { get; set; } = 0;
        public float Velocity { get; set; }
        public Vector2 Movement { get; private set; }

        public float Distance { get; private set; }
        public bool MarkedForDestroy { get; set; }

        CameraPoint CameraPoint { get; set; }

    }
}
