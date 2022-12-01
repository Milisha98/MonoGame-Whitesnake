using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whitesnake.Demo;

namespace Whitesnake.GameObjects
{
    internal class GameState
    {
        internal bool IsDemoMode { get; set; } = true;

        internal bool PauseGame { get; set; } = false;

        internal bool ShowDebugging { get; set; } = true;

        internal DemoStep DemoStep { get; set;}
    }
}
