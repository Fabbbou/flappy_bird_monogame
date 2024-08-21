using flappyrogue_mg.Game.Core;
using flappyrogue_mg.Game.Core.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;


namespace flappyrogue_mg
{
    public class DebugDynamicRectVsRectCollision : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private BoxingViewportAdapter _viewportAdapter;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        private static readonly Vector2 GravityZero = new Vector2(0, 0);
        private PhysicsObject _rectangleMoving;
        private PhysicsObject _rectangleStatic;
        private Collision _collision;
        private Collision _lastCollision;

        public DebugDynamicRectVsRectCollision()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";

            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 1400;
            _graphics.ApplyChanges();

            _rectangleMoving = PhysicsObject.Create(new Vector2(0, 150), 100, 100);
            _rectangleMoving.Gravity = GravityZero;
            _rectangleStatic = PhysicsObject.Create(new Vector2(200, 200), 250, 100);
            _rectangleStatic.Gravity = GravityZero;
            _collision = null;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("fonts/arial");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var velocity = 500f;
            //up down left right keys to move the rectangle
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _rectangleMoving.Velocity = new Vector2(0, -velocity);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _rectangleMoving.Velocity = new Vector2(0, velocity);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _rectangleMoving.Velocity = new Vector2(-velocity, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _rectangleMoving.Velocity = new Vector2(velocity, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _rectangleMoving.Velocity = new Vector2(0, 0);
            }

            Vector2 position = _rectangleMoving.Position;
            _collision = ColliderRegistry.Instance.isColliding(_rectangleMoving.Collider, gameTime);
            if (_collision != null)
            {
                if (_collision.RayVsRectCollision.THitNear > 1f || _collision.RayVsRectCollision.THitNear < 0 )
                {
                    //no collision, but printing still
                    _lastCollision = _collision;
                }
                else
                {
                    _lastCollision = _collision;
                    _rectangleMoving.Velocity = Vector2.Zero;
                    _rectangleMoving.Position = position;// bad, should be a matching edges or something
                }
            }
            _rectangleMoving.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            DebugDraw.Draw(_rectangleMoving, _spriteBatch, _font, 1f);
            DebugDraw.Draw(new Rectangle(150,150,350,200), _spriteBatch, Color.DeepSkyBlue);
            _rectangleStatic.Collider.DrawDebug(_spriteBatch, Color.LightYellow);
            _rectangleMoving.Collider.DrawDebug(_spriteBatch, Color.LightPink);


            if (_lastCollision != null)
            {
                DebugDraw.Draw(_lastCollision, _spriteBatch, _font, 1f, new(10,70));
            }

            if(_collision != null)
            {
                _spriteBatch.DrawCircle(new CircleF(_collision.RayVsRectCollision.Position, 3), 5, Color.Red, 10);
                _spriteBatch.DrawLine(_collision.RayVsRectCollision.Position, _collision.RayVsRectCollision.Position + _collision.RayVsRectCollision.Normal * 50, Color.Yellow);
                _spriteBatch.DrawLine(_collision.RayVsRectCollision.RayOrigin, _collision.RayVsRectCollision.RayOrigin + _collision.RayVsRectCollision.RayDirection, Color.Green, 2);
                _spriteBatch.DrawPoint(_lastCollision.RayVsRectCollision.RayOrigin.ToPoint().X, _lastCollision.RayVsRectCollision.RayOrigin.ToPoint().Y, Color.Pink,3);
                DebugDraw.Draw(_collision, _spriteBatch, _font, 1f, new(10, 110));
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


