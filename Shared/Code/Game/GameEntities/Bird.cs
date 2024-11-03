using flappyrogue_mg.Core;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Sprite = RenderingLibrary.Graphics.Sprite;

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
        public PhysicsObject PhysicsObject;
        private Vector2 _jumpForce = new Vector2(0, -BIRD_SPEED);
        private Sprite Sprite;
        private readonly GraphicalUiElement _birdGraphicalUiElement;

        public Bird(MainGameScreen mainGameScreen, GraphicalUiElement rootIngameWorldContainer)
        {
            _screen = mainGameScreen;
            _birdGraphicalUiElement = _screen.MainGameScreenGum.GetGraphicalUiElementByName("Bird");
            PhysicsObject = PhysicsObjectFactory.Circl("bird", _birdGraphicalUiElement.X, _birdGraphicalUiElement.Y, ColliderType.Moving, COLLIDER_RADIUS, rootGraphicalUiElement: rootIngameWorldContainer, graphicalUiElement: _birdGraphicalUiElement, entity: this);
            PhysicsObject.Gravity = new Vector2(0, BIRD_GRAVITY);
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _birdGraphicalUiElement.Rotation = MathHelper.Clamp(-PhysicsObject.Velocity.Y * BIRD_ROTATION,-90f, 30f);

            List<Collision> collided = PhysicsEngine.Instance.MoveAndSlide(PhysicsObject, gameTime);
            CheckDeath(collided);
        }

        public void Pause()
        {
            IsPaused = true;
            _birdGraphicalUiElement.SetProperty("SpriteInstance.Animate", false);
        }

        public void Resume()
        {
            IsPaused = false;
            _birdGraphicalUiElement.SetProperty("SpriteInstance.Animate", true);
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

        }

        public void UnloadContent()
        {

        }
    }
}