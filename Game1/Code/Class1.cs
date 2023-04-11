using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    class Entity
    {
        public static int Width, Height;
        public static SpriteBatch SpriteBatch { get; set; }
        static public Character Character { get; set; }

        public static void InIt(SpriteBatch spriteBatch, int width, int height)
        {
            SpriteBatch = spriteBatch;
            Width = width;
            Height = height;
            Character = new Character(new Vector2(0, 0));
        }
        
        static public void Draw()
        {
            Character.Draw();
        }
    }


    class Character: Entity
    {
        Color color = Color.Black;
        public static Texture2D texture2D { get; set; }
        Vector2 Pos;
        public Character(Vector2 pos)
        {
            Pos = pos;
        }

        public void Draw()
        {
            Entity.SpriteBatch.Draw(texture2D, Pos = new Vector2(0, Height-200), color);
        }
    }
}
