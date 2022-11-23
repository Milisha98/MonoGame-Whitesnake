using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whitesnake.Core.Easings
{
    internal interface IEasing
    {
        float Ease(float value);
        float Ease(float value, float min, float max);
    }
}
