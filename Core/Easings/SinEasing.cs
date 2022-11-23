using Whitesnake.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whitesnake.Core.Easings
{

    internal static class SinEasing
    {
        static internal float Ease(float value) =>
            (float)Math.Sin((double)value);

        static internal float Ease(float value, float min, float max) =>
            Ease(value.NormalizeEasing(min, max));

    }
}