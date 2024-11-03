using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static Constants;
using Gum.Wireframe;

namespace flappyrogue_mg.GameSpace
{
    public class Floor : GameEntity
    {
        public readonly PhysicsObject physicsObject;
        private readonly GraphicalUiElement _mainGameScreenGum;
        private readonly GraphicalUiElement _floor;
        private readonly GraphicalUiElement _nextFloor;
        public Floor(GraphicalUiElement mainGameScreenGum)
        {
            _mainGameScreenGum = mainGameScreenGum;
            _floor = _mainGameScreenGum.GetGraphicalUiElementByName("Floor");
            _nextFloor = _mainGameScreenGum.GetGraphicalUiElementByName("NextFloor");
            var backgroundPic = _mainGameScreenGum.GetGraphicalUiElementByName("BackgroundPic");
            physicsObject = PhysicsObjectFactory.Rect(label: "floor", x: SPRITE_POSITION_FLOOR.X, y: SPRITE_POSITION_FLOOR.Y, collisionType: ColliderType.Static, width: ATLAS_SIZE_FLOOR.X, height: ATLAS_SIZE_FLOOR.Y, graphicalUiElement: _floor, rootGraphicalUiElement: backgroundPic, entity: this);
        }

        public override void LoadContent(ContentManager content)
        {
            // Load the sprite sheet
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var currentScale = MainRegistry.I.CurrentFrameScale;
            _floor.X -= Pipes.GlobalPipesSpeed * deltaTime;
            _nextFloor.X -= Pipes.GlobalPipesSpeed * deltaTime;

            if (_floor.X <= -WORLD_WIDTH)
            {
                _floor.X = _nextFloor.X + 2* WORLD_WIDTH;
            }
            if (_nextFloor.X <= -WORLD_WIDTH * 2)
            {
                _nextFloor.X = _floor.X;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            // nothing to do cause its gum
        }
    }
}
