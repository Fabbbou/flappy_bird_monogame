using flappyrogue_mg.Game.Core.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;


namespace flappyrogue_mg
{
    public class DebugRayVsRectCollision : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private BoxingViewportAdapter _viewportAdapter;
        private SpriteBatch _spriteBatch;

        private PhysicsObject _rectangle;
        private Vector2 _ray_origin;
        private Vector2 _ray_target;
        private RayVsRectCollision _collision;

        public DebugRayVsRectCollision()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Content.RootDirectory = "Content";

            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.ApplyChanges();

            _rectangle = PhysicsObject.Create(new Vector2(200, 200), 100, 100);
            _rectangle.Gravity = new Vector2(0, 0);
            _ray_origin = new Vector2(100, 20);
            _ray_target = new Vector2(100, 20);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _rectangle.Update(gameTime);

            //Mouse position set to a point
            _ray_target = Mouse.GetState().Position.ToVector2();
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _ray_origin = Mouse.GetState().Position.ToVector2();
            }

            _collision = Collides.RayVsRect(_ray_origin, (_ray_target - _ray_origin), ((RectangleCollider)_rectangle.Collider).Rect);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            if (_collision != null && _collision.THitNear >=0 && _collision.THitNear <=1)
            {
                _rectangle.Collider.DrawDebug(_spriteBatch, Color.LightPink);
                _spriteBatch.DrawCircle(new CircleF(_collision.Position, 10),10, Color.Red, 10);
                _spriteBatch.DrawLine(_collision.Position, _collision.Position + _collision.Normal * 50, Color.Yellow);
            }
            else
            {
                _rectangle.Collider.DrawDebug(_spriteBatch, Color.LightYellow);
            }
            _spriteBatch.DrawLine(_ray_origin, _ray_target, Color.LightGreen);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


