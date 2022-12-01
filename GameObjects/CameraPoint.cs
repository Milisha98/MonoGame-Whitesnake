using Whitesnake.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Smoke.Core;
using Smoke.Sprites;
using System;
using Whitesnake.Demo;
using System.ComponentModel.Design;

namespace Whitesnake.GameObjects
{

    internal class CameraPoint : IMonoGame, IVisibleObject
    {
        const string TextureName = "Camera";
        public const float DefaultVelocity = 25f;

        public CameraPoint(GameState gameState)
        {
            GameState = gameState;
        }

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
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds / Global.FPS;

            if (GameState.IsDemoMode == false)
                ControllerInput(delta);
            else
                MoveTowardsNextWaypoint((float)gameTime.ElapsedGameTime.TotalMilliseconds);

            // Move the absolute position
            var sinTheta = (float)Math.Sin(Angle);
            var cosTheta = (float)Math.Cos(Angle);
            AngleVector = new Vector2(sinTheta, -cosTheta);
            MapPosition += (AngleVector * Velocity);

            UpdateDelta = delta;
        }

        private void ControllerInput(float delta)
        {
            var vector = KeyboardController.CheckInput();
            ControllerDirection = vector.X > 0 ? 1 : vector.X < 0 ? -1 : 0;

            // Set the Angle
            float deltaAngle = 0f;
            if (ControllerDirection == -1) deltaAngle = -delta * Handling;
            if (ControllerDirection == 1) deltaAngle = delta * Handling;
            Angle += deltaAngle;
        }

        private void MoveTowardsNextWaypoint(float ms)
        {
            var newAngle = MathHelper.ToRadians(GameState.DemoStep.Rotation);
            if (newAngle == 0)
                ControllerDirection = 0;
            else if (newAngle < 0)
                ControllerDirection = -1;
            else
                ControllerDirection = 1;
            Angle += newAngle;
            Velocity = GameState.DemoStep.Velocity;

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

        public GameState GameState { get; private set; }

        public SpriteFrame CameraSprite { get; private set; }

        public int ControllerDirection { get; private set; }
             
        public float UpdateDelta { get; private set; }

        public float Velocity { get; set; } = DefaultVelocity;
        
    }
}