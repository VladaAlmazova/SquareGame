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
    }


    internal class Money:Entity
    {
        Color color = Color.White;
        public static Texture2D texture2D { get; set; }
        public Vector2 Pos;
        public int size = 40;

        public Money(Platform platform)
        {
            var x1 = platform.Pos.X;
            var x2 = platform.r_down_p.X;

            Pos = new Vector2((x1 + x2) / 2 - (size / 2), platform.Pos.Y - size);
        }
        public void Update(int speed)
        {
            Pos.X -= speed;
            var l_ClUp = Character.Pos;
            var r_CrDow = new Vector2(Character.Pos.X + Character.size , Character.Pos.Y + Character.size);
            if (l_ClUp.X <= Pos.X && Pos.X < r_CrDow.X && l_ClUp.Y <= Pos.Y && Pos.Y < r_CrDow.Y)
            {
                MoneyPack.Delite(this);
                Score++;
            }
        }

        public void Draw(GameTime gameTime)
        {
            Entity.SpriteBatch.Draw(texture2D, Pos, color);
        }
    }
}
