using AsepriteDotNet.Aseprite;
using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;
using MonoGame.Extended.BitmapFonts;
using System.Collections.Generic;

namespace flappyrogue_mg.GameSpace
{
    public class Bird : GameEntity
    {
        public const int SPRITE_WIDTH = 17;
        public const int SPRITE_HEIGHT = 12;

        public const float COLLIDER_RADIUS = 5.5f;

        public static readonly Vector2 CenterSprite = new Vector2(SPRITE_WIDTH * .5f, SPRITE_HEIGHT * .5f);
        public float STARTING_POSITION_X = 64 + CenterSprite.X;
        public float STARTING_POSITION_Y = 84f + CenterSprite.Y;

        private const float BIRD_SPEED = 200f;
        private const float BIRD_GRAVITY = 450f;
        private const float BIRD_ROTATION = 0.17f;

        private MainGameScreen _screen;
        public readonly PhysicsObject PhysicsObject;
        private SpriteSheet _spriteSheet;
        private AnimatedSprite _idleCycle;
        private BitmapFont _font;
        private Vector2 _jumpForce = new Vector2(0, -BIRD_SPEED);
        //private ClickableRegionHandler _jumpBirdClickableRegionHandler;
        public Bird(MainGameScreen mainGameScreen)
        {
            _screen = mainGameScreen;
            PhysicsObject = PhysicsObjectFactory.Circl("bird", STARTING_POSITION_X, STARTING_POSITION_Y, ColliderType.Moving, COLLIDER_RADIUS);
            PhysicsObject.Gravity = new Vector2(0, BIRD_GRAVITY);
            //_jumpBirdClickableRegionHandler = new ClickableRegionHandler(this, _screen.Camera, Jump, new Rectangle(Constants.CLICK_REGION_POSITION_JUMP_REGION.ToPoint(), Constants.CLICK_REGION_SIZE_JUMP_REGION.ToPoint()));
        }

        public override void LoadContent(ContentManager content)
        {
            // Load the font
            _font = AssetsLoader.Instance.Font;
            _idleCycle = AssetsLoader.Instance.CreateBirdSprite(_screen.GraphicsDevice);
            //the origin of the sprite is the center of the sprite, so the rotation is centered
            _idleCycle.Origin = CenterSprite;
            _idleCycle.Play();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _idleCycle.Update(deltaTime);

            _idleCycle.Rotation = MathHelper.ToRadians(MathHelper.Clamp(PhysicsObject.Velocity.Y * BIRD_ROTATION, -30f, 90f));

            List<Collision> collided = PhysicsEngine.Instance.MoveAndSlide(PhysicsObject, gameTime);
            CheckDeath(collided);
        }

        public void Jump()
        {
            PhysicsObject.Velocity = _jumpForce;
            SoundManager.Instance.PlayJumpSound();
        }
        private void CheckDeath(List<Collision> collided)
        {
            if (collided.Count > 0)
            {
                _screen.StateMachine.ChangeState(new DeathState(_screen));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_idleCycle, PhysicsObject.Position);
        }
    }
}