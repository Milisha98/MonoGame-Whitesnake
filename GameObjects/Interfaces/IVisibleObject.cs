using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapi.GameObjects;

internal interface IVisibleObject
{
    Vector2 MapPosition { get; set; }
    Vector2 ScreenPosition { get; set; }
}
