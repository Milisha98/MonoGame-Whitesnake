using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whitesnake.Core
{
    internal static class KeyboardController
    {
        public static Vector2 CheckInput()
        {
            var keyboardState = Keyboard.GetState();
            var v = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Left) ||
                keyboardState.IsKeyDown(Keys.A))
            {
                v += new Vector2(-1, 0);
            }

            if (keyboardState.IsKeyDown(Keys.Right) ||
                keyboardState.IsKeyDown(Keys.D))
            {
                v += new Vector2(1, 0);
            }

            if (keyboardState.IsKeyDown(Keys.Up) ||
                keyboardState.IsKeyDown(Keys.W))
            {
                v += new Vector2(0, -1);
            }

            if (keyboardState.IsKeyDown(Keys.Down) ||
                keyboardState.IsKeyDown(Keys.S))
            {
                v += new Vector2(0, 1);
            }

            if (v != Vector2.Zero) v.Normalize();

            return v;
        }
    }
}
