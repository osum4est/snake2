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
            lightColor = new Color(255, 255, 0);
            lightStrength = .6f;
            lightRadius = 100;

            Sprite = Sprites.firefly;
            base.Initialize();
        }
    }
}
