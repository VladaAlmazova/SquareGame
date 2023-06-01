using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    internal class MoneyPack : Entity
    {
        public List<Money> AllMoney = new List<Money>();
        
        public void Add(Platform platform)
        {
            var rand = new Random();
            if(rand.Next(0, 2) == 1 && platform.Pos.Y - Money.size > 0)
                AllMoney.Add(new Money(platform));
            DeliteMoney();
        }

        public void Delite(Money money)
        {
            AllMoney.Remove(money);
        }

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < AllMoney.Count; i++)
            {
                AllMoney[i].Draw(gameTime);
            }
        }

        public void Update(int speed)
        {
            for(int i = 0; i < AllMoney.Count; i++)
            {
                AllMoney[i].Update(speed);
            }
        }

        private void DeliteMoney() //удаляет первый элемент листа платформ
        {
            while (AllMoney.Count > 0 && AllMoney[0].Pos.X + Money.size < 0)
            {
                AllMoney.RemoveAt(0);
            }
        }

        public void CollectWhatCan()
        {
            for(int j = 0; j < AllMoney.Count; j++)
            {
                if(AllMoney[j].TryCollectMoney())
                {
                    MoneyPack.Delite(AllMoney[j]);
                    Score++;
                }
            }
        }
    }


    internal class Money:Entity
    {
        Color color = Color.White;
        public static Texture2D texture2D { get; set; }
        public Vector2 Pos;
        static public int size = 40;

        public Money(Platform platform)
        {
            var x1 = platform.Pos.X;
            var x2 = platform.r_down_p.X;

            Pos = new Vector2((x1 + x2) / 2 - (size / 2), platform.Pos.Y - size);
        }

        public bool TryCollectMoney()
        {
            var moneyRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, size, size);
            var characterRectangle = new Rectangle((int)Character.Pos.X, (int)Character.Pos.Y, Character.size, Character.size);
            return characterRectangle.Intersects(moneyRectangle);
        }

        public void Update(int speed)
        {
            Pos.X -= speed;
        }

        public void Draw(GameTime gameTime)
        {
            Entity.SpriteBatch.Draw(texture2D, Pos, color);
        }
    }
}
