﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SquareGame
{
    class PackPlatforms : Entity 
    {
        static public int DistanceEmpty = 0;
        static public Random RandDist = new Random();

        public List<Platform> platforms = new List<Platform>()
        {
            new Platform(new Vector2(400+80, 700), 10),
            new Platform(new Vector2(600+80, 600), 10),
            new Platform(new Vector2(800+80, 460), 10),
        };

        public void TryAddPlatforn(int distEmpty)
        {
            PackPlatforms.DistanceEmpty += distEmpty;//для появления новой плашки 

            if (PackPlatforms.DistanceEmpty >= 20 * PackPlatforms.RandDist.NextInt64(20, 70))
            {
                Platforms.AddPlatform();
            }
        }

        public void AddPlatform()
        {
            var rand = new Random();
            var newY = rand.NextInt64(160, 880);
            newY -= newY % 20;
            var newPlatform = new Platform(new Vector2(1660, newY), (int)rand.NextInt64(6, 15));
            platforms.Add(newPlatform);

            MoneyPack.Add(newPlatform);

            DistanceEmpty = 0;
            DelitePlatform();
        }

        private void DelitePlatform() //удаляет первый элемент листа платформ
        {
            while (platforms[0].r_down_p.X < 0)
            {
                platforms.RemoveAt(0);
            }
        }

        public void GoToLeft(int speed)
        {
            for (int i = 0; i < Platforms.platforms.Count; i++)
            {
                Platforms.platforms[i].Update(speed);
            }
            MoneyPack.Update(speed);
        }

        public (bool, Platform) CorrectPositionWithAllPlat(Vector2 posCharact)
        {
            for (int i = 0; i < platforms.Count; i++)
                if (!platforms[i].CorrectPositionWithPlat(posCharact))
                    return (false, platforms[i]) ;
            return (true, null);
        }

    }

    class Platform : Entity
    {
        Color color = Color.White;
        public static Texture2D texture2D { get; set; }
        public Vector2 Pos;
        public int Width = 20;
        public int Height = 60;
        public int pictureSize = 20;
        private int countPlat = 0;
        public Platform(Vector2 pos, int count)
        {
            Pos = pos;
            countPlat = count;
            Width = Width * count;
            r_down_p = new Vector2(Pos.X + Width, Pos.Y + Height);
        }

        public void Update(int speed)
        {
            Pos.X -= speed;
            r_down_p = new Vector2(Pos.X + Width, Pos.Y + Height);
        }

        public void Draw(GameTime gameTime)
        {
            var ostPos = Pos;
            for (var i = 0; i < countPlat; i++)
            {
                Entity.SpriteBatch.Draw(texture2D, ostPos, color);
                for (int j = 1; j < 3; j++)
                {
                    Entity.SpriteBatch.Draw(texture2D, new Vector2(ostPos.X, ostPos.Y + j * pictureSize), color);
                }
                ostPos.X += pictureSize;
            }
        }


        public Vector2 r_down_p; //правого нижнего 

        public bool IsCharacterInPlatform(Vector2 ChPos)
        {
            var platformRectangle = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);
            var characterRectangle = new Rectangle((int)ChPos.X, (int)ChPos.Y, Character.size, Character.size);
            return platformRectangle.Intersects(characterRectangle);
        }


        public bool CorrectPositionWithPlat(Vector2 posCharact)
        {
            return IsInMap(posCharact) && !IsCharacterInPlatform(posCharact);
        }
    }
}
