using AsepriteDotNet.Aseprite;
using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;
using MonoGame.Extended.BitmapFonts;

namespace flappyrogue_mg.GameSpace
{
    public class Bird : GameEntity
    {
        public const int SPRITE_WIDTH = 17;
        public const int SPRITE_HEIGHT = 12;

        public const float COLLIDER_RADIUS = 5.5f;

        public const float STARTING_POSITION_X = Constants.WORLD_MIDDLE_SCREEN_WIDTH;
        public const float STARTING_POSITION_Y = Constants.WORLD_MIDDLE_SCREEN_HEIGHT;

        private const float BIRD_SPEED = 200f;
        private const float BIRD_GRAVITY = 450f;
        private const float BIRD_ROTATION = 0.17f;

        private MainGameScreen _screen;
        public readonly PhysicsObject PhysicsObject;
        private SpriteSheet _spriteSheet;
        private AnimatedSprite _idleCycle;
        private BitmapFont _font;
        private Vector2 _jumpForce = new Vector2(0, -BIRD_SPEED);
        private ClickableRegionHandler _jumpBirdClickableRegionHandler;
        public Bird(MainGameScreen mainGameScreen)
        {
            _screen = mainGameScreen;
            PhysicsObject = PhysicsObjectFactory.Circl("bird", STARTING_POSITION_X, STARTING_POSITION_Y, CollisionType.Moving, COLLIDER_RADIUS);
            PhysicsObject.Gravity = new Vector2(0, BIRD_GRAVITY);
            _jumpBirdClickableRegionHandler = new ClickableRegionHandler(Entity, _screen.Camera, Jump, new Rectangle(Constants.POSITION_JUMP_REGION.ToPoint(), Constants.SIZE_JUMP_REGION.ToPoint()));
        }

        public override void LoadContent(ContentManager content)
        {
            // Load the font
            _font = content.Load<BitmapFont>("fonts/04b19");

            // Load the sprite sheet
            AsepriteFile aseFile = content.Load<AsepriteFile>("sprites/bird");
            _spriteSheet = aseFile.CreateSpriteSheet(_screen.GraphicsDevice);
            //tag created in aseprite file selecting the frames to be animated
            _idleCycle = _spriteSheet.CreateAnimatedSprite("idle");
            //the origin of the sprite is the center of the sprite, so the rotation is centered
            _idleCycle.Origin = new Vector2(SPRITE_WIDTH / 2f, SPRITE_HEIGHT / 2f);
            _idleCycle.Play();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _idleCycle.Update(deltaTime);

            _idleCycle.Rotation = MathHelper.ToRadians(MathHelper.Clamp(PhysicsObject.Velocity.Y * BIRD_ROTATION, -30f, 90f));

            PhysicsEngine.Instance.MoveAndSlide(PhysicsObject, gameTime);
        }

        private void Jump()
        {
            PhysicsObject.Velocity = _jumpForce;
            SoundManager.Instance.PlayJumpSound();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_idleCycle, PhysicsObject.Position);
        }
    }
}