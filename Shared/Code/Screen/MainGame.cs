using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;


namespace flappyrogue_mg.GameSpace
{
    public class MainGame : GameScreen
    {
        protected BoxingViewportAdapter ViewportAdapter;

        private SpriteBatch _spriteBatch;

        private Texture2DAtlas _atlas;
        private Texture2DRegion _dayBackground;
        private Texture2DRegion _nightBackground;

        private Bird _bird;
        private Floor _floor;
        private Pipes _pipes;
        private PipesSpawner _pipesSpawner;
        public MainGame(Game game) : base(game){}

        public override void LoadContent()
        {
            //resize the startup window to match the game ratio
            Main.Instance.Graphics.PreferredBackBufferHeight = (int)(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.75f);
            Main.Instance.Graphics.PreferredBackBufferWidth = Main.Instance.Graphics.PreferredBackBufferHeight * 9 / 16;
            Main.Instance.Graphics.ApplyChanges();
            _floor = new Floor();
            _pipesSpawner = new PipesSpawner();
            _bird = new Bird(this);

            // setting the viewport dimensions to be the same as the background (bg) image
            // as the bg is portrait, the game will be portrait to
            // for a pixel perfect game, the viewport has to be the exact size of the background img
            ViewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, Constants.WORLD_WIDTH, Constants.WORLD_HEIGHT);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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


            ScoreManager.Instance.LoadContent(Content);
            _floor.LoadContent(Content);
            _pipesSpawner.LoadContent(Content);
            _bird.LoadContent(Content);
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();

            _pipesSpawner.Update(gameTime);
            _floor.Update(gameTime);
            _bird.Update(gameTime);
            
            PhysicsEngine.Instance.Update(gameTime);
        }

        protected virtual Matrix GetTransformMatrix()
        {
            return ViewportAdapter.GetScaleMatrix();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(transformMatrix: GetTransformMatrix(), samplerState: SamplerState.PointClamp);

            // Draw the background
            _spriteBatch.Draw(_dayBackground, Vector2.Zero, Color.White);

            // Draw the pipes (has to be behind the floor)
            _pipesSpawner.Draw(_spriteBatch);

            // Draw the floor
            _floor.Draw(_spriteBatch);

            ScoreManager.Instance.Draw(_spriteBatch);

            // Draw the bird
            _bird.Draw(_spriteBatch);

            PhysicsDebug.Instance.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}


