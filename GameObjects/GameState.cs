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

        internal bool IsFinished { get; set; } = false;

        internal int Score { get; set; } = 0;

        internal int Level
        {
            get
            {
                if (Score < 10) return 2000;
                if (Score < 20) return 1500;
                if (Score < 30) return 1000;
                return 500;
            }
        }

    }
}
