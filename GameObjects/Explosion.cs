﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Smoke.Core.Sprites;
using Smoke.Core;
using Smoke.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whitesnake.Core;
using Whitesnake.Core.Sprites;

namespace Whitesnake.GameObjects
{

    internal class Explosion : IMonoGame, IAnimation
    {
        private readonly TexturePackLoader _tileSetLoader = new TexturePackLoader();
        private SpriteSheet _explosionSheet;

 
        public void LoadContent(ContentManager contentManager)
        {
            var rnd = new Random();
            _explosionSheet = _tileSetLoader.LoadContent(contentManager, "Explosion3");
            Angle = MathHelper.ToRadians(rnd.Next(0, 359));
        }

        public void Update(GameTime gameTime)
        {
            if (!IsPlaying || Duration.TotalMilliseconds == 0) return;
            var delta = gameTime.ElapsedGameTime.TotalMilliseconds;

            // Increase the current
            CurrentMilliseconds += (long)delta;

            // If I can calcuate how long a frame takes
            int millisecondsPerFrame = (int)Duration.TotalMilliseconds / Frames;
            Frame = (int)(CurrentMilliseconds / millisecondsPerFrame);
            if (Frame >= Frames)
            {
                Frame = Frames;
                Stop();
                IsVisible = false;
            }

        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle viewPort)
        {
            if (IsVisible == false) return;
            if (Frame >= Frames) return;
            var frame = _explosionSheet.SpriteList[Frame];
            ScreenPosition = new Vector2(Global.RenderWidth / 2, Global.RenderHeight / 2);
            DrawFrame(spriteBatch, frame);
        }

        private void DrawFrame(SpriteBatch spriteBatch, SpriteFrame frame)
        {
            var texture = frame.Texture;

            var origin = frame.Size / 2;
            var rectangle = frame.SourceRectangle;
            var newPostion = ScreenPosition + origin;

            spriteBatch.Draw(texture, newPostion, rectangle, Color.White, Angle, origin, Scale, SpriteEffects.None, 1);

        }


        #region IAnimation

        public long CurrentMilliseconds { get; set; } = 0;
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(0.8);

        public bool IsPlaying { get; set; } = false;

        public void Start()
        {
            IsVisible = true;
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false; 
            IsVisible = false;
        }

        public void Reset()
        {
            CurrentMilliseconds = 0;
            Frame = 0;
        }

        #endregion

        public float Scale { get; set; } = 4f;
        public Vector2 MapPosition { get; set; } = Vector2.Zero;
        public Vector2 ScreenPosition { get; set; } = Vector2.Zero;

        public bool IsVisible { get; set; } = false;
        public int Frame { get; set; } = 0;
        public int Frames { get => _explosionSheet.SpriteList.Count(); }

        float Angle { get; set; }

    }
}
