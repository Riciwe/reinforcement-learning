using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Reinforcement_learning
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Plane plane;
        List<Skater> skaters = new List<Skater>();
        int steps = 0;
        SpriteFont font;
        Graph graph;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
            Global.Initialize();
        }

        //initialize some objects
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            plane = new Plane(graphics.GraphicsDevice);
            for (int i = 0; i < Global.N; i++)
                skaters.Add(new Skater(i, graphics.GraphicsDevice));

            foreach (Skater s in skaters)
                s.skaters = skaters;
            
            font = Content.Load<SpriteFont>("Hud");

            graph = new Graph(spriteBatch, font);
        }
        
        protected override void Update(GameTime gameTime)
        {
            //the time counter
            Global.totalTime++;

            //let skaters perform their movement
            foreach (Skater s in skaters)
                s.Step();
            steps++;
            
            //every dataStep times: update values
            if (steps % Global.dataStep == 0)
            {
                //the average rewards
                for (int i = 0; i < Global.n; i++)
                {
                    Global.avgRewards[i] = 0;
                    foreach (Skater s in skaters)
                        Global.avgRewards[i] += s.avgRewards[i];
                    Global.avgRewards[i] = (float)Math.Round(Global.avgRewards[i] / Global.N,2);
                }
                //set them in the graph
                if (graph.currentData < Graph.totalData)
                {
                    for (int i = 0; i < Global.n; i++)
                        graph.data[graph.currentData, i] = Global.avgRewards[i];
                    graph.currentData++;
                    graph.Update();
                }
            }
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (Global.totalTime % Global.dataStep == 0)
            {
                GraphicsDevice.Clear(Color.Gray);
                graph.Draw(spriteBatch);
            }

            //Draw the plane: uncomment to see visualisation.
            //plane.Draw(spriteBatch);
            //foreach (Skater s in skaters)
            //    s.Draw(spriteBatch);
            
            spriteBatch.End();
        }
    }
}
