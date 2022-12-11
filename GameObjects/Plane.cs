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

        /*
-- Box Coordinates
4,30 / 172,66
74,66 / 102,75
75,75 / 101,87
76,87 / 100,98
77,98 / 99,105
78,105 / 98,108
79,108 / 97,111
80,111 / 96,113
0,34 / 4,62
172,34 / 176,62
70,24 / 106,30
69,15 / 107,23
68,10 / 107,14
72,7 / 103,9
68,5 / 104,5
69,4 / 106,4
83,2 / 91,3
85,0 / 90,1
66,123 / 110,128
78,114 / 98,122
75,116 / 78,123
72,118 / 75,123
69,120 / 72,123
98,115 / 100,123
100,117 / 103,123
103,119 / 105,123
105,120 / 107,123
69,8 / 72,10
103,8 / 106,10
99,3 / 106,3
1,32 / 4,34
2,31 / 4,34
1,62 / 4,64
2,64 / 4,65
172,31 / 175,34
172,62 / 175,65
67,121 / 69,123
107,122 / 110,123        
        */

    }
}