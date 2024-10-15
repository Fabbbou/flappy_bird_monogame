using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using System.Diagnostics;
using static Constants;

namespace flappyrogue_mg.GameSpace
{
    public class Pipes : GameEntity
    {
        public const int CROP_SCORING_ZONE_WIDTH = 3;

        public readonly PhysicsObject PhysicsObjectPipeTop;
        public readonly PhysicsObject PhysicsObjectPipeBottom;
        public readonly PhysicsObject ScoringZoneCollider;
        private Texture2DRegion _pipeTopTexture;
        private Texture2DRegion _pipeBottomTexture;
        private float _speedForce;

        public int Right => (int)PhysicsObjectPipeTop.Position.X + (int)ATLAS_SIZE_PIPE.X;
        /// <summary>
        /// The Max height the bottom pipe can have is 40 pixels, or it will be flying out of the floor
        /// </summary>
        /// <param name="xOffsetFromRightBorder">x offset to add to the pipes default spawning x position, which is the right border of the screen</param>
        /// <param name="yOffsetFromTop">y offset to change the position of the pipes. Reduces the height of the pipes by world pixels</param>
        /// <param name="gapHeight">the gap between the top pipe and the bottom pipe. bottom pipe position is top.y+SPRITE_HEIGHT+gap</param>
        public Pipes(string label, float xOffsetFromRightBorder, float yOffsetFromTop, float gapHeight, float speed)
        {
            _speedForce = speed;
            float xPosition = WORLD_WIDTH - ATLAS_SIZE_PIPE.X + xOffsetFromRightBorder;
            PhysicsObjectPipeTop = PhysicsObjectFactory.Rect("pipe top" + label, xPosition, -ATLAS_SIZE_PIPE.Y + yOffsetFromTop, ColliderType.Moving, ATLAS_SIZE_PIPE.X, ATLAS_SIZE_PIPE.Y);
            PhysicsObjectPipeTop.Gravity = Vector2.Zero;
            PhysicsObjectPipeBottom = PhysicsObjectFactory.Rect("pipe bottom" + label, xPosition, yOffsetFromTop + gapHeight, ColliderType.Moving, ATLAS_SIZE_PIPE.X, ATLAS_SIZE_PIPE.Y);
            PhysicsObjectPipeBottom.Gravity = Vector2.Zero;
            ScoringZoneCollider = PhysicsObjectFactory.AreaRectTriggerOnce("scoring zone" + label, xPosition + CROP_SCORING_ZONE_WIDTH, yOffsetFromTop, ColliderType.AreaCastTrigger, ATLAS_SIZE_PIPE.X - CROP_SCORING_ZONE_WIDTH*2, gapHeight, onScoringZoneTriggered);
        }

        public void onScoringZoneTriggered()
        {
            ScoreManager.Instance.IncreaseScore();
            Debug.WriteLine("Scored! Current score: " + ScoreManager.Instance.CurrentScore);
        }

        public override void LoadContent(ContentManager content)
        {
            //textures are loaded from the atlas in GameMain.cs
            //we still load the atlas here to get the texture
            _pipeTopTexture = AssetsLoader.Instance.PipeTop;
            _pipeBottomTexture = AssetsLoader.Instance.PipeBottom;
        }

        public void BypassLoadContent()
        {
            _pipeTopTexture = AssetsLoader.Instance.PipeTop;
            _pipeBottomTexture = AssetsLoader.Instance.PipeBottom;
        }

        public override void Update(GameTime gameTime)
        {
            PhysicsObjectPipeTop.Velocity = new Vector2(-_speedForce, 0);
            PhysicsObjectPipeBottom.Velocity = new Vector2(-_speedForce, 0);
            ScoringZoneCollider.Velocity = new Vector2(-_speedForce, 0);

            //this allows to move pipes and ignore any possible collisions
            PhysicsObjectPipeTop.Update(gameTime);
            PhysicsObjectPipeBottom.Update(gameTime);
            ScoringZoneCollider.Update(gameTime);
        }

        /// <summary>
        /// Draw method used when only one pipe is created (means the LoadSingleInstance method was called on a Load method)
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Extensions.SpriteBatchExtensions.Draw(spriteBatch, _pipeTopTexture, PhysicsObjectPipeTop.Position, LAYER_DEPTH_INGAME);
            Extensions.SpriteBatchExtensions.Draw(spriteBatch, _pipeBottomTexture, PhysicsObjectPipeBottom.Position, LAYER_DEPTH_INGAME);
        }

        public void Kill()
        {
            PhysicsObjectPipeTop.Kill();
            PhysicsObjectPipeBottom.Kill();
            ScoringZoneCollider.Kill();
        }
    }
}
