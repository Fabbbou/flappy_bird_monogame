using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Security.Cryptography.X509Certificates;


namespace flappyrogue_mg.GameSpace
{
    public class MainGameScreen : GameScreen
    {
        protected BoxingViewportAdapter ViewportAdapter;
        public OrthographicCamera Camera {  get; private set; }
        private SpriteBatch _spriteBatch;

        private Texture2DAtlas _atlas;
        private Texture2DRegion _dayBackground;
        private Texture2DRegion _nightBackground;

        public StateMachine StateMachine { get; private set; }
        public Bird Bird { get; private set; }
        public Floor Floor { get; private set; }
        public PipesSpawner PipesSpawner { get; private set; }
        public PauseButton PauseButton { get; private set; }
        public World World { get; private set; }
        public MainGameScreen(Game game) : base(game){}

        public override void LoadContent()
        {
            base.LoadContent();
            
            // setting the viewport dimensions to be the same as the background (bg) image
            // as the bg is portrait, the game will be portrait to
            // for a pixel perfect game, the viewport has to be the exact size of the background img
            Game.IsMouseVisible = true;
            ViewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, Constants.WORLD_WIDTH, Constants.WORLD_HEIGHT);
            Camera = new OrthographicCamera(ViewportAdapter);
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            StateMachine = new StateMachine(new PlayState(this));
            SoundManager.Instance.LoadContent(Content);

            Floor = new Floor();
            PipesSpawner = new PipesSpawner();
            Bird = new Bird(this);
            PauseButton = new PauseButton(this);
            
            World = new World();
            World.AddGameEntity(Floor);
            World.AddGameEntity(PipesSpawner);
            World.AddGameEntity(Bird);
            World.AddGameEntity(ScoreManager.Instance);
            World.AddGameEntity(PauseButton);

            PreloadedAssets.Instance.LoadContent(Content);
            // 144 and 256 are width and height of the background image.
            // As they  are uniform, the altlas automatically find each sprite contained in the texture
            // i.e. the background image is divided in 144x256 sprites
            // it is considered a uniform grid of sprites (they are all the same size)
            // more info here: https://www.monogameextended.net/docs/features/texture-handling/texture2datlas/
            Texture2D backGroundTexture = Content.Load<Texture2D>("sprites/background");
            _atlas = Texture2DAtlas.Create("Atlas/Background", backGroundTexture, Constants.WORLD_WIDTH, Constants.WORLD_HEIGHT);
            _dayBackground = _atlas[0];
            _nightBackground = _atlas[1];

            World.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();
            StateMachine.Update(gameTime);

            World.Update(gameTime);

            PhysicsEngine.Instance.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(transformMatrix: Camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);
            
            //background
            _spriteBatch.Draw(_dayBackground, Vector2.Zero, Color.White);

            World.Draw(_spriteBatch);

            PhysicsGizmosRegistry.Instance.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}


