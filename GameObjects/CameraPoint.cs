using Mapi.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Smoke.Core;
using Smoke.Sprites;
using static System.Formats.Asn1.AsnWriter;

namespace Mapi.GameObjects;

internal class CameraPoint : IMonoGame, IVisibleObject
{
    const string TextureName = "Camera";
    const float Velocity = 20f;

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
        ScreenPosition = this.MapPosition.MapPositionToScreenPosition(viewPort);
        spriteBatch.Draw(CameraSprite.Texture, viewPort.Center.ToVector2(), Color.White);
    }


    public void Update(GameTime gameTime)
    {
        var newPosition = this.MapPosition;
        var keyboardState = Keyboard.GetState();
        float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds / Global.FPS;

        if (keyboardState.IsKeyDown(Keys.Left) ||
            keyboardState.IsKeyDown(Keys.A))
        {            
            newPosition.X -= Velocity * delta;
        }

        if (keyboardState.IsKeyDown(Keys.Right) ||
            keyboardState.IsKeyDown(Keys.D))
        {
            newPosition.X += Velocity * delta;
        }

        if (keyboardState.IsKeyDown(Keys.Up) ||
            keyboardState.IsKeyDown(Keys.W))
        {
            newPosition.Y -= Velocity * delta;
        }

        if (keyboardState.IsKeyDown(Keys.Down) ||
            keyboardState.IsKeyDown(Keys.S))
        {
            newPosition.Y += Velocity * delta;
        }
        
        if ((newPosition.X < 0) || (newPosition.Y < 0)) return;
        if ((newPosition.X + Global.RenderWidth) > Global.MapWidth) return;
        if ((newPosition.Y + Global.RenderHeight) > Global.MapHeight) return;



        this.MapPosition = newPosition;

    }

    #region IVisibleObject

    public Vector2 MapPosition { get; set; }
    public Vector2 ScreenPosition { get; set; }

    #endregion

    public SpriteFrame CameraSprite { get; set; }
}
