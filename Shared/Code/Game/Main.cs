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
        private GraphicsDeviceManager _graphics;
        private readonly FramesPerSecondCounter _fpsCounter = new FramesPerSecondCounter();
        ScreenRegistry _screenRegistry;
        public Main()
        {            
            _screenRegistry = new ScreenRegistry(this);
            _graphics = new GraphicsDeviceManager(this);
            _graphics.SynchronizeWithVerticalRetrace = true;
#if WINDOWS || DESKTOP
            //int height = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.75f);
            //_graphics.PreferredBackBufferHeight = height;
            //_graphics.PreferredBackBufferWidth = height * 9 / 16;
            _graphics.PreferredBackBufferWidth = 388; //a phone sized screen to test the responsiveness
            _graphics.PreferredBackBufferHeight = 736;

#elif ANDROID || IOS
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.ApplyChanges();
#endif
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;

            ViewportAdapterFactory factory = new VerticalViewportAdapterFactory(this, _graphics, WORLD_WIDTH, WORLD_HEIGHT);
            MainRegistry.I.Init(this, _graphics, _screenRegistry, factory);
        }


        protected override void Initialize()
        {
            //uncomment to see the Gizmos to debug
            GizmosRegistry.Instance.Start(_graphics.GraphicsDevice, true);

            //  Initialize screens
            _screenRegistry.AddScreen(ScreenName.MenuScreen, new MenuScreen(this));
            _screenRegistry.AddScreen(ScreenName.MainGameScreen, new MainGameScreen(this));
            _screenRegistry.AddScreen(ScreenName.GameOverScreen, new GameOverScreen(this));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            AssetsLoader.Instance.LoadContent(Content);
            _screenRegistry.LoadScreen(ScreenName.MainGameScreen);
        }

        protected override void Update(GameTime gameTime)
        {
            
            _fpsCounter.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _fpsCounter.Draw(gameTime);
            Window.Title = $"{_screenRegistry.CurrentScreen} {_fpsCounter.FramesPerSecond}";
            base.Draw(gameTime);
        }
    }
}


