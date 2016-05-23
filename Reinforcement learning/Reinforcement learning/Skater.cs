using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reinforcement_learning
{
    class Skater
    {
        int id;
        public float[] avgRewards = new float[Global.n];   //array for the average rewards gained
        int[] rewards = new int[Global.n];                 //array that stores how many times the action was done
        Vector2 position;
        Texture2D rect;
        public List<Skater> skaters = new List<Skater>();

        public Skater(int id, GraphicsDevice graphics)
        {
            this.id = id;
            for (int i = 0; i < avgRewards.Count(); i++)
            {
                avgRewards[i] = 0;
                rewards[i] = 0;
            }
            //choose random initial position and color
            position = new Vector2(Global.rand.Next(0, Global.w), Global.rand.Next(0, Global.h));

            //set 3x3 image for drawing
            rect = new Texture2D(graphics,3,3);
            Color[] data = new Color[3 * 3];
            for (int i = 0; i < data.Length; i++) data[i] = Color.Black;
            rect.SetData(data);
        }

        public void Step()
        {
            //pick action
            int action = -1; //index of A
        
            int random = Global.rand.Next(100);
            if (random >= Global.e)    //1-e chance: take best
            {
                //simply loop over all rewards. if equal: randomly pick 1
                float bestValue = avgRewards[0];
                int bestIndex = 0;
                for (int i = 1; i<avgRewards.Count(); i++)
                    if (avgRewards[i] > bestValue || (avgRewards[i] == bestValue && Global.rand.Next(2) == 0))
                    {
                        bestValue = avgRewards[i];
                        bestIndex = i;
                    }
                action = bestIndex;
            }
            else  //e chance: take random action
            {
                action = Global.rand.Next(rewards.Count());
            }
        
            //determine new position
            Vector2 newPos = new Vector2(position.X + Global.d * (float)(Math.Cos(Global.A[action])), 
                                         position.Y + Global.d * (float)(Math.Sin(Global.A[action])));

            //going of the map: appear on other side
            if (newPos.X < 0) newPos.X += Global.w;
            if (newPos.X > Global.w) newPos.X -= Global.w;
            if (newPos.Y < 0) newPos.Y += Global.h;
            if (newPos.Y > Global.h) newPos.Y -= Global.h;

            //check collision
            bool collision = false;
            foreach (Skater s in skaters)
                if (this != s && Distance(newPos, s.position) <= Global.r)
                {
                    collision = true;
                    break;
                }
            float reward = 0;
            //without collision: move and positive reward
            if (!collision)
            {
                reward = Global.R1;
                position = newPos;
            }
            //with collision: don't move, negative reward
            else
            {
                reward = Global.R2;
            }

            avgRewards[action] = avgRewards[action] + (0.05f) * (reward - avgRewards[action]);
        }

        //The euclidian distance between 2 points
        public float Distance(Vector2 a, Vector2 b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rect, position, Color.Black);
        }
    }
}
