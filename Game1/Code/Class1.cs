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
        private Vector2 pos_char = new Vector2(50, 900 - 113 - 50);
        private const int size = 122;
        const int indent = 10;

        public static int Width, Height;
        public static SpriteBatch SpriteBatch { get; set; }
        static public Character Character { get; set; }

        static public Platform Platform1 { get; set; }

        public static void InIt(SpriteBatch spriteBatch, int width, int height)
        {
            SpriteBatch = spriteBatch;
            Width = width;
            Height = height;
            Platform1 = new Platform(new Vector2(578, 550), 10);
            Character = new Character(new Vector2(indent, Height - size- indent), Platform1);
            //Character = new Character(Vector2.Zero, Platform1);
        }


        static public void Draw(GameTime gameTime)
        {
            Character.Draw(gameTime);
            Platform1.Draw(gameTime);
        }

        public static bool IsInMap(Vector2 pos)
        {
            return !(pos.X < indent || pos.X > Width - size || pos.Y < indent || pos.Y > Height - size - indent);    
        }
    }


    class Character: Entity
    {
        Color color = Color.White;
        public static Texture2D texture2D { get; set; }
        public Vector2 Pos;
        private Platform Platform = new Platform(Vector2.Zero, 0);
        public List<Platform> listPlat = new List<Platform>()
        {
            new Platform(new Vector2(800, 550), 15),
            new Platform(new Vector2(800, 300), 11),
            new Platform(new Vector2(700, 250), 10),
        };
        public const int size = 122;
        public int speed = 10;
        public Character(Vector2 pos, Platform platform)
        {
            Pos = pos;
            Platform = platform;
        }

        private bool VectorsIntersect(Tuple<Vector2, Vector2> vert, Tuple<Vector2, Vector2>gor)
        {
            return gor.Item1.X < vert.Item1.X && vert.Item1.X < gor.Item2.X &&
                vert.Item1.Y < gor.Item1.Y && gor.Item1.Y < vert.Item2.Y;
        }


        public Keys lastKey = Keys.S;
        private bool Fall = false;

        private void CanFall(Vector2 pos)
        {
            pos.Y += 5;
            if (IsInMap(pos) && !Platform.IsInPlatform(pos))
                Fall = true;
            pos.Y -= 5;
        }
        /*private void CanFall(Vector2 pos)
        {
            pos.Y += 5;
            foreach(var plat in listPlat)
            {
                if (IsInMap(pos) && !plat.IsInPlatform(pos))
                {
                    Fall = true;
                    break;
                }  
            }            
            pos.Y -= 5;
        }*/


        public void Update(Keys[] keys)
        {
            var posTest = Pos;
            if(!Fall)
            {
                if (keys.Length > 0)
                {
                    switch (keys[0])
                    {
                        case Keys.Left:
                            if (lastKey == Keys.Up)
                                Fall = true;
                            posTest.X -= speed;
                            lastKey = Keys.Left;
                            CanFall(posTest);
                            break;
                        case Keys.Right:

                            if (lastKey == Keys.Up)
                                Fall = true;
                            posTest.X += speed;
                            lastKey = Keys.Right;
                            CanFall(posTest);
                            break;
                        case Keys.Up:
                            posTest.Y -= speed;
                            lastKey = Keys.Up;
                            break;
                        default:
                            break;
                    }
                }
            }
            var speed_e = speed;
            if (Pos == posTest)
            {
                if (Fall)
                    speed_e = speed / 2;
                /*else
                    speed_e = speed;*/
                switch (lastKey)
                {
                    case Keys.Left:
                        posTest.X -= speed_e;
                        CanFall(posTest);
                        break;
                    case Keys.Right:
                        posTest.X += speed_e;
                        CanFall(posTest);
                        break;
                    case Keys.Up:
                        posTest.Y -= speed_e;
                        break;
                    /*case Keys.Down:
                        posTest.Y += 20;
                        break;*/
                    default:
                        break;
                }
                
                    
            }
            if (IsInMap(posTest) && !Platform.IsInPlatform(posTest))
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
                posTest.Y += 10;
            if (IsInMap(posTest) && !Platform.IsInPlatform(posTest))
            {
                Pos = posTest;
            }
            else
            {
                Fall = false;
                //lastKey = Keys.S;
            }
                
        }

        public void Draw(GameTime gameTime)
        {
            //Entity.SpriteBatch.Draw(texture2D, Pos = new Vector2(0, Height-200), color);
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
                ostPos.X += Height;
                Entity.SpriteBatch.Draw(texture2D, ostPos, color);
            }
        }


        Vector2 l_up_p;
        Vector2 r_down_p;

        public bool IsInPlatform(Vector2 ChPos)
        {          
            var ClUp = new Vector2(ChPos.X, ChPos.Y);
            var CrDow = new Vector2(ChPos.X + Character.size, ChPos.Y + Character.size);
            if ((CrDow.X > l_up_p.X && CrDow.X < r_down_p.X) || (ClUp.X > l_up_p.X && ClUp.X < r_down_p.X))
            {
                if (CrDow.Y > l_up_p.Y && CrDow.Y < r_down_p.Y)
                    return true;
                if (ClUp.Y < r_down_p.Y && ClUp.Y > l_up_p.Y)
                    return true;
                if (ClUp.Y < l_up_p.Y && CrDow.Y > r_down_p.Y)
                    return true;
            }
            return false;
        }
    }
}
