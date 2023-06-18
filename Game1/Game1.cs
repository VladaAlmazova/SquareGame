using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SquareGame
{
    public class Game1 : Game 
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public static Texture2D background { get; set; }



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
            Character.texture2D = Content.Load<Texture2D>("custom/character_n");
            Platform.texture2D = Content.Load<Texture2D>("custom/platform_n");
            Enemy.texture2D = Content.Load<Texture2D>("Enemy1");
            Money.texture2D = Content.Load<Texture2D>("custom/money_n");
            GameOver.gameOverTexture = Content.Load<Texture2D>("gameOver");
            Texts.TextMoney = Content.Load<SpriteFont>("TextMoney");
            Texts.TextTime = Content.Load<SpriteFont>("TextTime");
            Energy.texture2D = Content.Load<Texture2D>("custom/energy9");
            background = Content.Load<Texture2D>("custom/background");
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
            Entity.Enemy.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.WhiteSmoke);
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            Entity.Draw(gameTime);
            
            spriteBatch.End();           
            base.Draw(gameTime);
        }
    }
}