using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;


namespace flappyrogue_mg.GameSpace
{
    public class DebugPhysics : GameScreen
    {
        protected BoxingViewportAdapter ViewportAdapter;
        private SpriteBatch _spriteBatch;

        private PhysicsObject partFloor1;
        private PhysicsObject partFloor2;
        private PhysicsObject partFloor3;
        private PhysicsObject partFloor4;
        private PhysicsObject partFloor5;
        private PhysicsObject partFloor6;
        private PhysicsObject partFloor7;
        private PhysicsObject partFloor8;
        private PhysicsObject partFloor9;
        private PhysicsObject partFloor10;
        private PhysicsObject partFloor11;
        private PhysicsObject partFloor12;
        private PhysicsObject partFloor13;
        private PhysicsObject partFloor14;
        private PhysicsObject partFloor15;
        private PhysicsObject partFloor16;
        private PhysicsObject partFloor17;
        private PhysicsObject partFloor18;
        private PhysicsObject partFloor19;
        
        private PhysicsObject movingBox;

        public DebugPhysics(Game game) : base(game) { }

        public override void LoadContent()
        {
            Game.IsMouseVisible = true;
            ViewportAdapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, Constants.DEBUG_WORLD_WIDTH, Constants.DEBUG_WORLD_HEIGHT);
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            partFloor1 = new("partFloor1", 0, 800, 100, 100, CollisionType.Static, Vector2.Zero);
            partFloor2 = new("partFloor2", 100, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor3 = new("partFloor3", 110, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor4 = new("partFloor4", 120, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor5 = new("partFloor5", 130, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor18 = new("partFloor18", 140, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor6 = new("partFloor6", 150, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor7 = new("partFloor7", 160, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor8 = new("partFloor8", 170, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor9 = new("partFloor9", 180, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor10 = new("partFloor10", 190, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor11 = new("partFloor11", 200, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor12 = new("partFloor12", 210, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor13 = new("partFloor13", 220, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor14 = new("partFloor14", 230, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor15 = new("partFloor15", 240, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor16 = new("partFloor16", 250, 800, 10, 100, CollisionType.Static, Vector2.Zero);
            partFloor17 = new("partFloor17", 260, 800, 100, 100, CollisionType.Static, Vector2.Zero);
            partFloor19 = new("partFloor17", 270, 800, 1000 - 270, 100, CollisionType.Static, Vector2.Zero);

            movingBox = new("movingBox", 0, 0, 100, 100, CollisionType.Moving, new Vector2(100, 100));
            movingBox.Gravity = new Vector2(0, 450f);

        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();

            //arrow to move the box using addForce
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                movingBox.ApplyForce(new Vector2(0, -1000), ForceType.Continuous);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                movingBox.ApplyForce(new Vector2(0, 1000), ForceType.Continuous);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                movingBox.ApplyForce(new Vector2(-1000, 0), ForceType.Continuous);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                movingBox.ApplyForce(new Vector2(1000, 0), ForceType.Continuous);
            }
            PhysicsEngine.Instance.MoveAndSlide(movingBox, gameTime);

            PhysicsEngine.Instance.Update(gameTime);
        }

        protected virtual Matrix GetTransformMatrix()
        {
            return ViewportAdapter.GetScaleMatrix();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
            _spriteBatch.Begin(transformMatrix: GetTransformMatrix(), samplerState: SamplerState.PointClamp);

            //draw stuff here

            PhysicsDebug.Instance.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}


