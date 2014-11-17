using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    interface ILightable
    {
        Color lightColor { get; set; }
        float lightRadius { get; set; }
        float lightStrength { get; set; }
    }

}
