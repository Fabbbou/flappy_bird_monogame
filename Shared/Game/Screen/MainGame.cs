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
            Game.IsMouseVisible = true;
            _bird = new Bird(this);
            _floor = new Floor();
            //_pipes = new Pipes(60f, 100f, 60f, 60f); //test pipes
            _pipesSpawner = new PipesSpawner();

            ViewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, Constants.WORLD_WIDTH, Constants.WORLD_HEIGHT);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameAtlasTextures.Instance.Load(Content, GraphicsDevice);
            // 144 and 256 are width and height of the background image.
            // As they  are uniform, the altlas automatically find each sprite contained in the texture
            // i.e. the background image is divided in 144x256 sprites
            // it is considered a uniform grid of sprites (they are all the same size)
            // more info here: https://www.monogameextended.net/docs/features/texture-handling/texture2datlas/
            Texture2D backGroundTexture = Content.Load<Texture2D>("sprites/background");
            _atlas = Texture2DAtlas.Create("Atlas/Background", backGroundTexture, Constants.WORLD_WIDTH, Constants.WORLD_HEIGHT);
            _dayBackground = _atlas[0];
            _nightBackground = _atlas[1];


            _floor.LoadSingleInstance(Content);
            //_pipes.Load(Content, _graphics.GraphicsDevice
            _pipesSpawner.Load(Content);
            _bird.LoadSingleInstance(Content);
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();
            // Update sprite position based on elapsed time
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //_pipes.Update(gameTime, _graphics.GraphicsDevice);
            _pipesSpawner.UpdateRandomHeight(gameTime);
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
            //_pipes.Draw(_spriteBatch);
            _pipesSpawner.Draw(_spriteBatch);


            // Draw the floor
            _floor.Draw(_spriteBatch);

            // Draw the bird
            _bird.Draw(_spriteBatch);

            PhysicsDebug.Instance.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}


