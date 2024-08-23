using AsepriteDotNet.Aseprite;
using flappyrogue_mg.Core;
using flappyrogue_mg.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.ViewportAdapters;

namespace flappyrogue_mg.Game
{
    public class Bird : GameObject
    {
        public const int SPRITE_WIDTH = 17;
        public const int SPRITE_HEIGHT = 12;

        private const float SPEED = 400f;
        private const float GRAVITY = 1300f;

        public readonly PhysicsObject physicsObject;
        private SpriteSheet _spriteSheet;
        private AnimatedSprite _idleCycle;
        private BitmapFont _font;
        private Vector2 _jumpForce = new Vector2(0, -SPEED);
        private Vector2 _jumpContinuous = new Vector2(0, -500f);

        private bool _pressedJump = false;

        public Bird()
        {
            physicsObject = new(GameMain.WORLD_WIDTH / 2 - SPRITE_WIDTH / 2, (GameMain.WORLD_HEIGHT - Floor.SPRITE_HEIGHT) / 2 - SPRITE_HEIGHT / 2, SPRITE_WIDTH, SPRITE_HEIGHT);
            physicsObject.Gravity = new Vector2(0, GRAVITY);
        }

        public void Load(ContentManager content, GraphicsDevice graphicsDevice)
        {
            // Load the font
            _font = content.Load<BitmapFont>("fonts/04b19");

            // Load the sprite sheet
            AsepriteFile aseFile = content.Load<AsepriteFile>("sprites/bird");
            _spriteSheet = aseFile.CreateSpriteSheet(graphicsDevice);
            _idleCycle = _spriteSheet.CreateAnimatedSprite("idle"); //tag created in aseprite file selecting the frames to be animated
            _idleCycle.Play();
        }

        public void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _idleCycle.Update(deltaTime);

            // crossplatform jump input
            if (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                if (!_pressedJump)
                {
                    physicsObject.Velocity = _jumpForce;
                    _pressedJump = true;
                }
            }
            else
            {
                _pressedJump = false;
            }

            PhysicsEngine.Instance.MoveAndSlide(physicsObject, gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, ViewportAdapter viewportAdapter, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(_idleCycle, physicsObject.Position);

            //DrawDebug(spriteBatch, content, viewportAdapter);

        }

        private void DrawDebug(SpriteBatch spriteBatch, ContentManager content, ViewportAdapter viewportAdapter)
        {
            float scale = 0.5f;
            spriteBatch.DrawString(_font, $"Position: {physicsObject.Position}", new Vector2(10, 10), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, $"Velocity: {physicsObject.Velocity}", new Vector2(10, 30), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, $"Acceleration: {physicsObject.Acceleration}", new Vector2(10, 50), Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}