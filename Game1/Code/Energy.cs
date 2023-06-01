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
    internal class Energy : Entity
    {
        public static Texture2D texture2D { get; set; }
        public static double energy;
        public static int size = 9;
        public void Draw(GameTime gameTime)
        {
            for(int i = 0; i < (int)Math.Round(100*energy); i++)
                SpriteBatch.Draw(texture2D, new Vector2(MapWidth-size, (MapHeight - size) - i*size), Color.BlueViolet);
            /*if (IsTheEnd)
            {
                SpriteBatch.Draw(gameOverTexture, Vector2.Zero, Color.White);
                SpriteBatch.DrawString(Texts.TextMoney,
                    "Ваш cчет : " + Score.ToString(),
                    new Vector2(540, 540),
                    Color.White); // draw text
            }*/
        }
    }
}