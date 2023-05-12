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

        public static readonly Vector2 FocusPos = new Vector2(840, 800);

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

    static class PackPlatforms
    {
        static public List<Platform> platforms = new List<Platform>()
        {
            new Platform(new Vector2(400+80, 700), 10),
            new Platform(new Vector2(600+80, 600), 10),
            new Platform(new Vector2(800+80, 460), 10),
            //new Platform(new Vector2(200, 540), 8),
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

        private bool CorrectPositionWithPlat(Vector2 pos, Platform platform) //внутри класса героя
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

        private bool wasRight = false; //нажата ли клавиша вправо///////////////////////////
        private Vector2 PressingButton(Vector2 posTest, Keys key, int speed)
        {
            wasRight = false;
            switch (key)
            {
                case Keys.Left:
                    posTest.X -= speed;
                    CanFall(posTest);
                    break;
                case Keys.Right:
                    posTest.X += speed;
                    CanFall(posTest);
                    wasRight = true;                   
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

        private bool SomethingBad = false;////////////////////////////////////////////////////////////

        /////////
        private Vector2 CorrectPosUpdate(Vector2 posTest) 
        {
            var correctPos = Pos;
            if (CorrectPositionWithAllPlat(posTest))  
            {
                if (wasRight && !Fall && Pos.X >= FocusPos.X) // Если кубик падает, то не двигать карту
                {
                    for (int i = 0; i < PackPlatforms.platforms.Count; i++)
                        PackPlatforms.platforms[i].Update(speed);
                }
                else
                    correctPos = posTest;
            }
            else
                SomethingBad = true;
            return correctPos;
        }
        //////////

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
            if (Pos == posTest)//если ничего не нажато проолжаем в том же духе
            {
                var speed_e = speed;
                if (Fall)
                    speed_e = speed / 2;
                posTest = PressingButton(posTest, lastKey, speed_e);                    
            }
            Pos = CorrectPosUpdate(posTest);
            if (SomethingBad)
            {
                Fall = true;
                posTest = Pos;
                if(lastKey == Keys.Up)
                    lastKey = Keys.S;
            }
            SomethingBad = false;
            if (Fall)    
                posTest.Y += gravity;

            Pos = CorrectPosUpdate(posTest);
            if (SomethingBad)
            {
                Fall = false;
            }
            SomethingBad = false;

            /*

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
            }*/

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

        private Character charr = new Character(Vector2.Zero);

        //public void Update(Keys[] keys)
        public void Update(int speed)
        {
            Pos.X -= speed;//charr.speed;
            l_up_p = new Vector2(Pos.X, Pos.Y);///dop
            r_down_p = new Vector2(Pos.X + Width, Pos.Y + Height);///dop

            /*if (keys.Length > 0)
            {
                switch (keys[0])
                {
                    case Keys.Right:
                        Pos.X -= charr.speed;
                        break;
                    //case Keys.Up:
                        //Pos.Y += 50;
                        //break;
                    default:
                        break;
                }
            }*/
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


        Vector2 l_up_p; //координаты левого верхнего угла платформы
        Vector2 r_down_p; //правого нижнего 

        public bool IsInPlatform(Vector2 ChPos) //метод внутри класса платформы
        {          
            var l_ClUp = new Vector2(ChPos.X, ChPos.Y); // координаты углов квадрата (героя)
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
