using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake2
{
    class BaseObjectHandler : DrawableGameComponent
    {
        public static BaseObjectHandler Current { get; private set; }
        public Adventure a;
        public GameMain gm;

        public BaseObjectHandler() : base(GameMain.Current)
        {
            a = Adventure.Current;
            gm = GameMain.Current;
            Current = this;
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < a.objects.Count; i++)
                if (a.objects[i].enabled)
                     a.objects[i].Update(gameTime);

            base.Update(gameTime);
        }

        public void DrawMain(GameTime gameTime)
        {
            gm.GraphicsDevice.SetRenderTarget(a.rtMain);
            gm.GraphicsDevice.Clear(Color.White);
            foreach (BaseGameObject obj in a.objects)
            {
                if (obj.visible)
                {
                    obj.Draw();
                }
            }

            Adventure.Current.levelType.Draw(gameTime);
            gm.GraphicsDevice.SetRenderTarget(null);
        }

        public void DrawLight()
        {
            Texture2D back = new Texture2D(gm.GraphicsDevice, gm.Window.ClientBounds.Width, gm.Window.ClientBounds.Height);
            Color[] data = new Color[back.Width * back.Height];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.White;
            back.SetData<Color>(data);

            gm.GraphicsDevice.SetRenderTarget(a.rtLight);
            gm.GraphicsDevice.Clear(Color.Black);
            BlendState blendState = new BlendState();
            blendState.ColorSourceBlend = Blend.SourceColor;
            blendState.ColorDestinationBlend = Blend.DestinationColor;


            BlendState Multiply = new BlendState()
            {
                AlphaSourceBlend = Blend.DestinationAlpha,
                AlphaDestinationBlend = Blend.Zero,
                AlphaBlendFunction = BlendFunction.Add,
                ColorSourceBlend = Blend.DestinationColor,
                ColorDestinationBlend = Blend.Zero,
                ColorBlendFunction = BlendFunction.Add
            }; 


            gm.spriteBatch.Begin(SpriteSortMode.Immediate, blendState);

            

            foreach (BaseGameObject obj in a.objects)
            {
                if (obj.visible && obj is ILightable)
                {
                    Texture2D rtTemp = (Texture2D)a.rtLight;
                    ILightable o = obj as ILightable;
                    gm.fxLighting.CurrentTechnique = gm.fxLighting.Techniques["Technique1"];
                    gm.fxLighting.Parameters["lightColor"].SetValue(new float[] { 1f, .5f, .5f});
                    gm.fxLighting.Parameters["lightRadius"].SetValue(o.lightRadius);
                    gm.fxLighting.Parameters["lightStrength"].SetValue(o.lightStrength);
                    gm.fxLighting.Parameters["lightCoords"].SetValue(obj.position + obj.origin);
                    gm.fxLighting.Parameters["screenWidth"].SetValue(gm.settings.width);
                    gm.fxLighting.Parameters["screenHeight"].SetValue(gm.settings.height);
                    gm.fxLighting.Parameters["lightTexture"].SetValue(rtTemp);
                    gm.fxLighting.CurrentTechnique.Passes[0].Apply();
                }


            }

            gm.spriteBatch.Draw(back, new Vector2(0, 0), new Color(255, 255, 255, 0));
            

            gm.spriteBatch.End();
            gm.GraphicsDevice.SetRenderTarget(null);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawMain(gameTime);
            DrawLight();
            gm.GraphicsDevice.Clear(Color.Black);


            BlendState blendState = new BlendState();
            blendState.ColorSourceBlend = Blend.One;
            blendState.ColorDestinationBlend = Blend.InverseSourceAlpha;


            gm.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            gm.fxCombine.CurrentTechnique = gm.fxCombine.Techniques["Technique1"];
            gm.fxCombine.Parameters["lightTexture"].SetValue(a.rtLight);
            gm.fxCombine.Parameters["mainTexture"].SetValue(a.rtMain);
            gm.fxCombine.Parameters["ambientColor"].SetValue(new float[4] { 0f, 0, 0f, .98f });
            gm.fxCombine.Parameters["ambient"].SetValue(1f);
            gm.fxCombine.CurrentTechnique.Passes[0].Apply();
            gm.spriteBatch.Draw(a.rtMain, new Vector2(0, 0), Color.White);
            gm.spriteBatch.End();

            base.Draw(gameTime);            
        }
    }
}
