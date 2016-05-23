using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Reinforcement_learning
{
    class Plane
    {
        Texture2D rect;

        //plane used to draw the torus for the skaters
        public Plane(GraphicsDevice graphics)
        {
            rect = new Texture2D(graphics, Global.w, Global.h);

            Color[] data = new Color[Global.w * Global.h];
            for (int i = 0; i < data.Length; i++) data[i] = Color.White;
            rect.SetData(data);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rect, Vector2.Zero, Color.White);
        }
    }
}
