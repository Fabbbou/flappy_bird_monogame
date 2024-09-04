using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.ViewportAdapters;

namespace flappyrogue_mg.Game
{
    public class Pipes : GameObject
    {
        public const int SPRITE_WIDTH = 26;
        public const int SPRITE_HEIGHT = 160;

        public const int PIPES_GAP = 60;
        public const int PIPE_TOP_POSITION_X = GameMain.WORLD_WIDTH - SPRITE_WIDTH - 10;
        public const int PIPE_TOP_POSITION_Y = 20 - SPRITE_HEIGHT;
        public const int PIPE_BOT_POSITION_X = PIPE_TOP_POSITION_X;
        public const int PIPE_BOT_POSITION_Y = PIPE_TOP_POSITION_Y + SPRITE_HEIGHT + PIPES_GAP;

        public readonly PhysicsObject PhysicsObjectPipeTop;
        public readonly PhysicsObject PhysicsObjectPipeBottom;
        private Texture2DRegion _pipeTopTexture;
        private Texture2DRegion _pipeBottomTexture;

        private float _speedForce;

        public Pipes()
        {
            _speedForce = 10;
            PhysicsObjectPipeTop = new PhysicsObject(PIPE_TOP_POSITION_X, PIPE_TOP_POSITION_Y, SPRITE_WIDTH, SPRITE_HEIGHT)
            {
                Gravity = new Vector2(0, 0),
                Friction = new Vector2(0, 0)
            };
            PhysicsObjectPipeBottom = new PhysicsObject(PIPE_BOT_POSITION_X, PIPE_BOT_POSITION_Y, SPRITE_WIDTH, SPRITE_HEIGHT)
            {
                Gravity = new Vector2(0, 0),
                Friction = new Vector2(0, 0)
            };
        }

        /// <summary>
        /// The Max height the bottom pipe can have is 40 pixels, or it will be flying out of the floor
        /// </summary>
        /// <param name="xOffsetFromRightBorder">x offset to add to the pipes default spawning x position, which is the right border of the screen</param>
        /// <param name="yOffsetFromTop">y offset to change the position of the pipes. Reduces the height of the pipes by world pixels</param>
        /// <param name="gapHeight">the gap between the top pipe and the bottom pipe. bottom pipe position is top.y+gap</param>
        public Pipes(float xOffsetFromRightBorder, float yOffsetFromTop, float gapHeight, float speed)
        {
            _speedForce = speed;
            float xPosition = GameMain.WORLD_WIDTH - SPRITE_WIDTH + xOffsetFromRightBorder;
            PhysicsObjectPipeTop = new PhysicsObject(xPosition, -SPRITE_HEIGHT + yOffsetFromTop, SPRITE_WIDTH, SPRITE_HEIGHT)
            {
                Gravity = new Vector2(0, 0),
                Friction = new Vector2(0, 0)
            };
            PhysicsObjectPipeBottom = new PhysicsObject(xPosition, yOffsetFromTop + gapHeight, SPRITE_WIDTH, SPRITE_HEIGHT)
            {
                Gravity = new Vector2(0, 0),
                Friction = new Vector2(0, 0)
            };
        }


        public void Load(ContentManager content, GraphicsDevice graphicsDevice)
        {
            //textures are loaded from the atlas in GameMain.cs
            //we still load the atlas here to get the texture
            _pipeTopTexture = GameAtlasTextures.Instance.PipeTop;
            _pipeBottomTexture = GameAtlasTextures.Instance.PipeBottom;
        }

        public void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            PhysicsObjectPipeTop.Velocity = new Vector2(-_speedForce, 0);
            PhysicsObjectPipeBottom.Velocity = new Vector2(-_speedForce, 0);

            //this allows to move pipes and ignore any possible collisions
            PhysicsEngine.Instance.MoveAndPushOthers(PhysicsObjectPipeTop, gameTime);
            PhysicsEngine.Instance.MoveAndPushOthers(PhysicsObjectPipeBottom, gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, ContentManager content, ViewportAdapter viewportAdapter, GraphicsDevice graphicsDevice)
        {
            spriteBatch.Draw(_pipeTopTexture, PhysicsObjectPipeTop.Position, Color.White);
            spriteBatch.Draw(_pipeBottomTexture, PhysicsObjectPipeBottom.Position, Color.White);

            //PhysicsObjectPipeTop.Collider.DebugDraw(spriteBatch);
            //PhysicsObjectPipeBottom.Collider.DebugDraw(spriteBatch);
        }
    }
}
