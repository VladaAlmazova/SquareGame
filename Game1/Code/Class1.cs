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
            Character = new Character(new Vector2(50, 900 - 113 - 50));
        }


        static public void Draw(GameTime gameTime)
        {
            Character.Draw(gameTime);
        }
    }


    class Character: Entity
    {
        Color color = Color.White;
        public static Texture2D texture2D { get; set; }
        public Vector2 Pos;
        public Character(Vector2 pos)
        {
            Pos = pos;
        }

        public void Update(Keys[] keys)
        {
            if (keys.Length > 0)
            {
                switch (keys[0])
                {
                    case Keys.Left:
                        Pos.X -= 50;
                        break;
                    case Keys.Right:
                        Pos.X += 50;
                        break;
                    case Keys.Up:
                        Pos.Y -= 50;
                        break;
                    default:
                        break;
                }
            }
            else
                if (Pos.Y + 10 <= 737)
                Pos.Y += 10;
        }

        public void Draw(GameTime gameTime)
        {
            //Entity.SpriteBatch.Draw(texture2D, Pos = new Vector2(0, Height-200), color);
            Entity.SpriteBatch.Draw(texture2D, Pos, color);
        }
    }
}
