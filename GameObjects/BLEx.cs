using Microsoft.Xna.Framework;
using System;
using Whitesnake.Core;

namespace Whitesnake.GameObjects
{
    internal class BLEx : IVisibleObject
    {
        const int Buffer = 100;
        private Random _rnd;

        public BLEx(CameraPoint cameraPoint)
        {
            CameraPoint = cameraPoint;

            _rnd = new Random();
            MapPosition = GetPositionAroundViewPort();
            var destination = GetPositionAroundViewPort();
            Angle = MapPosition.GetAngle(destination);
            Velocity = _rnd.Next(5, 25);
            var deltaX = (float)Math.Sin(Angle) * Velocity;
            var deltaY = (float)-Math.Cos(Angle) * Velocity;
            Movement = new Vector2(deltaX, deltaY);
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
            var lastDistance = Distance;
            Distance = MapPosition.GetDistance(CameraPoint.MapPosition);
            var distanceDelta = lastDistance - Distance;
            MarkedForDestroy = Distance < Global.ScreenWidth * 2 && distanceDelta > 0;
        }

        private Vector2 GetPositionAroundViewPort()
        {
            int x = _rnd.Next(Global.RenderWidth);
            int y = _rnd.Next(Global.RenderHeight);
            int side = _rnd.Next(0, 3);
            Vector2 origin = Vector2.Zero;

            switch (side)
            {
                case 0:
                    origin = new Vector2(x, -Buffer);
                    break;
                case 1:
                    origin = new Vector2(-Buffer, y);
                    break;
                case 2:
                    origin = new Vector2(x, Global.RenderHeight + Buffer);
                    break;
                case 3:
                    origin = new Vector2(Global.RenderWidth + Buffer, y);
                    break;
            }
            return origin;
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
