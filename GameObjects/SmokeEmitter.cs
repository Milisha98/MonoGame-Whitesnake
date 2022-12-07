using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Smoke.Core;
using Smoke.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Whitesnake.Core;

namespace Whitesnake.GameObjects
{
    internal class SmokeEmitter : IMonoGame
    {
        const string TextureName = "Smoke";
        private List<Smoke> _smokeParticles = new List<Smoke>();
        private GameState _gameState;
        private CameraPoint _cameraPoint;

        public SmokeEmitter(GameState gameState, CameraPoint cameraPoint)
        {
            _gameState = gameState;
            _cameraPoint = cameraPoint;
        }

        //
        // Methods
        //
        public void LoadContent(ContentManager contentManager)
        {
            var texture = contentManager.Load<Texture2D>(TextureName);
            SmokeSprite = new SpriteFrame(TextureName,
                                          texture,
                                          texture.Bounds,
                                          new Vector2(texture.Bounds.Width, texture.Bounds.Height));
        }

        public void Update(GameTime gameTime)
        {
            // Smoke is just special effects. Not essential
            if (gameTime.IsRunningSlowly) return;

            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Emit Smoke
            foreach (var smoke in _smokeParticles)
            {
                smoke.Update(delta);
            }
            _smokeParticles.RemoveAll(x => x.MarkedForDestroy);

            var newSmoke = new Smoke(MapPosition);
            newSmoke.MapPosition -= (SmokeSprite.Texture.Middle() * newSmoke.Scale);
            newSmoke.MaxDecay = SmokeDuration;
            if (EmitSmoke) _smokeParticles.Add(newSmoke);

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle viewport)
        {
            // Smoke is just special effects. Not essential
            if (gameTime.IsRunningSlowly) return;

            // Draw the Smoke      
            foreach (var smoke in _smokeParticles)
            {
                smoke.ScreenPosition = smoke.MapPosition.MapPositionToScreenPosition(viewport);
                if (smoke.ScreenPosition != null)
                {
                    DrawFrame(spriteBatch, smoke);
                }
            }
        }
        private void DrawFrame(SpriteBatch spriteBatch, Smoke smoke)
        {
            if (smoke.ScreenPosition.HasValue == false) return;

            Color color = smoke.TintColor;
            var texture = SmokeSprite.Texture;
            var scale = smoke.Scale;

            var origin = new Vector2(texture.Width / 2, texture.Height / 2);
            var rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            var newPostion = smoke.ScreenPosition.Value + origin;

            spriteBatch.Draw(texture, newPostion, rectangle, color, smoke.Angle, origin, scale, SpriteEffects.None, 1);

        }

        //
        // Properties
        //
        public SpriteFrame SmokeSprite { get; set; }

        public Vector2 MapPosition { get; set; }

        public bool EmitSmoke { get; set; } = true;

        public int SmokeDuration { get; set; } = 200;

    }
}
