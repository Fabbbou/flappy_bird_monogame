using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using System;

namespace flappyrogue_mg.Game
{
    public class Floor : GameObject
    {
        public const int SPRITE_WIDTH = 168;
        public const int SPRITE_HEIGHT = 56;
        public const float STARTING_POSITION_X = 0;
        public const float STARTING_POSITION_Y = GameMain.WORLD_HEIGHT - SPRITE_HEIGHT;

        public readonly PhysicsObject physicsObject;
        private Texture2D _spriteSheet;
        private Vector2 _texturePosition;
        private Vector2 _texture2Position;

        public Floor()
        {
            physicsObject = new PhysicsObject("floor", STARTING_POSITION_X, STARTING_POSITION_Y, SPRITE_WIDTH, SPRITE_HEIGHT, ColliderType.Static)
            {
                //make the floor static
                Gravity = new Vector2(0, 0),
                Friction = new Vector2(0, 0)
            };
            _texturePosition = physicsObject.Position;
            _texture2Position = new Vector2(physicsObject.Position.X + SPRITE_WIDTH, physicsObject.Position.Y);
        }

        public void LoadSingleInstance(ContentManager content, GraphicsDevice graphicsDevice)
        {
            // Load the sprite sheet
            _spriteSheet = content.Load<Texture2D>("sprites/floor");
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _texturePosition.X -= PipesSpawner.SPEED * deltaTime;
            _texture2Position.X -= PipesSpawner.SPEED * deltaTime;

            if (_texturePosition.X <= -SPRITE_WIDTH)
            {
                _texturePosition.X = _texture2Position.X + SPRITE_WIDTH;
            }
            if (_texture2Position.X <= -SPRITE_WIDTH)
            {
                _texture2Position.X = _texturePosition.X + SPRITE_WIDTH;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_spriteSheet, _texturePosition, Color.White);
            spriteBatch.Draw(_spriteSheet, _texture2Position, Color.White);
        }
    }
}
