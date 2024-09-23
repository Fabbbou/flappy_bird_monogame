using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.ViewportAdapters;


namespace flappyrogue_mg.Game
{
    public class GameMainDebugBorders : Microsoft.Xna.Framework.Game
    {

        private GraphicsDeviceManager _graphics;
        private BoxingViewportAdapter _viewportAdapter;
        private OrthographicCamera _camera;
        private SpriteBatch _spriteBatch;

        private Texture2DAtlas _atlas;
        private Texture2DRegion _dayBackground;
        private Texture2DRegion _nightBackground;

        private readonly Bird _bird;
        private readonly Floor _floor;
        private readonly Pipes _pipes;
        private readonly PipesSpawner _pipesSpawner;

        public GameMainDebugBorders()
        {
            //uncomment to see the physics debug
            //PhysicsDebug.Instance.SetDebug(true);

            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";
            _graphics = new GraphicsDeviceManager(this);
            //resize the startup window to match the game ratio
            _graphics.PreferredBackBufferHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.75f);
            _graphics.PreferredBackBufferWidth = _graphics.PreferredBackBufferHeight * 9 / 16;
            _graphics.ApplyChanges();

            
            _bird = new Bird();
            _floor = new Floor();
            //_pipes = new Pipes(60f, 100f, 60f, 60f); //test pipes
            _pipesSpawner = new PipesSpawner();
        }

        protected override void Initialize()
        {
            base.Initialize();
            // setting the viewport dimensions to be the same as the background (bg) image
            // as the bg is portrait, the game will be portrait to
            // for a pixel perfect game, the viewport has to be the exact size of the background img
            _viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, GameMain.WORLD_WIDTH, GameMain.WORLD_HEIGHT);
            _camera = new OrthographicCamera(_viewportAdapter);
            _camera.ZoomOut(0.5f);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameAtlasTextures.Instance.Load(Content, GraphicsDevice);
            // 144 and 256 are width and height of the background image.
            // As they  are uniform, the altlas automatically find each sprite contained in the texture
            // i.e. the background image is divided in 144x256 sprites
            // it is considered a uniform grid of sprites (they are all the same size)
            // more info here: https://www.monogameextended.net/docs/features/texture-handling/texture2datlas/
            Texture2D backGroundTexture = Content.Load<Texture2D>("sprites/background");
            _atlas = Texture2DAtlas.Create("Atlas/Background", backGroundTexture, GameMain.WORLD_WIDTH, GameMain.WORLD_HEIGHT);
            _dayBackground = _atlas[0];
            _nightBackground = _atlas[1];


            _floor.LoadSingleInstance(Content, _graphics.GraphicsDevice);
            //_pipes.Load(Content, _graphics.GraphicsDevice
            _pipesSpawner.Load(Content, _graphics.GraphicsDevice);
            _bird.LoadSingleInstance(Content, _graphics.GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // Update sprite position based on elapsed time
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //_pipes.Update(gameTime, _graphics.GraphicsDevice);
            _pipesSpawner.UpdateRandomHeight(gameTime);
            _floor.Update(gameTime);
            _bird.Update(gameTime);
            
            PhysicsEngine.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);

            // Draw the background
            _spriteBatch.Draw(_dayBackground, Vector2.Zero, Color.White);

            // Draw the pipes (has to be behind the floor)
            //_pipes.Draw(_spriteBatch);
            _pipesSpawner.Draw(_spriteBatch);


            // Draw the floor
            _floor.Draw(_spriteBatch);

            // Draw the bird
            _bird.Draw(_spriteBatch);

            PhysicsDebug.Instance.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


