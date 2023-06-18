using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SquareGame
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
                Color.AliceBlue);
            Entity.SpriteBatch.DrawString(TextTime,
                gameTime.TotalGameTime.Minutes.ToString() + " : " + gameTime.TotalGameTime.Seconds.ToString(),
                new Vector2(20, 880),
                Color.Coral);
            Entity.SpriteBatch.DrawString(TextTime,
                (Math.Round(Enemy.speed)).ToString() + "  ",
                new Vector2(0, 0),
                Color.AliceBlue); 
        }
    }
}
