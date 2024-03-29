﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SquareGame
{
    internal class Enemy: Entity
    {
        Color color = Color.White;
        public static Texture2D texture2D { get; set; }
        public Vector2 Pos;
        public static double speed;
        private static int startSpeed;

        public Enemy(int startSpeed = 4)
        {
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
