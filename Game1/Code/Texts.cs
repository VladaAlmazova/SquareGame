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
    internal static class Texts
    {
        public static SpriteFont TextMoney;
        public static SpriteFont TextTime;

        public static void Draw(GameTime gameTime)
        {
            Entity.SpriteBatch.DrawString(TextMoney, 
                "cчет : " + Entity.Score.ToString(), 
                new Vector2(1400, 20), 
                Color.Black); // draw text
            Entity.SpriteBatch.DrawString(TextTime, 
                gameTime.TotalGameTime.Minutes.ToString() + " : " + gameTime.TotalGameTime.Seconds.ToString(), 
                new Vector2(20, 880), 
                Color.Black); // draw text
            Entity.SpriteBatch.DrawString(TextTime,
                (Math.Round(Entity.Character.energy*100)).ToString() + " % " ,
                new Vector2(1400, 60),
                Color.Black); // draw text
            Entity.SpriteBatch.DrawString(TextTime,
                (Math.Round(Enemy.speed)).ToString() + "  ",
                new Vector2(1400, 100),
                Color.Black); // draw text
        }
    }
}
