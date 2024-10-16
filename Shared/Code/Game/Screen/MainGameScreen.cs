using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using Extensions;


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
            SoundUI = new SoundUI(this);
            Floor = new Floor();
            PipesSpawner = new PipesSpawner();
            Bird = new Bird(this);
            PauseButton = new PauseButton(this);
            CurrentScoreUI = new();

            World = new World(GraphicsDevice);
            World.AddEntity(PipesSpawner);
            World.AddEntity(Floor);
            World.AddEntity(Bird);
            World.AddEntity(PauseButton);
            World.AddEntity(CurrentScoreUI);
            World.AddEntity(SoundUI);
            World.AddEntity(GetReadyUI);
        }

        public override void LoadContent()
        {            
            World.LoadContent(Content);
            _background = AssetsLoader.Instance.Background;
        }

        public override void UnloadContent()
        {
            World.UnloadContent();
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

            _spriteBatch.Begin(transformMatrix: _screenHandler.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_background, Vector2.Zero, Constants.LAYER_DEPTH_INGAME);
            _spriteBatch.End();

            World.Draw(_spriteBatch, _screenHandler.GetViewMatrix());
        }
    }
}


