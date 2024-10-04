using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.ViewportAdapters;
using System;
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
        private GraphicsDeviceManager _graphics;
        private readonly FramesPerSecondCounter _fpsCounter = new FramesPerSecondCounter();
        private readonly Dictionary<ScreenName, GameScreen> _screens = new Dictionary<ScreenName, GameScreen>();

        private readonly ScreenManager _screenManager;
        private ScreenName _currentScreen;

        public ViewportAdapter ViewportAdapter { get; private set; }

        private Main()
        {
            //uncomment to see the physics debug
            //PhysicsGizmosRegistry.Instance.DrawGizmos(true);
            
            _graphics = new GraphicsDeviceManager(this);
#if WINDOWS || DESKTOP
            int height = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.75f);
            _graphics.PreferredBackBufferHeight = height;
            _graphics.PreferredBackBufferWidth = height * 9 / 16;
            _graphics.ApplyChanges();
#elif ANDROID || IOS
            _graphics.PreferredBackBufferWidth = Constants.WORLD_WIDTH;
            _graphics.PreferredBackBufferHeight = Constants.WORLD_HEIGHT;
            _graphics.ApplyChanges();
#endif
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;

            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }


        protected override void Initialize()
        {
            //GizmosRegistry.Instance.DrawGizmos(true);

            //  Initialize screens
            _screens.Add(ScreenName.MenuScreen, new MenuScreen(this));
            _screens.Add(ScreenName.MainGameScreen, new MainGameScreen(this));
            _screens.Add(ScreenName.GameOverScreen, new GameOverScreen(this));

            //debug purpose only
            _screens.Add(ScreenName.ZoomedOutMainScreen, new ZoomedOutMainScreen(this));
            _screens.Add(ScreenName.DebugPhysicsScreen, new DebugPhysics(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadScreen(ScreenName.MenuScreen);
            //LoadScreen(ScreenName.ZoomedOutMain);
            //LoadScreen(ScreenName.DebugPhysics);
        }

        public void LoadScreen(ScreenName screen)
        {
            _screenManager.LoadScreen(_screens[screen]);
            _currentScreen = screen;
        }

        public void LoadScreen(ScreenName screen, Transition transition)
        {
            _screenManager.LoadScreen(_screens[screen], transition);
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


