using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using Extensions;
using static Constants;


namespace flappyrogue_mg.GameSpace
{
    public class MainGameScreen : GameScreen
    {
        private SpriteBatch _spriteBatch;
        private Texture2DRegion _background;
        private ScreenHandler _screenHandler;
        private ScalingViewportAdapter _viewportAdapter;
        private BoxingViewportAdapter BoxingViewportAdapter;
        private OrthographicCamera camera;
        public StateMachine StateMachine { get; private set; }
        public GetReadyUI GetReadyUI { get; private set; }
        public Bird Bird { get; private set; }
        public Floor Floor { get; private set; }
        public PipesSpawner PipesSpawner { get; private set; }
        public PauseButton PauseButton { get; private set; }
        public CurrentScoreUI CurrentScoreUI { get; private set; }
        public World World { get; private set; }
        public SoundUI SoundUI { get; private set; }
        public FullscreenRectangleEntity GrayBackground { get; private set; }
        public Entity EntityJumpClickRegion { get; set; }
        public ClickableRegionHandler JumpBirdClickableRegionHandler { get; set; }
        public MainGameScreen(Game game, ScreenHandler screenHandler) : base(game)
        {
            _screenHandler = screenHandler;
        }

        public override void Initialize()
        {
            // setting the viewport dimensions to be the same as the background (bg) image
            // as the bg is portrait, the game will be portrait to
            // for a pixel perfect game, the viewport has to be the exact size of the background img
            Game.IsMouseVisible = true;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            StateMachine = new StateMachine(new GetReadyState(this));
            GetReadyUI = new GetReadyUI();
            Floor = new Floor();
            PipesSpawner = new PipesSpawner();
            Bird = new Bird(this);
            PauseButton = new PauseButton(this);
            CurrentScoreUI = new();
            SoundUI = new SoundUI(this);
            GrayBackground = new FullscreenRectangleEntity(GraphicsDevice, COLOR_GRAY_UI)
            {
                IsActive = false
            };

            World = new World(GraphicsDevice);
            World.AddEntity(PipesSpawner);
            World.AddEntity(Floor);
            World.AddEntity(Bird);
            World.AddEntity(PauseButton);
            World.AddEntity(CurrentScoreUI);
            World.AddEntity(GetReadyUI);
        }

        public override void LoadContent()
        {            
            World.LoadContent(Content);
            _background = AssetsLoader.Instance.Background;
            SoundUI.LoadContent(Content);
        }

        public override void UnloadContent()
        {
            World.UnloadContent();
            GizmosRegistry.Instance.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();
            StateMachine.Update(gameTime);
            World.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);
            
            _spriteBatch.Begin();
            //the upper sky
            _spriteBatch.FillRectangle(new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height/2), COLOR_SKY);
            _spriteBatch.End();

            _spriteBatch.Begin(transformMatrix: _screenHandler.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_background, Vector2.Zero, Constants.LAYER_DEPTH_INGAME);
            _spriteBatch.End();

            World.BatchDraw(_spriteBatch, _screenHandler.GetViewMatrix());


            _spriteBatch.Begin(transformMatrix: _screenHandler.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            //padding floor to avoid seeing the end of the floor (and pipes too)
            _spriteBatch.FillRectangle(new Rectangle(0, (int)SPRITE_POSITION_FLOOR.Y+ FLOOR_HEIGHT_GREEN_BANNER, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), COLOR_FLOOR);
            if (GizmosRegistry.Instance.IsDebugging)
            {
                //draw the ingame world boundaries if debugging
                _spriteBatch.DrawRectangle(new Rectangle(0, 0, WORLD_WIDTH, WORLD_HEIGHT), Color.Blue);
            }
            _spriteBatch.End();
            
            if (GrayBackground.IsActive)
            {
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                GrayBackground.Draw(_spriteBatch);
                _spriteBatch.End();
            }

            if(SoundUI.IsActive)
            {
                _spriteBatch.Begin(transformMatrix: _screenHandler.GetViewMatrix(), samplerState: SamplerState.PointClamp);
                SoundUI.Draw(_spriteBatch);
                _spriteBatch.End();
            }


            //DEBUG
            _spriteBatch.Begin(transformMatrix: _screenHandler.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            if (GizmosRegistry.Instance.IsDebugging)
            {
            //draw the ingame world boundaries if debugging
                _spriteBatch.DrawRectangle(new Rectangle(0, 0, WORLD_WIDTH, WORLD_HEIGHT), Color.Blue);
            }
            _spriteBatch.End();
            GizmosRegistry.Instance.Draw();
        }
    }
}


