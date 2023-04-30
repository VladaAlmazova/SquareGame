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
        public const int size = 122;
        public Character(Vector2 pos, Platform platform)
        {
            Pos = pos;
            Platform = platform;
            cordPointsPlatf(platform);
            cordPointsChar(pos);
        }

        private bool VectorsIntersect(Tuple<Vector2, Vector2> vert, Tuple<Vector2, Vector2>gor)
        {
            return gor.Item1.X < vert.Item1.X && vert.Item1.X < gor.Item2.X &&
                vert.Item1.Y < gor.Item1.Y && gor.Item1.Y < vert.Item2.Y;
        }

        Vector2[] platCord = new Vector2[4];
        Vector2[] charCord = new Vector2[4];
        private void cordPointsPlatf(Platform platform)
        {
            var p = platform.Pos;
            platCord = new Vector2[]
            {
                /*new Vector2(p.X, p.Y-platform.Height),
                p,
                new Vector2(p.X+ platform.Width, p.Y),
                new Vector2(p.X + platform.Width, p.Y - platform.Height),*/
                p,
                new Vector2(p.X, p.Y+platform.Height),
                new Vector2(p.X + platform.Width, p.Y + platform.Height),
                new Vector2(p.X+ platform.Width, p.Y),               
            };
        }

        private void cordPointsChar(Vector2 pos)
        {
            var p = pos;
            charCord = new Vector2[]
            {
                p,
                new Vector2(p.X, p.Y+size),
                new Vector2(p.X + size, p.Y + size),
                new Vector2(p.X + size, p.Y),
                
            };

        }

        public void Update(Keys[] keys)
        {
            var posTest = Pos;
            if (keys.Length > 0)
            {
                switch (keys[0])
                {
                    case Keys.Left:
                        posTest.X -= 20;
                        

                        break;
                    case Keys.Right:
                        posTest.X += 20;
                        break;
                    case Keys.Up:
                        posTest.Y -= 20;
                        break;
                    /*case Keys.Down:
                        posTest.Y += 20;
                        break;*/
                    default:
                        break;
                }
            }
            //posTest.Y += 10;
            /*else
            {
                posTest.Y += 10;
            }*/
            if (IsInMap(posTest) && !Platform.IsInPlatform(posTest))
            {
                Pos = posTest;
            }
                
            posTest.Y += 10;
            if (IsInMap(posTest) && !Platform.IsInPlatform(posTest))
            {
                Pos = posTest;
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

        public bool IsInPlatform(Platform plat2)
        {
            return false;
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
            }
            return false;
        }
    }
}
