using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using System.Diagnostics;

namespace flappyrogue_mg.GameSpace
{
    public class Pipes : GameEntity
    {
        public const int SPRITE_WIDTH = 26;
        public const int SPRITE_HEIGHT = 160;
        public const int CROP_SCORING_ZONE_WIDTH = 3;

        public readonly PhysicsObject PhysicsObjectPipeTop;
        public readonly PhysicsObject PhysicsObjectPipeBottom;
        public readonly PhysicsObject ScoringZone;
        private Texture2DRegion _pipeTopTexture;
        private Texture2DRegion _pipeBottomTexture;
        private float _speedForce;

        /// <summary>
        /// The Max height the bottom pipe can have is 40 pixels, or it will be flying out of the floor
        /// </summary>
        /// <param name="xOffsetFromRightBorder">x offset to add to the pipes default spawning x position, which is the right border of the screen</param>
        /// <param name="yOffsetFromTop">y offset to change the position of the pipes. Reduces the height of the pipes by world pixels</param>
        /// <param name="gapHeight">the gap between the top pipe and the bottom pipe. bottom pipe position is top.y+SPRITE_HEIGHT+gap</param>
        public Pipes(string label, float xOffsetFromRightBorder, float yOffsetFromTop, float gapHeight, float speed)
        {
            _speedForce = speed;
            float xPosition = Constants.WORLD_WIDTH - SPRITE_WIDTH + xOffsetFromRightBorder;
            PhysicsObjectPipeTop = PhysicsObjectFactory.Rect("pipe top" + label, xPosition, -SPRITE_HEIGHT + yOffsetFromTop, CollisionType.Moving, SPRITE_WIDTH, SPRITE_HEIGHT);
            PhysicsObjectPipeTop.Gravity = Vector2.Zero;
            PhysicsObjectPipeBottom = PhysicsObjectFactory.Rect("pipe bottom" + label, xPosition, yOffsetFromTop + gapHeight, CollisionType.Moving, SPRITE_WIDTH, SPRITE_HEIGHT);
            PhysicsObjectPipeBottom.Gravity = Vector2.Zero;
            ScoringZone = PhysicsObjectFactory.AreaRectTriggerOnce("scoring zone" + label, xPosition + CROP_SCORING_ZONE_WIDTH, yOffsetFromTop, CollisionType.AreaCastTrigger, SPRITE_WIDTH - CROP_SCORING_ZONE_WIDTH*2, gapHeight, onScoringZoneTriggered);
        }

        public void onScoringZoneTriggered()
        {
            ScoreManager.Instance.IncreaseScore();
            Debug.WriteLine("Scored! Current score: " + ScoreManager.Instance.CurrentScore);
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            //textures are loaded from the atlas in GameMain.cs
            //we still load the atlas here to get the texture
            _pipeTopTexture = PreloadedAssets.Instance.PipeTop;
            _pipeBottomTexture = PreloadedAssets.Instance.PipeBottom;
        }

        public void BypassLoadContent()
        {
            _pipeTopTexture = PreloadedAssets.Instance.PipeTop;
            _pipeBottomTexture = PreloadedAssets.Instance.PipeBottom;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            PhysicsObjectPipeTop.Velocity = new Vector2(-_speedForce, 0);
            PhysicsObjectPipeBottom.Velocity = new Vector2(-_speedForce, 0);
            ScoringZone.Velocity = new Vector2(-_speedForce, 0);

            //this allows to move pipes and ignore any possible collisions
            PhysicsObjectPipeTop.Update(gameTime);
            PhysicsObjectPipeBottom.Update(gameTime);
            ScoringZone.Update(gameTime);
        }

        /// <summary>
        /// Draw method used when only one pipe is created (means the LoadSingleInstance method was called on a Load method)
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(_pipeTopTexture, PhysicsObjectPipeTop.Position, Color.White);
            spriteBatch.Draw(_pipeBottomTexture, PhysicsObjectPipeBottom.Position, Color.White);
        }

        public void Kill()
        {
            PhysicsObjectPipeTop.Kill();
            PhysicsObjectPipeBottom.Kill();
        }
    }
}
