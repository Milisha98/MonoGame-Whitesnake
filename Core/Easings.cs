using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whitesnake.Core
{
    internal class Easings
    {
        internal static double EaseInOutQuad(double x) =>
            x < 0.5 ? 2 * x * x
                    : 1 - Math.Pow(-2 * x + 2, 2) / 2;
    }
}
