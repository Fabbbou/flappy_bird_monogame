using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumFormsSample;
using GumRuntime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.ViewportAdapters;
using MonoGameGum.Forms;
using MonoGameGum.Forms.Controls;
using MonoGameGum.Forms.Controls.Primitives;
using MonoGameGum.GueDeriving;
using RenderingLibrary;
using System;
using System.Collections.Generic;
using static Constants;


namespace flappyrogue_mg.GameSpace
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        FramesPerSecondCounter _fpsCounter = new FramesPerSecondCounter();

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.SynchronizeWithVerticalRetrace = true;

            //change the window size to a phone size
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Gum init
            SystemManagers.Default = new SystemManagers();
            SystemManagers.Default.Initialize(_graphics.GraphicsDevice, fullInstantiation: true);
            FormsUtilities.InitializeDefaults();
            ElementSaveExtensions.RegisterGueInstantiationType("Buttons/GumTransparentButton", typeof(GumTransparentButton));

            //uncomment to see the Gizmos to debug
            GizmosRegistry.Instance.Start(_graphics.GraphicsDevice, true);

            var screenRegistry = new ScreenRegistry(this);
            screenRegistry.AddScreen(ScreenName.MenuScreen, new MenuScreen(this));
            screenRegistry.AddScreen(ScreenName.MainGameScreen, new MainGameScreen(this));
            screenRegistry.AddScreen(ScreenName.GameOverScreen, new GameOverScreen(this));
            ViewportAdapterFactory factory = new VerticalViewportAdapterFactory(this, GraphicsDevice, WORLD_WIDTH, WORLD_HEIGHT, false);
            MainRegistry.I.Initialize(this, GraphicsDevice, screenRegistry, factory, WORLD_WIDTH, WORLD_HEIGHT, gumProjectPath: "proj.gumx");
            MainRegistry.I.ScreenRegistry.LoadScreen(ScreenName.MenuScreen);

            SetStartupWindowSize();
            base.Initialize();
        }

        private void SetStartupWindowSize()
        {
#if WINDOWS || DESKTOP
            int height = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.75f);
            _graphics.PreferredBackBufferHeight = height;
            _graphics.PreferredBackBufferWidth = height * 9 / 16;
#elif ANDROID || IOS
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
#endif
            _graphics.ApplyChanges();
            MainRegistry.I.RefreshScale();
        }

        protected override void LoadContent()
        {
            AssetsLoader.Instance.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            _fpsCounter.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _fpsCounter.Draw(gameTime);
            Window.Title = $"FPS: {_fpsCounter.FramesPerSecond}";
            base.Draw(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            MainRegistry.I.Dispose();
            base.Dispose(disposing);
        }
    }


}


