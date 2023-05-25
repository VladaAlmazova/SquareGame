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
    internal class MoneyPack : Entity
    {
        public List<Money> AllMoney = new List<Money>();
        
        public void Add(Platform platform)
        {
            var rand = new Random();
            if(rand.Next(0, 2) == 1 && platform.Pos.Y - Money.size < Height)
                AllMoney.Add(new Money(platform));
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

        public void CollectWhatCan()
        {
            for(int j = 0; j < AllMoney.Count; j++)
            {
                AllMoney[j].TryCollectMoney();
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

        public void TryCollectMoney()
        {
            var moneyRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, size, size);
            var characterRectangle = new Rectangle((int)Character.Pos.X, (int)Character.Pos.Y, Character.size, Character.size);
            if (characterRectangle.Contains(moneyRectangle))
            {
                MoneyPack.Delite(this);
                Score++;
            }
        }

        public void Update(int speed)
        {
            Pos.X -= speed;
            if(Pos.X + size < 0)
                MoneyPack.Delite(this);
        }

        public void Draw(GameTime gameTime)
        {
            Entity.SpriteBatch.Draw(texture2D, Pos, color);
        }
    }
}
