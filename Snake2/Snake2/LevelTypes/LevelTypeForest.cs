﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake2
{
    class LevelTypeForest : BaseLevelType
    {
        public LevelTypeForest()
        {
            ambientLight = new Color(10, 15, 10, 250);
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}
