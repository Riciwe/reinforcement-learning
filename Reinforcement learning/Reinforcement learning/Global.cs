using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Reinforcement_learning
{
    public static class Global
    {
        //width and height of plane
        public static int w = 200;
        public static int h = 200;
        
        //player info
        public static int N = 29;               //number of players
        public static int k = 45;               //angle
        public static int n = 360 / k;          //number of angles
        public static double[] A = new double[n]; //array of angles
        public static int d = 5;                //speed 
        public static int r = 3;                //collision radius

        //reinforcement info
        public static int R1 = 4;               //positive reward
        public static int R2 = -1;              //negative reward
        public static int e = 0;                //the percentage to choose a random action (e-greedy)

        //variables to keep track of data
        public static int dataStep = 100;       //amount of steps untill data is stored again
        public static int totalTime = 0;        //the total number of steps taken
        public static float[] avgRewards = new float[n];    //keeps track of average rewards

        public static void Initialize()
        {
            for (int i = 0; i < n; i++)
            {
                A[i] = i * k * Math.PI /180;
                avgRewards[i] = 0;
            }
        }

        public static Random rand = new Random();
        
        //Code for drawing lines in the graph
        private static Texture2D _texture;
        private static Texture2D GetTexture(SpriteBatch spriteBatch)
        {
            if (_texture == null)
            {
                _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                _texture.SetData(new[] { Color.White });
            }

            return _texture;
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness = 1f)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }

        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness = 1f)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(GetTexture(spriteBatch), point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }

}
}
