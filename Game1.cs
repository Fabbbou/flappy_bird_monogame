using AsepriteDotNet.Aseprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.ViewportAdapters;


namespace flappyrogue_mg
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private BoxingViewportAdapter _viewportAdapter;
        private SpriteBatch _spriteBatch;

        private Texture2DAtlas _atlas;
        private Texture2DRegion _dayBackground;
        private Texture2DRegion _nightBackground;

        private Bird _bird;

        public Game1()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
            _graphics = new GraphicsDeviceManager(this);
            //set up a fullscreen portrait mode for the game default window
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 9/16;
            _graphics.ApplyChanges();

            
            _bird = new Bird(new Vector2(100, 100));
        }

        protected override void Initialize()
        {
            base.Initialize();
            // setting the viewport dimensions to be the same as the background (bg) image
            // as the bg is portrait, the game will be portrait to
            // for a pixel perfect game, the viewport has to be the exact size of the background img
            _viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 144, 256); 
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // 144 and 256 are width and height of the background image.
            // As they  are uniform, the altlas automatically find each sprite contained in the texture
            // i.e. the background image is divided in 144x256 sprites
            // it is considered a uniform grid of sprites (they are all the same size)
            // more info here: https://www.monogameextended.net/docs/features/texture-handling/texture2datlas/
            Texture2D backGroundTexture = Content.Load<Texture2D>("sprites/background");
            _atlas = Texture2DAtlas.Create("Atlas/Background",backGroundTexture, 144, 256);
            _dayBackground = _atlas[0];
            _nightBackground = _atlas[1];

            _bird.Load(Content, _graphics);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // Update sprite position based on elapsed time
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _bird.Update(gameTime, _graphics);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(transformMatrix: _viewportAdapter.GetScaleMatrix(), samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_dayBackground, Vector2.Zero, Color.White);
            _bird.Draw(gameTime, _spriteBatch, Content, _viewportAdapter, _graphics);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


