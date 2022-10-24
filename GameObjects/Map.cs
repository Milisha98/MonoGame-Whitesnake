using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Smoke.Core;
using Smoke.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapi.GameObjects;

internal class Map : IMonoGame, IVisibleObject
{
    const string TextureName = "Map";

    public void LoadContent(ContentManager contentManager)
    {
        // Load the Camera Point
        Texture2D texture = contentManager.Load<Texture2D>(TextureName);
        MapSprite = new SpriteFrame(TextureName,
                                       texture,
                                       texture.Bounds,
                                       new Vector2(texture.Bounds.Width, texture.Bounds.Height));
    }

    public void Update(GameTime gameTime)
    {
        // Do Nothing
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle viewPort)
    {
        var sourceRectangle = viewPort;
        spriteBatch.Draw(MapSprite.Texture, Vector2.Zero, sourceRectangle, Color.White);
    }

    public SpriteFrame MapSprite { get; private set; }

    public Vector2 MapPosition { get; set; } = Vector2.Zero;
    public Vector2 ScreenPosition { get; set; } = Vector2.Zero;

}
