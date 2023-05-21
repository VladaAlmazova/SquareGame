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
    class Character : Entity
    {
        Color color = Color.White;
        public static Texture2D texture2D { get; set; }
        public Vector2 Pos;
        public const int size = 120;
        public int speed = 10; // делитель 20;
        public int gravity = 10;// делитель 20;
        public Character(Vector2 pos)
        {
            Pos = pos;
        }

        public Keys lastKey = Keys.S;
        private bool Fall = false;      

        private void CanFall(Vector2 pos) //3plat
        {
            pos.Y += 5;
            if (Platforms.CorrectPositionWithAllPlat(pos))
                Fall = true;
            pos.Y -= 5;

            if (lastKey == Keys.Up)
                Fall = true;
        }

        private bool wasRight = false; //нажата ли клавиша вправо
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

        private bool SomethingBad = false;

        private Vector2 CorrectPosUpdate(Vector2 posTest)
        {
            //SomethingBad = false;
            var correctPos = Pos;
            if (Platforms.CorrectPositionWithAllPlat(posTest))
            {
                if (wasRight && !Fall && Pos.X >= FocusPos.X) // Если кубик падает, то не двигать карту // все же двигать 
                {
                    for (int i = 0; i < Platforms.platforms.Count; i++)
                    {
                        Platforms.platforms[i].Update(speed);
                    }

                    MoneyPack.Update(speed);
                    Enemy.Pos.X -= speed;

                    DistanceEmpty += 20;//для появления новой плашки 

                    if (DistanceEmpty >= 20 * RandDist.NextInt64(20, 50))
                    {
                        Platforms.AddPlatform();
                    }
                }
                /*if (wasRight && Pos.X >= FocusPos.X) // просто в два раза быстрее движется после фокуса,
                                                     // падение по направлению уменьшилось, но одновременно движутся и платформы и персонаж
                                                     // ! возможно сделать анимацию без нажатия кнопки
                {

                    var kof = 1;
                    if (Fall)
                        kof = 2;
                    //при падении двигать и плашки и кубик

                    if (Fall)
                    {
                        var p = posTest;
                        p.Y += gravity;
                        p.X = Pos.X;
                        if (CorrectPositionWithAllPlat(p))
                            correctPos.Y += gravity;
                        else
                            Fall = false;
                    }

                    for (int i = 0; i < Platforms.platforms.Count; i++)
                    {
                        Platforms.platforms[i].Update(speed / kof);
                    }
                    CanFall(correctPos);



                    DistanceEmpty += 20;//для появления новой плашки 

                    if (DistanceEmpty >= 20 * RandDist.NextInt64(20, 50))
                    {
                        Platforms.AddPlatform();
                    }
                }*/
                else
                    correctPos = posTest;
            }
            else
                SomethingBad = true;
            return correctPos;
        }

        public void Update(Keys[] keys)
        {
            var posTest = Pos;
            if (!Fall)
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
            //n
            var p1 = CorrectPosUpdate(posTest);
            if(SomethingBad)
            {
                if (lastKey == Keys.Right)
                {
                    p1 = CorrectPosUpdate(new Vector2(posTest.X - 5, posTest.Y));
                }
                if (lastKey == Keys.Left)
                {
                    p1 = CorrectPosUpdate(new Vector2(posTest.X + 5, posTest.Y));
                }
            }        
            Pos = p1;
            //            
            if (SomethingBad)
            {
                Fall = true;
                posTest = Pos;
                if (lastKey == Keys.Up)
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
        }

        public void Draw(GameTime gameTime)
        {
            Entity.SpriteBatch.Draw(texture2D, Pos, color);
        }
    }
}
