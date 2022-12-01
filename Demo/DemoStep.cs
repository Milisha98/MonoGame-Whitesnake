using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whitesnake.Demo
{
    internal class DemoStep
    {
        public DemoStep(int sequence, float velocity, float rotation, int duration, bool smokeOn = true, int maxDecay = 0)
        {
            Sequence = sequence;
            Velocity = velocity;
            Rotation = rotation;
            SmokeOn = smokeOn;
            MaxDecay = maxDecay == 0 ? 1000 - sequence : MaxDecay;
            Duration = duration;
            Elapsed = 0;
        }

        public int Sequence { get; set; }
        public float Velocity { get; set; }
        public float Rotation { get; set; }
        public bool SmokeOn { get; set; } = true;
        public int MaxDecay { get; set; }
        public int Duration { get; set; }
        public int Elapsed { get; set; }

    }
}
