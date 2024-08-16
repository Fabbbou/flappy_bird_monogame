using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;


namespace flappyrogue_mg
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private BoxingViewportAdapter _viewportAdapter;
        private SpriteBatch _spriteBatch;
//        private float _aspectRatio = 9f / 16f; // Desired aspect ratio (e.g., 16:9)


        private AtlasTexture _background;
        private Bird _bird;

        public Game1()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 9/16;
            _graphics.ApplyChanges();

            
            _background = new AtlasTexture("sprites/bg", "sprites/bg.json");
            _bird = new Bird(new Vector2(100, 100));
        }

        protected override void Initialize()
        {
            base.Initialize();
            _viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 144, 256); // viewport as to be the exact size of the background img to be pixel perfect
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _background.Load(Content);

            _bird.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // Update sprite position based on elapsed time
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _bird.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: _viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);
            _background.Draw(_spriteBatch, Vector2.Zero);
            _bird.Draw(gameTime, _spriteBatch, Content, _viewportAdapter);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


