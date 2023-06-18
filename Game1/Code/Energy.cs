using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SquareGame
{
    internal class Energy : Entity
    {
        public static Texture2D texture2D { get; set; }
        public static double energy;
        public static int size = 9;
        public void Draw(GameTime gameTime)
        {
            for(int i = 0; i < (int)Math.Round(100*energy); i++)
                SpriteBatch.Draw(texture2D, new Vector2(MapWidth-size, (MapHeight - size) - i*size), Color.BlueViolet);
        }
    }
}