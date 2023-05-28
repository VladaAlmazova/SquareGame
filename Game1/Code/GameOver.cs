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
    internal class GameOver: Entity
    {
        public static Texture2D gameOverTexture;
        public static bool IsTheEnd;

        public void Draw()
        {
            if(IsTheEnd)
            {
                SpriteBatch.Draw(gameOverTexture, Vector2.Zero, Color.White);
                SpriteBatch.DrawString(Texts.TextMoney,
                    "Ваш cчет : " + Score.ToString(),
                    new Vector2(540, 540),
                    Color.White); // draw text
            }            
        }
    }
}
