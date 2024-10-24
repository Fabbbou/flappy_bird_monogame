using Gum.Wireframe;
using GumFormsSample;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using MonoGameGum.Forms;
using RenderingLibrary;
using static Constants;


namespace flappyrogue_mg.GameSpace
{
    public class MainGameScreen : GameScreen
    {
        //gum
        public GraphicalUiElement SoundUIScreen { get; private set; }
        public SoundUI SoundUI { get; private set; }
        public GraphicalUiElement MainGameScreenGum { get; private set; }
        public GumWindowResizer Resizer { get; private set; }
        //end gum

        public const int ROUNDING = 10;
        private SpriteBatch _spriteBatch;
        private ViewportAdapter _viewportAdapter;
        private OrthographicCamera _camera;
        public StateMachine StateMachine { get; private set; }
        public World World { get; private set; }
        public FullscreenRectangleEntity SkyBackgroundBox { get; private set; }
        //public BackgroundPicture Background { get; private set; }
        public GetReadyUI GetReadyUI { get; private set; }
        public Bird Bird { get; private set; }
        public Floor Floor { get; private set; }
        public PipesSpawner PipesSpawner { get; private set; }
        public PauseButton PauseButton { get; private set; }
        public CurrentScoreUI CurrentScoreUI { get; private set; }
        public Entity EntityJumpClickRegion { get; set; }
        public ClickableRegionHandler JumpBirdClickableRegionHandler { get; set; }
        public MainGameScreen(Game game) : base(game){}

        public override void Initialize()
        { 
            // setting the viewport dimensions to be the same as the background (bg) image
            // as the bg is portrait, the game will be portrait to
            // for a pixel perfect game, the viewport has to be the exact size of the background img
            Game.IsMouseVisible = true;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ViewportAdapterFactory factory = new VerticalViewportAdapterFactory(Game, GraphicsDevice, WORLD_WIDTH, WORLD_HEIGHT, true);
            _viewportAdapter = factory.BuildViewport();
            _camera = factory.BuildCamera();

            StateMachine = new StateMachine(new GetReadyState(this));
            GetReadyUI = new GetReadyUI();
            SkyBackgroundBox = new FullscreenRectangleEntity(GraphicsDevice, COLOR_SKY);
            //Background = new BackgroundPicture();
            Floor = new Floor();
            PipesSpawner = new PipesSpawner();
            Bird = new Bird(this);
            PauseButton = new PauseButton(this);
            CurrentScoreUI = new();

            World = new World(GraphicsDevice);
            World.AddBackgroundUIEntity(SkyBackgroundBox);
            // because the UI is drawn on top of the ingame entities, could not do this for background pic as its a pic sized for ingame
            //World.AddIngameEntity(Background); 
            World.AddIngameEntity(PipesSpawner);
            World.AddIngameEntity(Floor);
            World.AddIngameEntity(Bird);
           
            World.AddIngameEntity(PauseButton); //should be UI
            World.AddIngameEntity(GetReadyUI); //should be UI
            //CurrentScoreUI Font is scaling from transformation matrix
            //this is working because we are using the same matrix for the UI
            World.AddUIEntity(CurrentScoreUI);
            //_viewportAdapter.Reset();

            //gum
            FormsUtilities.InitializeDefaults();
            SoundUIScreen = MainRegistry.I.LoadGumScreen("SoundUI", andLoadTheScreen:false);
            SoundUI = new(this, SoundUIScreen);
            MainGameScreenGum = MainRegistry.I.LoadGumScreen("MainGameScreen", andLoadTheScreen: true);
            Resizer = new GumWindowResizer(GraphicsDevice, MainGameScreenGum);
            Resizer.Resize();
            Game.Window.ClientSizeChanged += Resizer.Resize;
            //end gum
        }

        public override void LoadContent()
        {
            World.LoadContent(Content);
            SoundUI.Load();
        }

        public override void UnloadContent()
        {
            World.UnloadContent();
            GizmosRegistry.Instance.Clear();
            SoundUIScreen.RemoveFromManagers();
            //_viewportAdapter.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();
            StateMachine.Update(gameTime);
            World.Update(gameTime);
            if (SoundUI.IsActive) FormsUtilities.Update(gameTime, SoundUIScreen);
            SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            var ingameMatrix = _camera.GetViewMatrix();
            var startScreen = MainRegistry.I.PointToScreen(0, 0);
            var endScreen = _camera.ScreenToWorld(new(_viewportAdapter.ViewportWidth, _viewportAdapter.ViewportHeight)).ToPoint();

            World.Draw(ingameMatrix);

            GizmosRegistry.Instance.Draw();

            //draw the ingame world boundaries if debugging
            if (GizmosRegistry.Instance.IsDebugging)
            {
                _spriteBatch.Begin(transformMatrix: ingameMatrix, samplerState: SamplerState.PointClamp);
                _spriteBatch.DrawRectangle(new Rectangle(0, 0, WORLD_WIDTH, WORLD_HEIGHT), Color.Blue);
                _spriteBatch.End();

                _spriteBatch.Begin( samplerState: SamplerState.PointClamp);
                _spriteBatch.DrawRectangle(new Rectangle(0, 0, _viewportAdapter.ViewportWidth, _viewportAdapter.ViewportHeight), Color.Green, thickness: 2);
                _spriteBatch.End();
            }
            SystemManagers.Default.Draw();
        }
    }
}


