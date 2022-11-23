using Whitesnake.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Smoke.Core;
using Smoke.Sprites;
using System;

namespace Whitesnake.GameObjects
{

    internal class CameraPoint : IMonoGame, IVisibleObject
    {
        const string TextureName = "Camera";
        const float Velocity = 25f;

        public void LoadContent(ContentManager contentManager)
        {
            // Load the Camera Point
            Texture2D texture = contentManager.Load<Texture2D>(TextureName);
            CameraSprite = new SpriteFrame(TextureName,
                                           texture,
                                           texture.Bounds,
                                           new Vector2(texture.Bounds.Width, texture.Bounds.Height));
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle viewPort)
        {
            spriteBatch.Draw(CameraSprite.Texture, viewPort.Center.ToVector2(), Color.White);
        }


        public void Update(GameTime gameTime)
        {
            var newPosition = this.MapPosition;
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds / Global.FPS;

            ControllerInput(delta);

            // Move the absolute position
            var sinTheta = (float)Math.Sin(Angle);
            var cosTheta = (float)Math.Cos(Angle);
            AngleVector = new Vector2(sinTheta, -cosTheta);
            MapPosition += (AngleVector * Velocity);

        }

        private void ControllerInput(float delta)
        {
            if (IsDemoMode) return;
            var vector = KeyboardController.CheckInput();
            int direction = vector.X > 0 ? 1 : vector.X < 0 ? -1 : 0;

            // Set the Angle
            float deltaAngle = 0f;
            if (direction == -1) deltaAngle = -delta * Handling;
            if (direction == 1) deltaAngle = delta * Handling;
            Angle += deltaAngle;
        }

        #region IVisibleObject

        public Vector2 MapPosition { get; set; }
        public Vector2 ScreenPosition { get; set; } = new Vector2(Global.RenderWidth / 2, Global.RenderHeight / 2);

        #endregion

        #region Rotation

        public float Handling { get; set; } = MathHelper.ToRadians(5);
        public float Angle { get; set; } = 0;
        public Vector2 AngleVector { get; set; } = Vector2.Zero;

        #endregion

        public bool IsDemoMode { get; set; }

        public SpriteFrame CameraSprite { get; set; }
    }
}