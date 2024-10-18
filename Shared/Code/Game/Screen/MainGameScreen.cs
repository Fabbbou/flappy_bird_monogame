using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using Extensions;
using static Constants;


namespace flappyrogue_mg.GameSpace
{
    public class MainGameScreen : GameScreen
    {
        public const int ROUNDING = 10;
        private SpriteBatch _spriteBatch;
        private Texture2DRegion _background;
        public StateMachine StateMachine { get; private set; }
        public GetReadyUI GetReadyUI { get; private set; }
        public Bird Bird { get; private set; }
        public Floor Floor { get; private set; }
        public PipesSpawner PipesSpawner { get; private set; }
        public PauseButton PauseButton { get; private set; }
        public CurrentScoreUI CurrentScoreUI { get; private set; }
        public World World { get; private set; }
        public SoundUI SoundUI { get; private set; }
        public FullscreenRectangleEntity GrayBackground { get; private set; }
        public Entity EntityJumpClickRegion { get; set; }
        public ClickableRegionHandler JumpBirdClickableRegionHandler { get; set; }
        public MainGameScreen(Game game) : base(game){}

        public override void Initialize()
        {
            // setting the viewport dimensions to be the same as the background (bg) image
            // as the bg is portrait, the game will be portrait to
            // for a pixel perfect game, the viewport has to be the exact size of the background img
            Game.IsMouseVisible = true;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            StateMachine = new StateMachine(new GetReadyState(this));
            GetReadyUI = new GetReadyUI();
            Floor = new Floor();
            PipesSpawner = new PipesSpawner();
            Bird = new Bird(this);
            PauseButton = new PauseButton(this);
            CurrentScoreUI = new();
            SoundUI = new SoundUI(this);
            GrayBackground = new FullscreenRectangleEntity(GraphicsDevice, COLOR_GRAY_UI, MainRegistry.I.Camera)
            {
                IsActive = false
            };

            World = new World(GraphicsDevice);
            World.AddIngameEntity(PipesSpawner);
            World.AddIngameEntity(Floor);
            World.AddIngameEntity(Bird);

            World.AddIngameEntity(PauseButton); //should be UI
            World.AddIngameEntity(GetReadyUI); //should be UI
            //CurrentScoreUI Font is scaling from transformation matrix
            //this is working because we are using the same matrix for the UI
            World.AddUIEntity(CurrentScoreUI);
            MainRegistry.I.ViewportAdapter.Reset();
        }

        public override void LoadContent()
        {            
            World.LoadContent(Content);
            _background = AssetsLoader.Instance.Background;
            SoundUI.LoadContent(Content);
        }

        public override void UnloadContent()
        {
            World.UnloadContent();
            GizmosRegistry.Instance.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();
            StateMachine.Update(gameTime);
            World.UpdateV2(gameTime);
            World.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            var ingameMatrix = MainRegistry.I.GetScaleMatrix();
            var startScreen = MainRegistry.I.ScreenToWorld(0, 0).ToPoint();
            var endScreen = MainRegistry.I.ViewportAdapter.PointToScreen(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            GraphicsDevice.Clear(Color.Transparent);
            
            _spriteBatch.Begin(transformMatrix: ingameMatrix, samplerState: SamplerState.PointClamp);
            //background : the sky above the picture (out of world bounds, for responsiveness)
            _spriteBatch.FillRectangle(new Rectangle(startScreen.X, startScreen.Y, endScreen.X, endScreen.Y), COLOR_SKY);
            //background : background pic
            _spriteBatch.Draw(_background, Vector2.Zero, Constants.LAYER_DEPTH_INGAME);
            _spriteBatch.End();

            World.BatchDraw(_spriteBatch, ingameMatrix);
            World.Draw(ingameMatrix);

            _spriteBatch.Begin(transformMatrix: ingameMatrix, samplerState: SamplerState.PointClamp);
            //Floor: brown part 
            _spriteBatch.FillRectangle(new Rectangle(0, (int)SPRITE_POSITION_FLOOR.Y + FLOOR_HEIGHT_GREEN_BANNER, endScreen.X, endScreen.Y), COLOR_FLOOR);
            //_spriteBatch.End();

            if (GrayBackground.IsActive)
            {
                //_spriteBatch.Begin(transformMatrix: ingameMatrix, samplerState: SamplerState.PointClamp);
                //GrayBackground.Draw(_spriteBatch);
                _spriteBatch.FillRectangle(new Rectangle(startScreen.X, startScreen.Y, endScreen.X, endScreen.Y), COLOR_GRAY_UI);

                //_spriteBatch.End();
            }

            if (SoundUI.IsActive)
            {
                //_spriteBatch.Begin(transformMatrix: ingameMatrix, samplerState: SamplerState.PointClamp);
                SoundUI.Draw(_spriteBatch);
                //_spriteBatch.End();
            }
            _spriteBatch.End();

            GizmosRegistry.Instance.Draw();

            //draw the ingame world boundaries if debugging
            if (GizmosRegistry.Instance.IsDebugging)
            {
                _spriteBatch.Begin(transformMatrix: ingameMatrix, samplerState: SamplerState.PointClamp);
                _spriteBatch.DrawRectangle(new Rectangle(0, 0, WORLD_WIDTH, WORLD_HEIGHT), Color.Blue);
                _spriteBatch.End();

                _spriteBatch.Begin( samplerState: SamplerState.PointClamp);
                _spriteBatch.DrawRectangle(new Rectangle(0, 0, MainRegistry.I.ViewportAdapter.ViewportWidth, MainRegistry.I.ViewportAdapter.ViewportHeight), Color.Green, thickness: 2);
                _spriteBatch.End();
            }
        }
    }
}


