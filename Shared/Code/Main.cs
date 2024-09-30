using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using System.Collections.Generic;


namespace flappyrogue_mg.GameSpace
{
    public class Main : Game
    {
        private static Main _instance;
        public static Main Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Main();
                }
                return _instance;
            }
        }
        public GraphicsDeviceManager Graphics { get; private set; }
        private readonly FramesPerSecondCounter _fpsCounter = new FramesPerSecondCounter();
        private readonly Dictionary<ScreenNames, GameScreen> _screens = new Dictionary<ScreenNames, GameScreen>();

        private readonly ScreenManager _screenManager;
        private ScreenNames _currentScreen;

        public ViewportAdapter ViewportAdapter { get; private set; }

        private Main()
        {
            //uncomment to see the physics debug
            PhysicsGizmosRegistry.Instance.DrawGizmos(true);
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;

            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }


        protected override void Initialize()
        {
            
            // setting the viewport dimensions to be the same as the background (bg) image
            // as the bg is portrait, the game will be portrait to
            // for a pixel perfect game, the viewport has to be the exact size of the background img
            //ViewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, Constants.WORLD_WIDTH, Constants.WORLD_HEIGHT);

            //  Initialize screens
            _screens.Add(ScreenNames.MainGameScreen, new MainGameScreen(this));
            _screens.Add(ScreenNames.ZoomedOutMainScreen, new ZoomedOutMainScreen(this));
            _screens.Add(ScreenNames.DebugPhysicsScreen, new DebugPhysics(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            ViewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            LoadScreen(ScreenNames.MainGameScreen);
            //LoadScreen(ScreenName.ZoomedOutMain);
            //LoadScreen(ScreenName.DebugPhysics);
        }

        public void LoadScreen(ScreenNames screen)
        {
            _screenManager.LoadScreen(_screens[screen]);
            _currentScreen = screen;
        }

        protected override void Update(GameTime gameTime)
        {
            _fpsCounter.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _fpsCounter.Draw(gameTime);
            Window.Title = $"{_currentScreen} {_fpsCounter.FramesPerSecond}";
            base.Draw(gameTime);
        }
    }
}


