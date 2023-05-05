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
        private const int size = 120;
        const int indent = 0;//10; кратное 5

        public static int Width, Height;
        public static SpriteBatch SpriteBatch { get; set; }
        static public Character Character { get; set; }

        public static void InIt(SpriteBatch spriteBatch, int width, int height)
        {
            SpriteBatch = spriteBatch;
            Width = width;
            Height = height;
            Character = new Character(new Vector2(200, 800));
        }


        static public void Draw(GameTime gameTime)
        {
            Character.Draw(gameTime);
            for(int i = 0; i < PackPlatforms.platforms.Count; i++)
                PackPlatforms.platforms[i].Draw(gameTime);
        }

        public static bool IsInMap(Vector2 pos)
        {
            return !(pos.X < indent || pos.X + size > Width - indent || pos.Y < indent || pos.Y + size > Height - indent);    
        }
    }

    class PackPlatforms: Entity
    {
        static public List<Platform> platforms = new List<Platform>()
        {
            new Platform(new Vector2(400, 700), 10),
            new Platform(new Vector2(600, 600), 10),
            new Platform(new Vector2(800, 460), 10),
        };
    }

    class Character: Entity
    {
        Color color = Color.White;
        public static Texture2D texture2D { get; set; }
        public Vector2 Pos;
        public const int size = 120;
        public int speed = 10;
        public int gravity = 10;
        public Character(Vector2 pos)
        {
            Pos = pos;
        }

        public Keys lastKey = Keys.S;
        private bool Fall = false;

        private bool CorrectPositionWithPlat(Vector2 pos, Platform platform)
        {
            return IsInMap(pos) && !platform.IsInPlatform(pos);
        }

        private bool CorrectPositionWithAllPlat(Vector2 pos)
        {
            for (int i = 0; i < PackPlatforms.platforms.Count; i++)
                if (!CorrectPositionWithPlat(pos, PackPlatforms.platforms[i]))
                    return false;
            return true;
        }

        private void CanFall(Vector2 pos) //3plat
        {
            pos.Y += 5;
            if(CorrectPositionWithAllPlat(pos))
                Fall = true;
            pos.Y -= 5;

            if (lastKey == Keys.Up)
                Fall = true;
        }

        private Vector2 PressingButton(Vector2 posTest, Keys key, int speed)
        {
            switch (key)
            {
                case Keys.Left:
                    posTest.X -= speed;
                    CanFall(posTest);
                    break;
                case Keys.Right:
                    posTest.X += speed;
                    CanFall(posTest);
                    break;
                case Keys.Up:
                    posTest.Y -= speed;
                    break;
                default:
                    key = lastKey;
                    break;
            }
            lastKey = key;
            return posTest;
        }

        public void Update(Keys[] keys)
        {
            var posTest = Pos;
            if(!Fall)
            {
                if (keys.Length > 0)
                {
                    posTest = PressingButton(posTest, keys[0], speed);
                }
            }            
            if (Pos == posTest)
            {
                var speed_e = speed;
                if (Fall)
                    speed_e = speed / 2;
                posTest = PressingButton(posTest, lastKey, speed_e);                    
            }

            if (CorrectPositionWithAllPlat(posTest))       //3 plat
            {
                Pos = posTest;
            }

            else
            {
                Fall = true;
                posTest = Pos;
                if(lastKey == Keys.Up)
                    lastKey = Keys.S;
            }
            if (Fall)    
                posTest.Y += gravity;

            if (CorrectPositionWithAllPlat(posTest))       //3 plat
            {
                Pos = posTest;
            }

            else
            {
                Fall = false;
            }
                
        }

        public void Draw(GameTime gameTime)
        {
            Entity.SpriteBatch.Draw(texture2D, Pos, color);
        }
    }

    class Platform : Entity
    {
        Color color = Color.White;
        public static Texture2D texture2D { get; set; }
        public Vector2 Pos;
        public int Width = 20;
        public int Height = 20;
        private int countPlat = 0;
        public Platform(Vector2 pos, int count)
        {
            Pos = pos;
            countPlat = count;
            Width = Width * count;

            l_up_p = new Vector2(Pos.X, Pos.Y);
            r_down_p = new Vector2(Pos.X + Width, Pos.Y + Height);
        }

        public void Update(Keys[] keys)
        {
            if (keys.Length > 0)
            {
                switch (keys[0])
                {
                    /*case Keys.Right:
                        Pos.X -= 50;
                        break;
                    case Keys.Up:
                        Pos.Y += 50;
                        break;*/
                    default:
                        break;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            var ostPos = Pos;
            for(var i = 0; i < countPlat; i++)
            {                
                Entity.SpriteBatch.Draw(texture2D, ostPos, color);
                ostPos.X += Height;
            }
        }


        Vector2 l_up_p;
        Vector2 r_down_p;

        public bool IsInPlatform(Vector2 ChPos)
        {          
            var l_ClUp = new Vector2(ChPos.X, ChPos.Y);
            var r_CrDow = new Vector2(ChPos.X + Character.size, ChPos.Y + Character.size);
            if ((l_up_p.X < r_CrDow.X && r_CrDow.X <= r_down_p.X) || (l_up_p.X <= l_ClUp.X && l_ClUp.X < r_down_p.X))
            {
                if (l_up_p.Y < r_CrDow.Y && r_CrDow.Y <= r_down_p.Y)
                    return true;
                if (l_up_p.Y <= l_ClUp.Y && l_ClUp.Y < r_down_p.Y)
                    return true;
                if (l_ClUp.Y <= l_up_p.Y && r_down_p.Y <= r_CrDow.Y)
                    return true;
            }
            return false;
        }
    }
}
