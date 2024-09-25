using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace flappyrogue_mg.GameSpace
{
    public class Pipes : GameObject
    {
        public const int SPRITE_WIDTH = 26;
        public const int SPRITE_HEIGHT = 160;

        public readonly PhysicsObject PhysicsObjectPipeTop;
        public readonly PhysicsObject PhysicsObjectPipeBottom;
        private Texture2DRegion _pipeTopTexture;
        private Texture2DRegion _pipeBottomTexture;
        private float _speedForce;

        /// <summary>
        /// The Max height the bottom pipe can have is 40 pixels, or it will be flying out of the floor
        /// </summary>
        /// <param name="xOffsetFromRightBorder">x offset to add to the pipes default spawning x position, which is the right border of the screen</param>
        /// <param name="yOffsetFromTop">y offset to change the position of the pipes. Reduces the height of the pipes by world pixels</param>
        /// <param name="gapHeight">the gap between the top pipe and the bottom pipe. bottom pipe position is top.y+SPRITE_HEIGHT+gap</param>
        public Pipes(float xOffsetFromRightBorder, float yOffsetFromTop, float gapHeight, float speed)
        {
            _speedForce = speed;
            float xPosition = Constants.WORLD_WIDTH - SPRITE_WIDTH + xOffsetFromRightBorder;
            PhysicsObjectPipeTop = new PhysicsObject("pipe top", xPosition, -SPRITE_HEIGHT + yOffsetFromTop, SPRITE_WIDTH, SPRITE_HEIGHT, CollisionType.Moving, Vector2.Zero)
            {
                Gravity = new Vector2(0, 0),
                Friction = new Vector2(0, 0)
            };
            PhysicsObjectPipeBottom = new PhysicsObject("pipe bottom", xPosition, yOffsetFromTop + gapHeight, SPRITE_WIDTH, SPRITE_HEIGHT, CollisionType.Moving, Vector2.Zero)
            {
                Gravity = new Vector2(0, 0),
                Friction = new Vector2(0, 0)
            };
        }
        public void LoadSingleInstance(ContentManager content)
        {
            //textures are loaded from the atlas in GameMain.cs
            //we still load the atlas here to get the texture
            _pipeTopTexture = GameAtlasTextures.Instance.PipeTop;
            _pipeBottomTexture = GameAtlasTextures.Instance.PipeBottom;
        }

        public void Update(GameTime gameTime)
        {
            PhysicsObjectPipeTop.Velocity = new Vector2(-_speedForce, 0);
            PhysicsObjectPipeBottom.Velocity = new Vector2(-_speedForce, 0);
            
            //this allows to move pipes and ignore any possible collisions
            PhysicsObjectPipeTop.Update(gameTime);
            PhysicsObjectPipeBottom.Update(gameTime);
        }

        /// <summary>
        /// Draw method used when only one pipe is created (means the LoadSingleInstance method was called on a Load method)
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_pipeTopTexture, PhysicsObjectPipeTop.Position, Color.White);
            spriteBatch.Draw(_pipeBottomTexture, PhysicsObjectPipeBottom.Position, Color.White);
        }

        /// <summary>
        /// Draw method used when the textures are loaded before this pipe has been created
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="pipeTopTexture"></param>
        /// <param name="pipeBottomTexture"></param>
        public void Draw(SpriteBatch spriteBatch, Texture2DRegion pipeTopTexture, Texture2DRegion pipeBottomTexture)
        {
            spriteBatch.Draw(pipeTopTexture, PhysicsObjectPipeTop.Position, Color.White);
            spriteBatch.Draw(pipeBottomTexture, PhysicsObjectPipeBottom.Position, Color.White);
        }

        public void Kill()
        {
            PhysicsObjectPipeTop.Kill();
            PhysicsObjectPipeBottom.Kill();
        }
    }
}
