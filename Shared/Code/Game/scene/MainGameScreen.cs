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
        public GraphicalUiElement ClickZone { get; private set; }
        public PipesSpawner PipesSpawner { get; private set; }
        //end gum

        public static readonly Rectangle JumpRegion = new Rectangle(CLICK_REGION_POSITION_JUMP_REGION.ToPoint(), CLICK_REGION_SIZE_JUMP_REGION.ToPoint());
        public StateMachine StateMachine { get; private set; }
        public World World { get; private set; }
        public Bird Bird { get; private set; }
        public Floor Floor { get; private set; }
        
        public MainGameScreen(Game game) : base(game){}

        public override void Initialize()
        { 
            Game.IsMouseVisible = true;

            //gum
            FormsUtilities.InitializeDefaults();
            InitializeGumComponents();
            var getReadyElement = MainGameScreenGum.GetGraphicalUiElementByName("GetReadyFullscreenContainer");
            
            StateMachine = new StateMachine(new GetReadyState(this, getReadyElement));

            //end gum

            // setting the viewport dimensions to be the same as the background (bg) image
            // as the bg is portrait, the game will be portrait to
            // for a pixel perfect game, the viewport has to be the exact size of the background img
            Bird = new Bird(this, RootIngameWorld);

            World = new World(GraphicsDevice);
            World.AddEntity(PipesSpawner);
            World.AddEntity(Bird);

            //has to be setup at the end because of PauseButtonMobile and PauseButtonWidescreen initialization
            Floor = new Floor(MainGameScreenGum);
            World.AddEntity(Floor);
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

            PipesSpawner = new PipesSpawner(RootIngameWorld);
            ClickZone = GumTransparentButton.FindAndAttachButton("ClickZone", RootIngameWorld, actionPushed: OnClickZone);
        }

        public void OnWindowResize()
        {
            if (MainRegistry.I.CurrentFrameScale.IsWideScreen)
            {
                PauseButtonMobile.Visible = false;
                PauseButtonWidescreen.Visible = true;
                CurrentPauseButton = PauseButtonWidescreen;
            }
            else
            {
                PauseButtonMobile.Visible = true;
                PauseButtonWidescreen.Visible = false;
                CurrentPauseButton = PauseButtonMobile;
            }
            if (StateMachine != null && StateMachine.CurrentState is GetReadyState)
            {
                CurrentPauseButton.Visible = false;
            }
        }

        public void OnClickZone()
        {
            if (StateMachine.CurrentState is GetReadyState)
            {
                StateMachine.ChangeState(new PlayState(this));
            }
            else if (StateMachine.CurrentState is PlayState)
            {
                Bird.Jump();
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
            World.UnloadContent();
            GizmosRegistry.Instance.Clear();
            SoundUIScreen.RemoveFromManagers();
            BackgroundGumWindowResizer.Dispose();
            Game.Window.ClientSizeChanged -= SoundUIResizer.Resize;
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
            GizmosRegistry.Instance.Refresh();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SystemManagers.Default.Draw();
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


