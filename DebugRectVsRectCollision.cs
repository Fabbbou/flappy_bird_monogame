using flappyrogue_mg.Game.Core;
using flappyrogue_mg.Game.Core.Collider;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;


namespace flappyrogue_mg
{
    public class DebugRectVsRectCollision : Microsoft.Xna.Framework.Game
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

        private Vector2 _rayOrigin;
        private Vector2 _rayTarget;
        private Vector2 _direction;

        public DebugRectVsRectCollision()
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

            var velocityMouse = 5f;

            //Mouse position set to a point

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _rayOrigin = _rectangleMoving.Collider.Center;
                _rayTarget = Mouse.GetState().Position.ToVector2();
                _direction = (_rayTarget - _rayOrigin);
                _direction.Normalize();
                _rectangleMoving.Velocity += _direction * velocityMouse;
            }

            _rectangleMoving.Update(gameTime);
            _collision = ColliderRegistry.Instance.isColliding(_rectangleMoving.Collider, gameTime);
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

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}


