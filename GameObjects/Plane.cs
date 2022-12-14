using Whitesnake.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Smoke.Core;
using Smoke.Sprites;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

namespace Whitesnake.GameObjects
{

    internal class Plane : IMonoGame, IVisibleObject
    {
        const string PlaneTextureName = "biplane";
        const string PlaneLeftTextureName = "biplane-left";
        const string PlaneRightTextureName = "biplane-right";

        public Plane(GameState gameState, CameraPoint cameraPoint)
        {
            GameState = gameState;
            CameraPoint = cameraPoint;
        }

        public void LoadContent(ContentManager contentManager)
        {
            // Load the Camera Point
            Texture2D bodyTexture = contentManager.Load<Texture2D>(PlaneTextureName);
            PlaneSprite = new SpriteFrame(PlaneTextureName,
                                           bodyTexture,
                                           bodyTexture.Bounds,
                                           new Vector2(bodyTexture.Bounds.Width, bodyTexture.Bounds.Height));

            Texture2D footTexture = contentManager.Load<Texture2D>(PlaneLeftTextureName);
            PlaneLeftSprite = new SpriteFrame(PlaneLeftTextureName,
                                           footTexture,
                                           footTexture.Bounds,
                                           new Vector2(footTexture.Bounds.Width, footTexture.Bounds.Height));

            Texture2D laserGunTexture = contentManager.Load<Texture2D>(PlaneRightTextureName);
            PlaneRightSprite = new SpriteFrame(PlaneRightTextureName,
                                           laserGunTexture,
                                           laserGunTexture.Bounds,
                                           new Vector2(laserGunTexture.Bounds.Width, laserGunTexture.Bounds.Height));
        }

        public void Update(GameTime gameTime)
        {
            if (GameState.IsFinished) return;

            SetCurrentSpriteFromControllerInput();          

            // Set the Location
            Angle = CameraPoint.Angle;
            ScreenPosition = CameraPoint.ScreenPosition;
            MapPosition = CameraPoint.MapPosition;

            // Smoke Emitter
            var middle = CurrentSprite.Texture.Middle();
            Vector2 planeMiddle = this.MapPosition - CurrentSprite.Texture.Middle();
            var smokeDelta = CameraPoint.AngleVector * (CurrentSprite.Texture.Height / 2);
            SmokePosition = planeMiddle - smokeDelta;
            
            var sinTheta = (float)Math.Sin(Angle);
            var cosTheta = (float)Math.Cos(Angle);
            CollisionPoints = RelativeCollisionPoints
                .Select(vector => new Vector2                                       // Rotate
                {
                    X = cosTheta * (vector.X - middle.X) -
                        sinTheta * (vector.Y - middle.Y) + middle.X,
                    Y = sinTheta * (vector.X - middle.X) +
                        cosTheta * (vector.Y - middle.Y) + middle.Y
                })
                .Select(vector => MapPosition + vector - middle)
                .Select(vector => vector * 0.5f)                                    // Scale               
                .Select(vector => new Rectangle(vector.ToPoint(), new Point(2, 2)))
                .ToList();

        }

        private void SetCurrentSpriteFromControllerInput()
        {
            int direction = CameraPoint.HorizontalControllerDirection;

            // Switch the current Sprite            
            if (direction == -1) CurrentSprite = PlaneLeftSprite;
            if (direction == 1) CurrentSprite = PlaneRightSprite;
            if (direction == 0) CurrentSprite = PlaneSprite;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle viewPort)
        {
            if (IsVisible == false) return;
            var origin = new Vector2(CurrentSprite.Texture.Width / 2, CurrentSprite.Texture.Height / 2);
            spriteBatch.Draw(CurrentSprite.Texture, ScreenPosition, null, Color.White, Angle, origin, 1f, SpriteEffects.None, 1);
        }

        public SpriteFrame PlaneSprite { get; private set; }
        public SpriteFrame PlaneLeftSprite { get; private set; }
        public SpriteFrame PlaneRightSprite { get; private set; }
        public SpriteFrame CurrentSprite { get; private set; }


        public Vector2 MapPosition { get; set; } = Vector2.Zero;
        public Vector2 ScreenPosition { get; set; } = Vector2.Zero;

        public CameraPoint CameraPoint { get; private set; }
        public GameState GameState { get; private set; }
        public float Angle { get; set; } = 0;

        public Vector2 SmokePosition { get; private set; }

        public List<Rectangle> CollisionPoints { get; private set; }

        public bool IsVisible { get; set; } = true;


        private List<Vector2> RelativeCollisionPoints
        {
            get =>
                new List<Vector2>
                {
new Vector2(4, 30),
new Vector2(172, 30),
new Vector2(4, 66),
new Vector2(172, 66),
new Vector2(74, 66),
new Vector2(102, 66),
new Vector2(74, 75),
new Vector2(102, 75),
new Vector2(75, 75),
new Vector2(101, 75),
new Vector2(75, 87),
new Vector2(101, 87),
new Vector2(76, 87),
new Vector2(100, 87),
new Vector2(76, 98),
new Vector2(100, 98),
new Vector2(77, 98),
new Vector2(99, 98),
new Vector2(77, 105),
new Vector2(99, 105),
new Vector2(78, 105),
new Vector2(98, 105),
new Vector2(78, 108),
new Vector2(98, 108),
new Vector2(79, 108),
new Vector2(97, 108),
new Vector2(79, 111),
new Vector2(97, 111),
new Vector2(80, 111),
new Vector2(96, 111),
new Vector2(80, 113),
new Vector2(96, 113),
new Vector2(0, 34),
new Vector2(4, 34),
new Vector2(0, 62),
new Vector2(4, 62),
new Vector2(172, 34),
new Vector2(176, 34),
new Vector2(172, 62),
new Vector2(176, 62),
new Vector2(70, 24),
new Vector2(106, 24),
new Vector2(70, 30),
new Vector2(106, 30),
new Vector2(69, 15),
new Vector2(107, 15),
new Vector2(69, 23),
new Vector2(107, 23),
new Vector2(68, 10),
new Vector2(107, 10),
new Vector2(68, 14),
new Vector2(107, 14),
new Vector2(72, 7),
new Vector2(103, 7),
new Vector2(72, 9),
new Vector2(103, 9),
new Vector2(68, 5),
new Vector2(104, 5),
new Vector2(68, 5),
new Vector2(104, 5),
new Vector2(69, 4),
new Vector2(106, 4),
new Vector2(69, 4),
new Vector2(106, 4),
new Vector2(83, 2),
new Vector2(91, 2),
new Vector2(83, 3),
new Vector2(91, 3),
new Vector2(85, 0),
new Vector2(90, 0),
new Vector2(85, 1),
new Vector2(90, 1),
new Vector2(66, 123),
new Vector2(110, 123),
new Vector2(66, 128),
new Vector2(110, 128),
new Vector2(78, 114),
new Vector2(98, 114),
new Vector2(78, 122),
new Vector2(98, 122),
new Vector2(75, 116),
new Vector2(78, 116),
new Vector2(75, 123),
new Vector2(78, 123),
new Vector2(72, 118),
new Vector2(75, 118),
new Vector2(72, 123),
new Vector2(75, 123),
new Vector2(69, 120),
new Vector2(72, 120),
new Vector2(69, 123),
new Vector2(72, 123),
new Vector2(98, 115),
new Vector2(100, 115),
new Vector2(98, 123),
new Vector2(100, 123),
new Vector2(100, 117),
new Vector2(103, 117),
new Vector2(100, 123),
new Vector2(103, 123),
new Vector2(103, 119),
new Vector2(105, 119),
new Vector2(103, 123),
new Vector2(105, 123),
new Vector2(105, 120),
new Vector2(107, 120),
new Vector2(105, 123),
new Vector2(107, 123),
new Vector2(69, 8),
new Vector2(72, 8),
new Vector2(69, 10),
new Vector2(72, 10),
new Vector2(103, 8),
new Vector2(106, 8),
new Vector2(103, 10),
new Vector2(106, 10),
new Vector2(99, 3),
new Vector2(106, 3),
new Vector2(99, 3),
new Vector2(106, 3),
new Vector2(1, 32),
new Vector2(4, 32),
new Vector2(1, 34),
new Vector2(4, 34),
new Vector2(2, 31),
new Vector2(4, 31),
new Vector2(2, 34),
new Vector2(4, 34),
new Vector2(1, 62),
new Vector2(4, 62),
new Vector2(1, 64),
new Vector2(4, 64),
new Vector2(2, 64),
new Vector2(4, 64),
new Vector2(2, 65),
new Vector2(4, 65),
new Vector2(172, 31),
new Vector2(175, 31),
new Vector2(172, 34),
new Vector2(175, 34),
new Vector2(172, 62),
new Vector2(175, 62),
new Vector2(172, 65),
new Vector2(175, 65),
new Vector2(67, 121),
new Vector2(69, 121),
new Vector2(67, 123),
new Vector2(69, 123),
new Vector2(107, 122),
new Vector2(110, 122),
new Vector2(107, 123),
new Vector2(110, 123)
                };
        }

    }
}