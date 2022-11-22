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

internal class Mech : IMonoGame, IVisibleObject
{
    const string BodyTextureName = "Mech/body";
    const string FootTextureName = "Mech/foot";
    const string LaserGunTextureName = "Mech/laser-gun";
    const string RocketLauncherTextureName = "Mech/rocket-launcher";

    public void LoadContent(ContentManager contentManager)
    {
        // Load the Camera Point
        Texture2D bodyTexture = contentManager.Load<Texture2D>(BodyTextureName);
        BodySprite = new SpriteFrame(BodyTextureName,
                                       bodyTexture,
                                       bodyTexture.Bounds,
                                       new Vector2(bodyTexture.Bounds.Width, bodyTexture.Bounds.Height));
        
        Texture2D footTexture = contentManager.Load<Texture2D>(FootTextureName);
        FootSprite = new SpriteFrame(FootTextureName,
                                       footTexture,
                                       footTexture.Bounds,
                                       new Vector2(footTexture.Bounds.Width, footTexture.Bounds.Height));

        // TODO: Animation?
        Texture2D laserGunTexture = contentManager.Load<Texture2D>(LaserGunTextureName);
        LaserGunSprite = new SpriteFrame(LaserGunTextureName,
                                       laserGunTexture,
                                       laserGunTexture.Bounds,
                                       new Vector2(laserGunTexture.Bounds.Width, laserGunTexture.Bounds.Height));

        // TODO: Animation?
        Texture2D rocketLauncherTexture = contentManager.Load<Texture2D>(RocketLauncherTextureName);
        RocketLauncherSprite = new SpriteFrame(RocketLauncherTextureName,
                                       rocketLauncherTexture,
                                       rocketLauncherTexture.Bounds,
                                       new Vector2(rocketLauncherTexture.Bounds.Width, rocketLauncherTexture.Bounds.Height));

    }

    public void Update(GameTime gameTime)
    {
        
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Rectangle viewPort)
    {
        var sourceRectangle = viewPort;
        spriteBatch.Draw(BodySprite.Texture, Vector2.Zero, sourceRectangle, Color.White);
    }

    public SpriteFrame BodySprite { get; private set; }
    public SpriteFrame FootSprite { get; private set; }
    public SpriteFrame LaserGunSprite { get; private set; }
    public SpriteFrame RocketLauncherSprite { get; private set; }

    public Vector2 MapPosition { get; set; } = Vector2.Zero;
    public Vector2 ScreenPosition { get; set; } = Vector2.Zero;

}
