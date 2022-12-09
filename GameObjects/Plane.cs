using Whitesnake.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Smoke.Core;
using Smoke.Sprites;


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
            SetCurrentSpriteFromControllerInput();          

            // Set the Location
            Angle = CameraPoint.Angle;
            ScreenPosition = CameraPoint.ScreenPosition;
            MapPosition = CameraPoint.MapPosition;

            // Smoke Emitter
            Vector2 planeMiddle = this.MapPosition - CurrentSprite.Texture.Middle();
            var smokeDelta = CameraPoint.AngleVector * (CurrentSprite.Texture.Height / 2);
            SmokePosition = planeMiddle - smokeDelta;

        }

        private void SetCurrentSpriteFromControllerInput()
        {
            int direction = CameraPoint.ControllerDirection;

            // Switch the current Sprite            
            if (direction == -1) CurrentSprite = PlaneLeftSprite;
            if (direction == 1) CurrentSprite = PlaneRightSprite;
            if (direction == 0) CurrentSprite = PlaneSprite;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle viewPort)
        {
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
    }
}