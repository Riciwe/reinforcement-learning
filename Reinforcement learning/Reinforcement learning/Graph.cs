using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace Reinforcement_learning
{
    class Graph
    {
        Vector2 position = new Vector2(120,10);
        Texture2D backGround;
        int size = 400;                     //width and height of the graph
        public static int totalData = 40;   //number of timesteps on the x-as
        public float[,] data = new float[totalData,Global.A.Count()];
        public int currentData = 0;
        SpriteFont font;
        float min = 0, max = 0, step = 0;
        Color[] color;

        public Graph(SpriteBatch spriteBatch, SpriteFont font)
        {
            //Create the white background of the graph
            backGround = new Texture2D(spriteBatch.GraphicsDevice, size, size, false, SurfaceFormat.Color);
            Color[] data = new Color[size * size];
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                data[pixel] = Color.White;
            }
            backGround.SetData(data);
            this.font = font;

            //Create random colors for each angle
            color = new Color[Global.A.Count()];
            Random rand = new Random();
            for (int i = 0; i < color.Length; i++)
                color[i] = new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }

        public void Update()
        {
            //determine the max, min and remaining values on the y-as
            max = -10;
            min = 10;
            for (int x = 0; x < currentData; x++)
                for (int y = 0; y < Global.A.Count(); y++)
                {
                    if (data[x, y] > max) max = data[x, y];
                    if (data[x, y] < min) min = data[x, y];
                }
            max += 0.03f;
            min -= 0.03f;
            step = (max - min) / 8;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //draw the white background of the graph
            spriteBatch.Draw(backGround, position, Color.White);

            //draw the bottum numbers + description
            for (int i = 0; i <= totalData; i+=5)
                spriteBatch.DrawString(font, i.ToString(), new Vector2(position.X -5 + i * backGround.Width / totalData, position.Y + backGround.Height +5), Color.Black);
            spriteBatch.DrawString(font, "steps * " + Global.dataStep, new Vector2(position.X + backGround.Width / 2 - 50, position.Y + backGround.Height + 40), Color.Black);

            //draw the left numbers + description
            for (int i = 0; i < 8; i++)
            {
                spriteBatch.DrawString(font, (min + step * i).ToString("N2"), new Vector2(position.X-40, position.Y + backGround.Height - 10 - i * backGround.Height / 7), Color.Black);
                spriteBatch.DrawLine(new Vector2(position.X, position.Y+backGround.Height - i * backGround.Height / 7), new Vector2(position.X+backGround.Width, position.Y+backGround.Height - i * backGround.Height / 7), Color.Gray);
            }
            spriteBatch.DrawString(font, "avg.", new Vector2(position.X - 100, position.Y + backGround.Height / 2 - 20), Color.Black);
            spriteBatch.DrawString(font, "rew.", new Vector2(position.X - 100, position.Y + backGround.Height / 2 - 0), Color.Black);

            //draw the lines of the angles
            for (int angle = 0; angle < Global.A.Count(); angle++)
            {
                for (int time = 0; time < currentData - 1; time++)
                    spriteBatch.DrawLine(new Vector2(position.X + time * backGround.Width / totalData, position.Y+backGround.Height - (data[time, angle] - min) / (max - min) * backGround.Height),
                                         new Vector2(position.X + (time + 1) * backGround.Width / totalData, position.Y + backGround.Height - (data[time + 1, angle] - min) / (max - min) * backGround.Height), color[angle],2);
            }

            //draw variables
            spriteBatch.DrawString(font, "plane: " + Global.w.ToString() + "x" + Global.h.ToString(), new Vector2(position.X + backGround.Width + 50, position.Y), Color.Black);
            spriteBatch.DrawString(font, "players: " + Global.N.ToString(), new Vector2(position.X + backGround.Width + 50, position.Y + 20), Color.Black);
            spriteBatch.DrawString(font, "angle: " + Global.k.ToString(), new Vector2(position.X + backGround.Width + 50, position.Y + 40), Color.Black);
            spriteBatch.DrawString(font, "distance: " + Global.d.ToString(), new Vector2(position.X + backGround.Width + 50, position.Y + 60), Color.Black);
            spriteBatch.DrawString(font, "radius: " + Global.r.ToString(), new Vector2(position.X + backGround.Width + 50, position.Y + 80), Color.Black);
            spriteBatch.DrawString(font, "R1: " + Global.R1.ToString(), new Vector2(position.X + backGround.Width + 50, position.Y + 100), Color.Black);
            spriteBatch.DrawString(font, "R2: " + Global.R2.ToString(), new Vector2(position.X + backGround.Width + 50, position.Y + 120), Color.Black);
            spriteBatch.DrawString(font, "e: " + ((float)Global.e/100).ToString(), new Vector2(position.X + backGround.Width + 50, position.Y + 140), Color.Black);

            //draw index
            for (int i = 0; i < Global.A.Count(); i++)
            {
                spriteBatch.DrawLine(new Vector2(position.X + backGround.Width + 50, position.Y + 200 + i * 20), new Vector2(position.X + backGround.Width + 70, position.Y + 200 + i * 20), color[i], 3);
                spriteBatch.DrawString(font, i*Global.k + " degrees", new Vector2(position.X + backGround.Width + 80, position.Y + 190 + i * 20), Color.Black);
            }
        }
    }
}