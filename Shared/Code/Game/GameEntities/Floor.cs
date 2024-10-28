using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using static Constants;
using Extensions;
using MonoGame.Extended;
using Gum.Wireframe;
using RenderingLibrary;

namespace flappyrogue_mg.GameSpace
{
    public class Floor : GameEntity
    {
        public readonly PhysicsObject physicsObject;
        private GraphicalUiElement _mainGameScreenGum;
        private GraphicalUiElement _floor;
        private GraphicalUiElement _nextFloor;
        private GraphicalUiElement _backgroundPic;
        public Floor(GraphicalUiElement mainGameScreenGum)
        {
            _mainGameScreenGum = mainGameScreenGum;
            _floor = _mainGameScreenGum.GetGraphicalUiElementByName("Floor");
            _nextFloor = _mainGameScreenGum.GetGraphicalUiElementByName("NextFloor");
            //Rect(string label, float x, float y, CollisionType collisionType, float width, float height)
            //see how to handle the scale change when the game is resized, for physicsobject, as there where scaled on rendering before.
            physicsObject = PhysicsObjectFactory.Rect("floor", SPRITE_POSITION_FLOOR.X, SPRITE_POSITION_FLOOR.Y, ColliderType.Static, ATLAS_SIZE_FLOOR.X, ATLAS_SIZE_FLOOR.Y);
            _backgroundPic = _mainGameScreenGum.GetGraphicalUiElementByName("BackgroundPic");
        }

        public override void LoadContent(ContentManager content)
        {
            // Load the sprite sheet
           
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var currentScale = MainRegistry.I.CurrentFrameScale;
            _floor.X -= PipesSpawner.SPEED * deltaTime * currentScale.Scale;
            _nextFloor.X -= PipesSpawner.SPEED * deltaTime * currentScale.Scale;
            float absoluteWorldWidth = ((float)((float)WORLD_WIDTH) * (float)currentScale.Scale);

            if (_floor.X <= -absoluteWorldWidth)
            {
                _floor.X = _nextFloor.X + 2*absoluteWorldWidth ;
            }
            if (_nextFloor.X <= -absoluteWorldWidth*2)
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
