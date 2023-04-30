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
        Texture2D texture;

        //private Vector2 pos_char = new Vector2(50, 900 - 113 - 50);
        //private const int size = 113;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1680;
            graphics.PreferredBackBufferHeight = 900;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Entity.InIt(spriteBatch, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Character.texture2D = Content.Load<Texture2D>("main_character"); //113*113
            Platform.texture2D = Content.Load<Texture2D>("platform");//20*20
           // texture = Content.Load<Texture2D>("platform");//20*20
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
            Entity.Platform1.Update(keys);

            //pos_char = Entity.Character.Pos;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.WhiteSmoke);
            spriteBatch.Begin();
            /*for(var i = 0; i < 10; i++)
                spriteBatch.Draw(texture, new Vector2(578+(20*i), 550), Color.White);*/
            Entity.Draw(gameTime);

            spriteBatch.End();

            /*spriteBatch.Begin();
            spriteBatch.Draw(texture, pos_char, Color.White);
            spriteBatch.End();*/
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}