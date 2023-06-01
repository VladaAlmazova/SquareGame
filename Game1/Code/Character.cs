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
        //public int nowSpeed = 13;//15;//10; // делитель 20
        public int gravity = 10;// делитель 20;

        public int nowSpeed = 10;
        private int maxspeed = 20;
        public double energy = 1;
        public Character(Vector2 pos)
        {
            Pos = pos;
        }

        public Keys lastKey = Keys.S;
        private bool Fall = false;

        private void CanFall(Vector2 pos) //3plat
        {
            pos.Y += 5;
            if (Platforms.CorrectPositionWithAllPlat(pos).Item1)
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
                    Ups = 0;
                    break;
                case Keys.Right:
                    posTest.X += speed;
                    CanFall(posTest);
                    wasRight = true;
                    Ups = 0;
                    break;
                case Keys.Up:
                    posTest.Y -= speed;
                    Ups++;
                    break;
                default:
                    key = lastKey;
                    break;
            }
            lastKey = key;
            return posTest;
        }

        private bool SomethingBad = false;
        private int Ups = 0;

        private static int Ws = 0;

        private Vector2 CorrectPosUpdate(Vector2 posTest, int dist)
        {
            var correctPos = Pos;
            if (Pos == posTest)
                return Pos;
            var (isCorrect, crashPlat) = Platforms.CorrectPositionWithAllPlat(posTest);

            if(!isCorrect)
            {
                SomethingBad = true;
                posTest = Alignment(posTest, crashPlat);//
                if (Pos == posTest)
                    return Pos;
                dist = (int)Math.Abs(Pos.X - posTest.X);//
                var (iscor2, crplat) = Platforms.CorrectPositionWithAllPlat(posTest);
                isCorrect = iscor2;
            }
            dist = (int)Math.Abs(Pos.X - posTest.X);//

            if (isCorrect)
            {
                if (wasRight && posTest.X >= FocusPos.X)
                {
                    Platforms.GoToLeft(dist);
                    if(Enemy.Pos.X + MapWidth + 300>= 0)
                        Enemy.Pos.X -= dist;
                    Platforms.TryAddPlatforn(dist);
                    correctPos.Y = posTest.Y;
                }
                else
                    correctPos = posTest;
            }
            else
            {
                SomethingBad = true;
            }
               
            return correctPos;
        }

        private Vector2 Alignment(Vector2 incorrectPos, Platform? crashPlat)
        {
            if(crashPlat == null)
                return incorrectPos;
            var correctPos = incorrectPos;
            if(!IsInMap(incorrectPos))
            {
                if (incorrectPos.X < 0)
                    correctPos.X = 0;
                if (incorrectPos.X > MapWidth - Character.size)
                    correctPos.X = MapWidth - Character.size;
                if(incorrectPos.Y < 0)
                    correctPos.Y = 0;
                if (incorrectPos.Y > MapHeight - Character.size)
                    correctPos.Y = MapHeight - Character.size;
                if (crashPlat.CorrectPositionWithPlat(correctPos))
                    return correctPos;
            }
            
            var test2 = correctPos;
            var rectan = new Rectangle((int)crashPlat.Pos.X, (int)crashPlat.Pos.Y, crashPlat.Width, crashPlat.Height);
            switch (lastKey)
            {
                case Keys.Left:
                    if(Math.Abs(test2.X - rectan.Right) <= Math.Abs(Pos.X - incorrectPos.X))
                        test2.X = crashPlat.Pos.X + crashPlat.Width;
                    break;
                case Keys.Right:
                    if (Math.Abs(rectan.Left - (test2.X + size)) <= Math.Abs(Pos.X - incorrectPos.X))//speed)
                        test2.X = crashPlat.Pos.X - Character.size;
                    break;
                case Keys.Up:
                    test2.Y = crashPlat.Pos.Y + crashPlat.Height;
                    break;
                default:
                    break;
            }
            correctPos = test2;

            if (Math.Abs(correctPos.Y + Character.size - crashPlat.Pos.Y) < gravity)//speed/2)
            {
                correctPos.Y = crashPlat.Pos.Y - Character.size;
            //Fall = false;
            }

            if (!crashPlat.CorrectPositionWithPlat(correctPos))
                SomethingBad = true;

            return correctPos;
        }

        public void Update(Keys[] keys)
        {
            var startPos = Pos;
            var posTest = Pos;
            if (!Fall)
            {
                if (keys.Length > 0)
                {
                    posTest = PressingButton(posTest, keys[0], nowSpeed);//nowSpeed);//
                }
            }
            var speed_e = nowSpeed;//nowSpeed;//
            if (Fall)
                speed_e = nowSpeed / 2;//nowSpeed / 2;//
            if (Pos == posTest)//если ничего не нажато проолжаем в том же духе
            {
                posTest = PressingButton(posTest, lastKey, speed_e);
            }
            Pos = CorrectPosUpdate(posTest, speed_e);//p1;
            //            
            if (SomethingBad)
            {
                Fall = true; //n
                posTest = Pos;
                if (lastKey == Keys.Up)
                {
                    lastKey = Keys.S;
                    Ups = 0;
                }

            }
            SomethingBad = false;
            if (Fall)
            {
                posTest.Y += gravity;
                Ups = 0;

            }


            Pos = CorrectPosUpdate(posTest, speed_e);
            if (SomethingBad)
            {
                Fall = false;
                Ups = 0;
            }
            SomethingBad = false;

            MoneyPack.CollectWhatCan();

            /*if (Ups == 0)
            {
                nowSpeed = (int)Math.Round(maxspeed * energy);
                energy += 0.004;
                if (energy >= 1)
                    energy = 1;
            }            
            else
            {
               
                energy -= 0.011;
                if(energy <= 0 )
                {
                    energy = 0;
                    lastKey = Keys.S;
                    Fall = true;
                }
                    
                nowSpeed = (int)Math.Round(maxspeed * energy);
            }*/
            if (Ups == 0)
            {
                //nowSpeed = (int)Math.Round(maxspeed * energy);
                Energy.energy += 0.004;
                if (Energy.energy >= 1)
                    Energy.energy = 1;
            }
            else
            {

                Energy.energy -= 0.011;
                if (Energy.energy <= 0)
                {
                    Energy.energy = 0;
                    lastKey = Keys.S;
                    Fall = true;
                }

                nowSpeed = (int)Math.Round(maxspeed * Energy.energy);
            }
            nowSpeed = (int)Math.Round(maxspeed * Energy.energy);
        }

        public void Draw(GameTime gameTime)
        {
            Entity.SpriteBatch.Draw(texture2D, Pos, color);
        }
    }

    
}