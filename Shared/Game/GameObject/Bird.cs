﻿using AsepriteDotNet.Aseprite;
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

namespace flappyrogue_mg.GameSpace
{
    public class Bird : GameObject
    {
        public const int SPRITE_WIDTH = 17;
        public const int SPRITE_HEIGHT = 12;
        public const int BORDER_CROP_COLLIDER = 2;
        public const int COLLIDER_WIDTH = SPRITE_WIDTH - BORDER_CROP_COLLIDER * 2;
        public const int COLLIDER_HEIGHT = SPRITE_HEIGHT - BORDER_CROP_COLLIDER * 2;

        public const float STARTING_POSITION_X = Constants.WORLD_WIDTH / 2 - SPRITE_WIDTH / 2;
        public const float STARTING_POSITION_Y = Constants.PLAYABLE_WORLD_HEIGHT / 2 - SPRITE_HEIGHT / 2;

        private const float SPEED = 200f;
        private const float GRAVITY = 450f;

        private GameScreen _screen;
        public readonly PhysicsObject PhysicsObject;
        private SpriteSheet _spriteSheet;
        private AnimatedSprite _idleCycle;
        private BitmapFont _font;
        private SoundEffect _flapSound;
        private Vector2 _jumpForce = new Vector2(0, -SPEED);
        private Vector2 _jumpContinuous = new Vector2(0, -500f);

        // by default, if the button is maintained, the bird will jump continuously.
        // this variable is used to avoid this behavior
        private bool _pressedButtonJump = false;

        public Bird(GameScreen gameScreen)
        {
            _screen = gameScreen;
            PhysicsObject = new("bird", STARTING_POSITION_X, STARTING_POSITION_Y, COLLIDER_WIDTH, COLLIDER_HEIGHT, CollisionType.Moving, Vector2.One * BORDER_CROP_COLLIDER);
            PhysicsObject.Gravity = new Vector2(0, GRAVITY);
        }

        public void LoadSingleInstance(ContentManager content)
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
            spriteBatch.Draw(_idleCycle, PhysicsObject.Position);
        }
    }
}