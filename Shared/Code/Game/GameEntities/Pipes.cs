using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using System.Diagnostics;
using static Constants;
using Extensions;
using Gum.Wireframe;

namespace flappyrogue_mg.GameSpace
{
    public class Pipes : GameEntity
    {

        //gum stuff=
        private const int PIPE_WIDTH = 52;
        private const int PIPE_HEIGHT = 160;
        private const int SCORING_ZONE_WIDTH = PIPE_WIDTH - CROP_SCORING_ZONE_WIDTH * 2;

        private readonly int _gapBetweenPipes;
        private readonly GraphicalUiElement _rootIngameWorld;
        private readonly GraphicalUiElement _gumPipesContainer;
        //end gum

        public const int CROP_SCORING_ZONE_WIDTH = 3;
        public PhysicsObject PhysicsObjectPipeTop;
        public PhysicsObject PhysicsObjectPipeBottom;
        public PhysicsObject ScoringZoneTriggerOnceCollider;

        private float _speedForce;

        public int Right => (int)PhysicsObjectPipeTop.Position.X + (int)ATLAS_SIZE_PIPE.X;
        /// <summary>
        /// The Max height the bottom pipe can have is 40 pixels, or it will be flying out of the floor
        /// </summary>
        /// <param name="xOffsetFromRightBorder">x offset to add to the pipes default spawning x position, which is the right border of the screen</param>
        /// <param name="yOffsetFromTop">y offset to change the position of the pipes. Reduces the height of the pipes by world pixels</param>
        /// <param name="gapHeight">the gap between the top pipe and the bottom pipe. bottom pipe position is top.y+SPRITE_HEIGHT+gap</param>
        //public Pipes(string label, float xOffsetFromRightBorder, float yOffsetFromTop, float gapHeight, float speed)
        //{
        //    _speedForce = speed;
        //    float xPosition = WORLD_WIDTH - ATLAS_SIZE_PIPE.X + xOffsetFromRightBorder;
        //    PhysicsObjectPipeTop = PhysicsObjectFactory.Rect("pipe top" + label, xPosition, -ATLAS_SIZE_PIPE.Y + yOffsetFromTop, ColliderType.Moving, ATLAS_SIZE_PIPE.X, ATLAS_SIZE_PIPE.Y);
        //    PhysicsObjectPipeTop.Gravity = Vector2.Zero;
        //    PhysicsObjectPipeBottom = PhysicsObjectFactory.Rect("pipe bottom" + label, xPosition, yOffsetFromTop + gapHeight, ColliderType.Moving, ATLAS_SIZE_PIPE.X, ATLAS_SIZE_PIPE.Y);
        //    PhysicsObjectPipeBottom.Gravity = Vector2.Zero;
        //    ScoringZoneTriggerOnceCollider = PhysicsObjectFactory.AreaRectTriggerOnce("scoring zone" + label, xPosition + CROP_SCORING_ZONE_WIDTH, yOffsetFromTop, ColliderType.AreaCastTrigger, ATLAS_SIZE_PIPE.X - CROP_SCORING_ZONE_WIDTH*2, gapHeight, onScoringZoneTriggered);
        //}

        /// <summary>
        /// gumPipesContainer has aalready been added in the screen, so we can use it to get the pipes if needed
        /// </summary>
        public Pipes(GraphicalUiElement rootIngameWorld, GraphicalUiElement gumPipesContainer, int gapBetweenPipes)
        {
            _rootIngameWorld = rootIngameWorld;
            _gumPipesContainer = gumPipesContainer;
            _gapBetweenPipes = gapBetweenPipes;
            _speedForce = 60f;
        }

        public void onScoringZoneTriggered()
        {
            ScoreManager.Instance.IncreaseScore();
            Debug.WriteLine("Scored! Current score: " + ScoreManager.Instance.CurrentScore);
        }

        public override void LoadContent(ContentManager content)
        {
            Vector2 topleftPipeTop = new(_gumPipesContainer.X, _gumPipesContainer.Y);
            Vector2 topleftPipeBottom = new(_gumPipesContainer.X, _gumPipesContainer.Y + PIPE_HEIGHT + _gapBetweenPipes);
            Vector2 topleftScoringZone = new(_gumPipesContainer.X + CROP_SCORING_ZONE_WIDTH, _gumPipesContainer.Y + PIPE_HEIGHT);

            PhysicsObjectPipeTop = PhysicsObjectFactory.Rect("pipe top", topleftPipeTop.X, topleftPipeTop.Y, ColliderType.Moving, PIPE_WIDTH, PIPE_HEIGHT, _rootIngameWorld, _gumPipesContainer, debugColor: Color.Blue);
            PhysicsObjectPipeTop.Gravity = Vector2.Zero;
            PhysicsObjectPipeBottom = PhysicsObjectFactory.Rect("pipe bottom", topleftPipeBottom.X, topleftPipeBottom.Y, ColliderType.Static, PIPE_WIDTH, PIPE_HEIGHT, _rootIngameWorld, _gumPipesContainer);
            PhysicsObjectPipeBottom.Gravity = Vector2.Zero;
            ScoringZoneTriggerOnceCollider = PhysicsObjectFactory.AreaRectTriggerOnce("scoring zone", topleftScoringZone.X, topleftScoringZone.Y, ColliderType.AreaCastTrigger, SCORING_ZONE_WIDTH, _gapBetweenPipes, onScoringZoneTriggered);
        }

        public override void Update(GameTime gameTime)
        {
            PhysicsObjectPipeTop.Velocity = new Vector2(-_speedForce, 0);
            PhysicsObjectPipeBottom.Velocity = new Vector2(-_speedForce, 0);
            ScoringZoneTriggerOnceCollider.Velocity = new Vector2(-_speedForce, 0);

            //this allows to move pipes and ignore any possible collisions
            //means that pipes dont collide to anything, but anything collides to them by using MoveAndCollide
            PhysicsObjectPipeTop.Update(gameTime);
            PhysicsObjectPipeBottom.Update(gameTime);
            ScoringZoneTriggerOnceCollider.Update(gameTime);
        }

        public void Kill()
        {
            PhysicsObjectPipeTop.Kill();
            PhysicsObjectPipeBottom.Kill();
            ScoringZoneTriggerOnceCollider.Kill();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
