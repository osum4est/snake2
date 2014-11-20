using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    class _MouseFollowerObject : BaseGameObject, ILightable
    {
        public Color lightColor { get; set; }
        public float lightStrength { get; set; }
        public float lightRadius { get; set; }

        public _MouseFollowerObject() : base() { }
        public override void SnakeCollision()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            position.X = Mouse.GetState().X - 16;
            position.Y = Mouse.GetState().Y - 16;
            base.Update(gameTime);
        }

        public override void Initialize()
        {
            lightColor = new Color(255, 0, 0);
            lightStrength = .6f;
            lightRadius = 200;

            Sprite = Sprites.firefly;
            base.Initialize();
        }
    }
}
