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
   class Entity
    {
        private const int CharSize = 120;
        const int indent = 0;//10; кратное 5

        public static int MapWidth, MapHeight;
        public static SpriteBatch SpriteBatch { get; set; }
        static public Character Character { get; set; }
        static public Enemy Enemy { get; set; }

        public static readonly Vector2 FocusPos = new Vector2(840, 800);
        public static int Score = 0;

        

        public static PackPlatforms Platforms = new PackPlatforms();
        public static MoneyPack MoneyPack = new MoneyPack();

        public static GameOver GameOver = new GameOver();

        public static void InIt(SpriteBatch spriteBatch, int width, int height)
        {
            SpriteBatch = spriteBatch;
            MapWidth = width;
            MapHeight = height;
            Character = new Character(new Vector2(200, 800));
            Enemy = new Enemy();

        }

        static public void Draw(GameTime gameTime)
        {
            Character.Draw(gameTime);
            for(int i = 0; i < Platforms.platforms.Count; i++)
                Platforms.platforms[i].Draw(gameTime);
            MoneyPack.Draw(gameTime);
            Enemy.Draw(gameTime);
            Texts.Draw(gameTime);
            GameOver.Draw();
        }

        public static bool IsInMap(Vector2 pos)
        {
            return !(pos.X < indent || pos.X + CharSize > MapWidth - indent || pos.Y < indent || pos.Y + CharSize > MapHeight - indent);    
        }
    }   
}
