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

        /// <summary>
        /// .5 for best light affects!
        /// </summary>
        float lightStrength { get; set; }
    }
}
