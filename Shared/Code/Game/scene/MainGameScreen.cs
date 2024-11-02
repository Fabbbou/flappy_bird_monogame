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
using System.Diagnostics;
using static Constants;


namespace flappyrogue_mg.GameSpace
{
    public class MainGameScreen : GameScreen
    {
        //gum
        public GraphicalUiElement SoundUIScreen { get; private set; }
        public SoundUI SoundUI { get; private set; }
        public GraphicalUiElement MainGameScreenGum { get; private set; }
        public GumWindowResizer SoundUIResizer { get; private set; }
        public BackgroundGumWindowResizer BackgroundGumWindowResizer { get; private set; }
        public GraphicalUiElement FloorExtension { get; private set; }
        public GraphicalUiElement PauseButtonMobile { get; private set; }
        public GraphicalUiElement PauseButtonWidescreen { get; private set; }
        public GraphicalUiElement CurrentPauseButton { get; private set; }
        public GraphicalUiElement RootIngameWorld { get; private set; }
        public GraphicalUiElement ScoreText { get; private set; }

        public GraphicalUiElement APipeContainer { get; private set; }
        public Pipes APipe { get; private set; }
        //end gum

        public static readonly Rectangle JumpRegion = new Rectangle(CLICK_REGION_POSITION_JUMP_REGION.ToPoint(), CLICK_REGION_SIZE_JUMP_REGION.ToPoint());
        public const int ROUNDING = 10;
        private SpriteBatch _spriteBatch;
        //private ViewportAdapter _viewportAdapter;
        //private OrthographicCamera _camera;
        public StateMachine StateMachine { get; private set; }
        public World World { get; private set; }
        public GetReadyUI GetReadyUI { get; private set; }
        public Bird Bird { get; private set; }
        public Floor Floor { get; private set; }
        public PipesSpawner PipesSpawner { get; private set; }
        public CurrentScoreUI CurrentScoreUI { get; private set; }
        public Entity EntityJumpClickRegion { get; set; }
        public ClickableRegionHandler JumpBirdClickableRegionHandler { get; set; }
        public MainGameScreen(Game game) : base(game){}

        public override void Initialize()
        { 
            StateMachine = new StateMachine(new GetReadyState(this));
            //gum
            FormsUtilities.InitializeDefaults();
            InitializeGumComponents();
            //end gum

            // setting the viewport dimensions to be the same as the background (bg) image
            // as the bg is portrait, the game will be portrait to
            // for a pixel perfect game, the viewport has to be the exact size of the background img
            Game.IsMouseVisible = true;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //ViewportAdapterFactory factory = new VerticalViewportAdapterFactory(Game, GraphicsDevice, WORLD_WIDTH, WORLD_HEIGHT, true);
            //_viewportAdapter = factory.BuildViewport();
            //_camera = factory.BuildCamera();

            GetReadyUI = new GetReadyUI();
            PipesSpawner = new PipesSpawner();
            Bird = new Bird(this);
            CurrentScoreUI = new();

            World = new World(GraphicsDevice);
            // because the UI is drawn on top of the ingame entities, could not do this for background pic as its a pic sized for ingame
            //World.AddIngameEntity(Background); 
            //World.AddIngameEntity(PipesSpawner);
            World.AddIngameEntity(APipe);
            World.AddIngameEntity(Bird);
           
            //World.AddIngameEntity(PauseButton); //should be UI
            World.AddIngameEntity(GetReadyUI); //should be UI
            //CurrentScoreUI Font is scaling from transformation matrix
            //this is working because we are using the same matrix for the UI
            World.AddUIEntity(CurrentScoreUI);
            //_viewportAdapter.Reset();

            //has to be setup at the end because of PauseButtonMobile and PauseButtonWidescreen initialization
            Floor = new Floor(MainGameScreenGum);
            World.AddIngameEntity(Floor);
        }

        private void InitializeGumComponents()
        {
            MainGameScreenGum = MainRegistry.I.ChangeScreen("MainGameScreen");
            PauseButtonMobile = GumTransparentButton.FindAndAttachButton("PauseButtonMobile", MainGameScreenGum, OnClickPause);
            PauseButtonWidescreen = GumTransparentButton.FindAndAttachButton("PauseButtonWidescreen", MainGameScreenGum, OnClickPause);
            FloorExtension = MainGameScreenGum.GetGraphicalUiElementByName("FloorExtension");
            BackgroundGumWindowResizer = new BackgroundGumWindowResizer(Game.Window, GraphicsDevice, MainGameScreenGum, OnWindowResize);
            BackgroundGumWindowResizer.InitAndResizeOnce();

            RootIngameWorld = MainGameScreenGum.GetGraphicalUiElementByName("BackgroundPic");

            SoundUIScreen = MainRegistry.I.GetScreenButDontShow("SoundUI");
            SoundUI = new(this, SoundUIScreen);
            SoundUIResizer = new GumWindowResizer(GraphicsDevice, SoundUIScreen);
            SoundUIResizer.Resize();
            Game.Window.ClientSizeChanged += SoundUIResizer.Resize;

            
            ScoreManager.Instance.AttachScoreText(MainGameScreenGum);
            ScoreManager.Instance.ResetScore();

            APipeContainer = MainGameScreenGum.GetGraphicalUiElementByName("APipeContainer");
            APipe = new(RootIngameWorld, APipeContainer, 60);
        }

        public void OnWindowResize()
        {
            if (MainRegistry.I.CurrentFrameScale.IsWideScreen)
            {
                //FloorExtension.Visible = false;
                PauseButtonMobile.Visible = false;
                PauseButtonWidescreen.Visible = true;
                CurrentPauseButton = PauseButtonWidescreen;
            }
            else
            {
                //FloorExtension.Visible = true;
                PauseButtonMobile.Visible = true;
                PauseButtonWidescreen.Visible = false;
                CurrentPauseButton = PauseButtonMobile;
            }
            if (StateMachine.CurrentState is GetReadyState)
            {
                CurrentPauseButton.Visible = false;
            }
        }

        public override void LoadContent()
        {
            World.LoadContent(Content);
            SoundUI.Load();
            Debug.WriteLine("MainGameScreen LoadContent");
        }

        public override void UnloadContent()
        {
            //Bird.UnloadContent();
            World.UnloadContent();
            GizmosRegistry.Instance.Clear();
            SoundUIScreen.RemoveFromManagers();
            //MainGameScreenGum.RemoveFromManagers();
            BackgroundGumWindowResizer.Dispose();
            Game.Window.ClientSizeChanged -= SoundUIResizer.Resize;
            //_viewportAdapter.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();
            MainGameScreenGum.AnimateSelf(gameTime.ElapsedGameTime.TotalSeconds);
            StateMachine.Update(gameTime);
            World.Update(gameTime);
            if (SoundUI.IsActive)
            {
                FormsUtilities.Update(gameTime, SoundUIScreen);
            }
            else
            {
                FormsUtilities.Update(gameTime, MainGameScreenGum);
            }
            SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SystemManagers.Default.Draw();
            GizmosRegistry.Instance.Draw();
        }

        public void OnClickPause()
        {
            Debug.WriteLine("Pause button clicked");
            if (StateMachine.CurrentState is PlayState)
            {
                StateMachine.ChangeState(new PauseState(this));
            }
        }
    }
}


