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
        public SoundUI SoundUI { get; private set; }
        public GraphicalUiElement MainGameScreenGum { get; private set; }
        public BackgroundGumHandler BackgroundHandler { get; private set; }
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
            //end gum


            World = new World(GraphicsDevice);
            World.AddEntity(PipesSpawner);
            World.AddEntity(Bird);
            World.AddEntity(Floor);
        }

        private void InitializeGumComponents()
        {
            MainGameScreenGum = MainRegistry.I.ChangeScreen("MainGameScreen");
            PauseButtonMobile = GumTransparentButton.AttachButton("PauseButtonMobile", MainGameScreenGum, OnClickPause);
            PauseButtonWidescreen = GumTransparentButton.AttachButton("PauseButtonWidescreen", MainGameScreenGum, OnClickPause);
            FloorExtension = MainGameScreenGum.GetGraphicalUiElementByName("FloorExtension");
            BackgroundHandler = new BackgroundGumHandler(Game.Window, MainGameScreenGum, OnWindowResize);
            BackgroundHandler.InitAndResizeOnce();

            RootIngameWorld = MainGameScreenGum.GetGraphicalUiElementByName("WorldContainerCliper");

            SoundUI = new(this, MainGameScreenGum);

            Floor = new Floor(RootIngameWorld);

            ScoreText = MainGameScreenGum.GetGraphicalUiElementByName("ScoreText");
            ScoreManager.Instance.AttachScoreText(MainGameScreenGum);
            ScoreManager.Instance.ResetScore();

            PipesSpawner = new PipesSpawner(MainGameScreenGum, RootIngameWorld);
            ClickZone = GumTransparentButton.AttachButton("ClickZone", RootIngameWorld, actionPushed: OnClickZone);
            Bird = new Bird(this, RootIngameWorld);

            var getReadyElement = MainGameScreenGum.GetGraphicalUiElementByName("GetReadyFullscreenContainer");
            StateMachine = new StateMachine(new GetReadyState(this, getReadyElement));
        }

        public void OnWindowResize()
        {
            var currentVisible = CurrentPauseButton?.Visible ?? false;
            if (MainRegistry.I.CurrentFrameScale.IsWideScreen)
            {
                PauseButtonMobile.Visible = false;
                CurrentPauseButton = PauseButtonWidescreen;
            }
            else
            {
                PauseButtonWidescreen.Visible = false;
                CurrentPauseButton = PauseButtonMobile;
            }
            CurrentPauseButton.Visible = currentVisible;
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
            Debug.WriteLine("MainGameScreen LoadContent");
        }

        public override void UnloadContent()
        {
            World.UnloadContent();
            PipesSpawner.Reset();
            GizmosRegistry.Instance.Clear();
            BackgroundHandler.Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            StateMachine.Update(gameTime);
            World.Update(gameTime);
            GizmosRegistry.Instance.Refresh();
            BackgroundHandler.AnimateParallax(gameTime);
        }

        public void OnClickPause()
        {
            Debug.WriteLine("Pause button clicked");
            if (StateMachine.CurrentState is PlayState)
            {
                StateMachine.ChangeState(new PauseState(this));
            }
        }
        public override void Draw(GameTime gameTime) {}
    }
}


