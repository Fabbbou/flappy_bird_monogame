using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using static Constants;
using Extensions;

namespace flappyrogue_mg.GameSpace
{
    public class Floor : GameEntity
    {
        public readonly PhysicsObject physicsObject;
        private Texture2DRegion _floorTexture;
        private Vector2 _texturePosition;
        private Vector2 _texture2Position;

        public Floor()
        {
            //Rect(string label, float x, float y, CollisionType collisionType, float width, float height)
            physicsObject = PhysicsObjectFactory.Rect("floor", SPRITE_POSITION_FLOOR.X, SPRITE_POSITION_FLOOR.Y, ColliderType.Static, ATLAS_SIZE_FLOOR.X, ATLAS_SIZE_FLOOR.Y);
            _texturePosition = physicsObject.Position;
            _texture2Position = new Vector2(physicsObject.Position.X + ATLAS_SIZE_FLOOR.X, physicsObject.Position.Y);
        }

        public override void LoadContent(ContentManager content)
        {
            // Load the sprite sheet
            _floorTexture = AssetsLoader.Instance.Floor;
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _texturePosition.X -= PipesSpawner.SPEED * deltaTime;
            _texture2Position.X -= PipesSpawner.SPEED * deltaTime;

            if (_texturePosition.X <= -ATLAS_SIZE_FLOOR.X)
            {
                _texturePosition.X = _texture2Position.X + ATLAS_SIZE_FLOOR.X;
            }
            if (_texture2Position.X <= -ATLAS_SIZE_FLOOR.X)
            {
                _texture2Position.X = _texturePosition.X + ATLAS_SIZE_FLOOR.X;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_floorTexture, _texturePosition, LAYER_DEPTH_INGAME);
            spriteBatch.Draw(_floorTexture, _texture2Position,LAYER_DEPTH_INGAME);
        }
    }
}
