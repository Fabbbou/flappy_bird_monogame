using AsepriteDotNet.Aseprite;
using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using MonoGame.Extended.BitmapFonts;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Extended.Screens;
using System;

namespace flappyrogue_mg.GameSpace
{
    public class Bird : GameEntity
    {
        public const int SPRITE_WIDTH = 17;
        public const int SPRITE_HEIGHT = 12;

        public const int COLLIDER_WIDTH = 9;
        public const int COLLIDER_HEIGHT = 10;
        public const float COLLIDER_RADIUS = 5.5f;
        public readonly Vector2 OffsetCollider = new Vector2(8.5f, 5.8f);

        public const float STARTING_POSITION_X = Constants.WORLD_MIDDLE_SCREEN_WIDTH - SPRITE_WIDTH * .5f;
        public const float STARTING_POSITION_Y = Constants.WORLD_MIDDLE_SCREEN_HEIGHT - SPRITE_HEIGHT * .5f;

        private const float BIRD_SPEED = 200f;
        private const float BIRD_GRAVITY = 450f;
        private const float BIRD_ROTATION = 0.1f;

        private GameScreen _screen;
        public readonly PhysicsObject PhysicsObject;
        private SpriteSheet _spriteSheet;
        private AnimatedSprite _idleCycle;
        private BitmapFont _font;
        private SoundEffect _flapSound;
        private Vector2 _jumpForce = new Vector2(0, -BIRD_SPEED);

        // by default, if the button is maintained, the bird will jump continuously.
        // this variable is used to avoid this behavior
        private bool _pressedButtonJump = false;

        public Bird(GameScreen gameScreen)
        {
            _screen = gameScreen;
            PhysicsObject = PhysicsObjectFactory.Circl("bird", STARTING_POSITION_X + OffsetCollider.X, STARTING_POSITION_Y + OffsetCollider.Y, CollisionType.Moving, COLLIDER_RADIUS);
            PhysicsObject.Gravity = new Vector2(0, BIRD_GRAVITY);
        }

        public void LoadContent(ContentManager content)
        {
            // Load the font
            _font = content.Load<BitmapFont>("fonts/04b19");

            // Load the sprite sheet
            AsepriteFile aseFile = content.Load<AsepriteFile>("sprites/bird");
            _spriteSheet = aseFile.CreateSpriteSheet(_screen.GraphicsDevice);
            _idleCycle = _spriteSheet.CreateAnimatedSprite("idle"); //tag created in aseprite file selecting the frames to be animated
            _idleCycle.Play();

            //load sfx_wing sound
            _flapSound = content.Load<SoundEffect>("sounds/sfx_wing");
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _idleCycle.Update(deltaTime);
            Jump();

            _idleCycle.Rotation = MathHelper.ToRadians(MathHelper.Clamp(PhysicsObject.Velocity.Y * BIRD_ROTATION, -90f, 30f));

            PhysicsEngine.Instance.MoveAndSlide(PhysicsObject, gameTime);
        }

        // crossplatform jump input
        private void Jump()
        {
            //if android, jump on touch
            bool hasCrossplatformJump = false;
            #if ANDROID
                hasCrossplatformJump = TouchPanel.GetState().Count > 0;
            #else
                hasCrossplatformJump = Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed;
            #endif

            if (hasCrossplatformJump)
            {
                if (!_pressedButtonJump)
                {
                    PhysicsObject.Velocity = _jumpForce;
                    _pressedButtonJump = true;
                    _flapSound.Play();
                }
            }
            else
            {
                _pressedButtonJump = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_idleCycle, PhysicsObject.Position - OffsetCollider);
        }
    }
}