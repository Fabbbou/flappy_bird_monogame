using GumFormsSample;
using GumRuntime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGameGum.Forms;
using RenderingLibrary;
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
            //GizmosRegistry.Instance.Start(_graphics.GraphicsDevice, true);

            var sceneRegistry = new SceneRegistry(this);
            sceneRegistry.AddScene(SceneName.MenuScreen, new MenuScreen(this));
            sceneRegistry.AddScene(SceneName.MainGameScreen, new MainGameScreen(this));
            sceneRegistry.AddScene(SceneName.GameOverScreen, new GameOverScreen(this));
            //ViewportAdapterFactory factory = new VerticalViewportAdapterFactory(this, GraphicsDevice, WORLD_WIDTH, WORLD_HEIGHT, false);
            MainRegistry.I.Initialize(this, GraphicsDevice, sceneRegistry, WORLD_WIDTH, WORLD_HEIGHT, gumProjectPath: "proj.gumx");
            MainRegistry.I.SceneRegistry.LoadScene(SceneName.MenuScreen);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _fpsCounter.Update(gameTime);
            base.Update(gameTime);
            MainRegistry.I.SceneRegistry?.CurrentScreen?.AnimateSelf(gameTime.ElapsedGameTime.TotalSeconds);
            if (IsActive) FormsUtilities.Update(gameTime, MainRegistry.I.SceneRegistry.CurrentScreen);
            SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
            MousePrinter.PrintOnLeftPressed(GraphicsDevice);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _fpsCounter.Draw(gameTime);
            Window.Title = $"FPS: {_fpsCounter.FramesPerSecond}";
            base.Draw(gameTime);
            SystemManagers.Default.Draw();
        }

        protected override void Dispose(bool disposing)
        {
            MainRegistry.I.Dispose();
            base.Dispose(disposing);
        }
    }


}


