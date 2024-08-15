using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace flappyrogue_mg
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private Vector2 _spritePosition;
        private float _spriteSpeed = 100f; // pixels per second
        private Texture2D _spriteTexture;
        Dictionary<string, Rectangle> atlasData;

        private Bird _bird;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _bird = new Bird(new Vector2(100, 100));
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load a texture from the Content pipeline

            string json = File.ReadAllText("Content/sprites/bird.json");
            atlasData = JsonConvert.DeserializeObject<Dictionary<string, Rectangle>>(json);

            //set _spriteTexture to the bird texture from the Content pipeline
            _spriteTexture = Content.Load<Texture2D>("sprites/bird");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update sprite position based on elapsed time
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _bird.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_spriteTexture, _bird.Position, atlasData["bird-1"], Color.White);

            //drw debug of bird fields
            _spriteBatch.DrawString(Content.Load<SpriteFont>("fonts/04B_19"), $"Position: {_bird.Position}", new Vector2(10, 10), Color.White);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("fonts/04B_19"), $"Velocity: {_bird.Velocity}", new Vector2(10, 30), Color.White);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("fonts/04B_19"), $"Acceleration: {_bird.Acceleration}", new Vector2(10, 50), Color.White);

            // TODO: Add your drawing code here

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


