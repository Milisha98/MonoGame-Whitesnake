using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Smoke.Core;
using Smoke.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Whitesnake.Core;

namespace Whitesnake.GameObjects
{

    internal class BLExEmitter : IMonoGame
    {
        const string TextureName = "blex";
        private List<BLEx> _blexList = new List<BLEx>();
        private GameState _gameState;
        private CameraPoint _cameraPoint;

        public BLExEmitter(GameState gameState, CameraPoint cameraPoint)
        {
            _gameState = gameState;
            _cameraPoint = cameraPoint;
        }

        public void LoadContent(ContentManager contentManager)
        {
            Texture2D bodyTexture = contentManager.Load<Texture2D>(TextureName);
            BLExSprite = new SpriteFrame(TextureName,
                                     bodyTexture,
                                     bodyTexture.Bounds,
                                     new Vector2(bodyTexture.Bounds.Width, bodyTexture.Bounds.Height));
        }

        public void Update(GameTime gameTime)
        {
            if (_gameState.IsDemoMode || _gameState.IsFinished) return;

            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            foreach (var blex in _blexList)
            {
                blex.Update(delta);
            }

            // Spring Cleaning
            _blexList.RemoveAll(x => x.MarkedForDestroy);

            // Determine when to create new
            LastAdded += delta;
            if (_blexList.Count() < (_gameState.Score * 10))
            {
                var newBlex = new BLEx(_cameraPoint);
                _blexList.Add(newBlex);
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle viewPort)
        {
            foreach (var blex in _blexList)
            {
                blex.ScreenPosition = blex.MapPosition.MapPositionToScreenPosition(viewPort);
                if (blex.ScreenPosition != null)
                {
                    DrawFrame(spriteBatch, blex);
                }
            }
        }


        private void DrawFrame(SpriteBatch spriteBatch, BLEx blex)
        {
            Color color = Color.White;
            var texture = BLExSprite.Texture;
            var scale = 1.0f;

            var origin = new Vector2(texture.Width / 2, texture.Height / 2);
            var rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            var newPostion = blex.ScreenPosition + origin;

            spriteBatch.Draw(texture, newPostion, rectangle, color, blex.Angle, origin, scale, SpriteEffects.None, 1);

        }

        public SpriteFrame BLExSprite { get; private set; }

        public int BLExCount { get => _blexList.Count(); }

        public double LastAdded { get; private set; } = 0;

    }
}
