using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    class EntityFirefly : BaseGameEnemy, ILightable
    {
        public Color lightColor { get; set; }
        public float lightStrength { get; set; }
        public float lightRadius { get; set; }

        public EntityFirefly() : base() { }
        public override void SnakeCollision()
        {
            
        }

        public override void Initialize()
        {
            lightColor = Color.Yellow;
            lightStrength = 2f;
            lightRadius = 250;

            Sprite = Sprites.firefly;
            base.Initialize();
        }
    }
}
