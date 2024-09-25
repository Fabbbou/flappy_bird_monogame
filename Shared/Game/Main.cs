using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using System.Collections.Generic;


namespace flappyrogue_mg.GameSpace
{
    public class Main : Game
    {

        private readonly GraphicsDeviceManager _graphics;
        private readonly FramesPerSecondCounter _fpsCounter = new FramesPerSecondCounter();
        private readonly Dictionary<ScreenName, GameScreen> _screens = new Dictionary<ScreenName, GameScreen>();

        private readonly ScreenManager _screenManager;
        private ScreenName _currentScreen;
        public Main()
        {
            //uncomment to see the physics debug
            PhysicsDebug.Instance.DrawGizmos(true);

            _graphics = new GraphicsDeviceManager(this);

            //resize the startup window to match the game ratio
            //_graphics.PreferredBackBufferHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.75f);
            //_graphics.PreferredBackBufferWidth = _graphics.PreferredBackBufferHeight * 9 / 16;
            _graphics.PreferredBackBufferHeight = Constants.DEBUG_SCREEN_WIDTH;
            _graphics.PreferredBackBufferWidth = Constants.DEBUG_SCREEN_HEIGHT;

            _graphics.ApplyChanges();

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
            _screens.Add(ScreenName.MainGame, new MainGame(this));
            _screens.Add(ScreenName.ZoomedOutMain, new ZoomedOutMain(this));
            _screens.Add(ScreenName.DebugPhysics, new DebugPhysics(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //LoadScreen(ScreenName.MainGame);
            //LoadScreen(ScreenName.ZoomedOutMain);
            LoadScreen(ScreenName.DebugPhysics);
        }

        public void LoadScreen(ScreenName screen)
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


