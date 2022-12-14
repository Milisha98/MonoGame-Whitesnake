using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Smoke.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whitesnake.GameObjects
{

    internal class ScoreBoard : IMonoGame, IVisibleObject
    {
        private SpriteFont _spriteFont;

        public ScoreBoard(GameState gameState)
        {
            GameState = gameState;
        }

        public void LoadContent(ContentManager contentManager)
        {
            _spriteFont = contentManager.Load<SpriteFont>("Text");
        }

        public void Update(GameTime gameTime)
        {
            if (GameState.IsDemoMode || 
                GameState.IsFinished) return;

            double milliseconds = gameTime.ElapsedGameTime.TotalMilliseconds;
            Milliseconds += milliseconds;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle viewPort)
        {           
            if (GameState.IsDemoMode)
            {
                spriteBatch.DrawString(_spriteFont, $"Press Space to Exit demo", ScreenPosition - new Vector2(120, 0), Color.Red, 0f, Vector2.Zero, 2, SpriteEffects.None, 0);
                return;
            }

            spriteBatch.DrawString(_spriteFont, $"Score: {Score}", ScreenPosition, Color.Yellow, 0f, Vector2.Zero, 2, SpriteEffects.None, 0);
            if (GameState.IsFinished)
                spriteBatch.DrawString(_spriteFont, $"Press Enter to Restart", ScreenPosition - new Vector2(90, -40), Color.Red, 0f, Vector2.Zero, 2, SpriteEffects.None, 0);
    
        }

        public GameState GameState { get; private set; }
        public Vector2 MapPosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 ScreenPosition { get => new Vector2((Global.RenderWidth / 2) - 48, 20); set => throw new NotImplementedException(); }
        public double Milliseconds { get; set; }
        public bool IsVisible { get => GameState.IsDemoMode == false; }

        public int Score { get => (int)Milliseconds / 1000; }
    }
}