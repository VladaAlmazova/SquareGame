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
    internal class Enemy: Entity
    {
        Color color = Color.White;
        public static Texture2D texture2D { get; set; }
        public Vector2 Pos;
        //public bool GameOver = false;
        public static double speed; // делитель 20;
        private static int startSpeed;

        public Enemy(int startSpeed = 4)
        {
            /*this*/
            Enemy.startSpeed = startSpeed;
            Pos = new Vector2(-MapWidth+50, 0);
        }

        public void Update(GameTime gameTime)
        {
            var seconds = gameTime.TotalGameTime.TotalSeconds;
            if(seconds >= 1)
            {
                Pos.X += (int)Math.Round(speed);
            }

            if (Character.Pos.X < Pos.X + MapWidth)
            {
                GameOver.IsTheEnd = true;
            }
        }

        public void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(texture2D, Pos, color);
            speed = startSpeed +  gameTime.TotalGameTime.TotalSeconds/ 10;
            if(speed >= 7)
                speed = 7;
        }
    }
}
