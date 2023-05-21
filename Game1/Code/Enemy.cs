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
        public bool GameOver = false;
        public int speed; // делитель 20;

        public Enemy(int speed = 5)
        {
            this.speed = speed;
            Pos = new Vector2(-Width-200, 0);
        }

        public void Update()
        {
            Pos.X += speed;
            if (Character.Pos.X < Pos.X + Width)
                GameOver = true;
        }

        public void Draw(GameTime gameTime)
        {
            Entity.SpriteBatch.Draw(texture2D, Pos, color);
        }
    }
}
