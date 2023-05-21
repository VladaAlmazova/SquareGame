using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Game1
{
    public class Game1 : Game 
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;


        Texture2D gameOverTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1680;
            graphics.PreferredBackBufferHeight = 920;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Entity.InIt(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Character.texture2D = Content.Load<Texture2D>("character");//"main_character"); //120*120
            Platform.texture2D = Content.Load<Texture2D>("platform");//20*20
            Enemy.texture2D = Content.Load<Texture2D>("Enemy1");
            Money.texture2D = Content.Load<Texture2D>("money");
            gameOverTexture = Content.Load<Texture2D>("gameOver");
        }

        protected override void Update(GameTime gameTime)
        {
            var keys = Keyboard.GetState().GetPressedKeys();
            if(keys.Length > 0)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
            }
            Entity.Character.Update(keys);
            Entity.Enemy.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);
            spriteBatch.Begin();
            Entity.Draw(gameTime);
            if(Entity.Enemy.GameOver) // конец игры
                spriteBatch.Draw(gameOverTexture, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}