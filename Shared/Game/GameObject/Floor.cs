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

        public Floor()
        {
            physicsObject = new PhysicsObject("floor", STARTING_POSITION_X, STARTING_POSITION_Y, SPRITE_WIDTH, SPRITE_HEIGHT, ColliderType.Static)
            {
                //make the floor static
                Gravity = new Vector2(0, 0),
                Friction = new Vector2(0, 0)
            };
        }

        public void Load(ContentManager content, GraphicsDevice graphicsDevice)
        {
            // Load the sprite sheet
            _spriteSheet = content.Load<Texture2D>("sprites/floor");
        }

        public void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            //nothing to do for now... will be animated later to makke the floor slide when moving
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, ViewportAdapter viewportAdapter, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(_spriteSheet, physicsObject.Position, Color.White);
            
            //physicsObject.Collider.DebugDraw(spriteBatch);
        }
    }
}
