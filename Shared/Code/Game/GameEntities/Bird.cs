using AsepriteDotNet.Aseprite;
using flappyrogue_mg.Core;
using Gum.DataTypes;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;
using MonoGame.Extended.BitmapFonts;
using MonoGameGum.GueDeriving;
using System.Collections.Generic;
using Sprite = RenderingLibrary.Graphics.Sprite;
using AnimatedSprite = MonoGame.Aseprite.AnimatedSprite;
using static Constants;
using MonoGame.Extended.Graphics;
using RenderingLibrary.Graphics;
using BitmapFont = MonoGame.Extended.BitmapFonts.BitmapFont;

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
        private AnimatedSprite _idleCycle;
        private BitmapFont _font;
        private Vector2 _jumpForce = new Vector2(0, -BIRD_SPEED);
        private Sprite Sprite;
        private GraphicalUiElement birdGraphicalUiElement;
        public Bird(MainGameScreen mainGameScreen)
        {
            _screen = mainGameScreen;
            PhysicsObject = PhysicsObjectFactory.Circl("bird", STARTING_POSITION_X, STARTING_POSITION_Y, ColliderType.Moving, COLLIDER_RADIUS);
            PhysicsObject.Gravity = new Vector2(0, BIRD_GRAVITY);        }

        public override void LoadContent(ContentManager content)
        {
            // Load the font
            _font = AssetsLoader.Instance.Font;
            _idleCycle = AssetsLoader.Instance.CreateBirdSprite(_screen.GraphicsDevice);
            //the origin of the sprite is the center of the sprite, so the rotation is centered
            //_idleCycle.Origin = CenterSprite;
            //_idleCycle.Play();
            //_idleCycle.LayerDepth = LAYER_DEPTH_INGAME;

            //animatedSpriteIpso = new AnimatedSpriteIpso(_idleCycle, SpriteRuntime);
            birdGraphicalUiElement = _screen.MainGameScreenGum.GetGraphicalUiElementByName("Bird");
            var birdSprite = birdGraphicalUiElement.GetGraphicalUiElementByName("SpriteInstance");
            Sprite = birdSprite.RenderableComponent as Sprite;
            Sprite.CurrentChainName = "Play";
            Sprite.Animate = true;
            //sprite.Width = SPRITE_WIDTH;
            //sprite.Height = SPRITE_HEIGHT;

            //el.WidthUnits = DimensionUnitType.Absolute;
            //el.HeightUnits = DimensionUnitType.Absolute;
            //el.X = STARTING_POSITION_X;
            //el.XUnits = Gum.Converters.GeneralUnitType.PixelsFromSmall;
            //el.Y = STARTING_POSITION_Y;
            //el.YUnits = Gum.Converters.GeneralUnitType.PixelsFromSmall;
            //_screen.MainGameScreenGum.Children.Add(SpriteRuntime);
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _idleCycle.Update(deltaTime);
            //_idleCycle.Rotation = MathHelper.ToRadians(MathHelper.Clamp(PhysicsObject.Velocity.Y * BIRD_ROTATION, -30f, 90f));
            birdGraphicalUiElement.Rotation = MathHelper.Clamp(-PhysicsObject.Velocity.Y * BIRD_ROTATION,-90f, 30f);

            //birdGraphicalUiElement.SetProperty


            List<Collision> collided = PhysicsEngine.Instance.MoveAndSlide(PhysicsObject, gameTime);
            birdGraphicalUiElement.X = this.PhysicsObject.Position.X;
            birdGraphicalUiElement.Y = this.PhysicsObject.Position.Y;
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