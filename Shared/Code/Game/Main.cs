using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using static Constants;


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

        public ScreenHandler ScreenHandler { get; private set; }

        private Main()
        {            
            _graphics = new GraphicsDeviceManager(this);
            _graphics.SynchronizeWithVerticalRetrace = true;
#if WINDOWS || DESKTOP
            int height = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.75f);
            _graphics.PreferredBackBufferHeight = height;
            _graphics.PreferredBackBufferWidth = height * 9 / 16;
            //_graphics.PreferredBackBufferWidth = 388; //a phone sized screen to test the responsiveness
            //_graphics.PreferredBackBufferHeight = 736;

#elif ANDROID || IOS
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.ApplyChanges();
#endif
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;

            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }


        protected override void Initialize()
        {
            ScreenHandler = new(_graphics.GraphicsDevice, Window, WORLD_WIDTH, WORLD_HEIGHT);
            //uncomment to see the Gizmos to debug
            //GizmosRegistry.Instance.Start(_graphics.GraphicsDevice, ScreenHandler);

            //  Initialize screens
            _screens.Add(ScreenName.MenuScreen, new MenuScreen(this, ScreenHandler));
            _screens.Add(ScreenName.MainGameScreen, new MainGameScreen(this, ScreenHandler));
            _screens.Add(ScreenName.GameOverScreen, new GameOverScreen(this, ScreenHandler));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            AssetsLoader.Instance.LoadContent(Content);
            LoadScreen(ScreenName.MenuScreen);
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


