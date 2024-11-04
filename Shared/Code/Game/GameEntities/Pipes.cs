using flappyrogue_mg.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using static Constants;
using Gum.Wireframe;
using RenderingLibrary;

namespace flappyrogue_mg.GameSpace
{
    public class Pipes : GameEntity
    {

        //gum stuff
        public static float GlobalPipesSpeed = DEFAULT_SPEED;
        //Both const are done using the gum editor to choose a approx centered position for the pipes
        //Both const are done to have the pipe edges visible on the screen when the DEFAULT_GAP_HEIGHT is used.
        //Warn: if using a custom gap height, the pipes may be too small and so th player may cheat.
        public static readonly Vector2 DEFAULT_SPAWN_POSITION = new(199, -81);

        
        private const float DEFAULT_SPEED = 60f;
        private float _overrideSpeed;
        private const float DEFAULT_GAP_HEIGHT = 60f;
        private const int PIPE_WIDTH = 26;
        private const int PIPE_HEIGHT = 160;
        private const int CROP_SCORING_ZONE_WIDTH = 3;
        private const int SCORING_ZONE_WIDTH = PIPE_WIDTH - CROP_SCORING_ZONE_WIDTH * 2;
        private readonly float _gapBetweenPipes;
        private readonly GraphicalUiElement _screen;
        private readonly GraphicalUiElement _rootIngameWorld;
        private readonly GraphicalUiElement _pipeTopContainer;
        private readonly GraphicalUiElement _pipeBottomContainer;
        private readonly GraphicalUiElement _pipeTop;
        private readonly GraphicalUiElement _pipeBottom;
        private readonly Vector2 _spawnPosition;
        private readonly int _instanceNumber;
        private readonly Entity _entityScoringZone;
        private readonly Camera _camera;
        //end gum

        public PhysicsObject PhysicsObjectPipeTop;
        public PhysicsObject PhysicsObjectPipeBottom;
        public PhysicsObject ScoringZoneTriggerOnceCollider;


        public int Right => (int)PhysicsObjectPipeTop.Position.X + (int)ATLAS_SIZE_PIPE.X;

        /// <summary>
        /// Create a pair of pipes (top and bottom) with a gap between them.
        /// The pipes speed is set using the GlobalPipesSpeed static field. It is better to use this field to set the speed of all pipes and keep the player experience uniform.
        ///
        /// You can use the overrideSpeed parameter to set a custom speed for this specific pipes instance.
        /// BUT: by doing this, this may look odd for the player, as the GlobalPipesSpeed is used by the floor animation to define the floor movement, 
        /// to be synced with the Pipes (so like the bird is going forward and not the pipes are moving to the bird).
        /// 
        /// 
        /// Warn - loophole known: if using a custom gapBetweenPipes, the pipes may be too small and so the player may cheat.
        /// This is aa loophole known, but we wont fix it yet as it's not a priority.
        /// </summary>
        /// <param name="spawnPosition">the position given is the top left corner of the top pipe. Everything is position from there accuratly</param>
        /// <param name="rootPipeSpawnContainer">A pipe container that should be 100pourcent of the scene root ingame world GraphicalUiElement (the BackgroundPic). It required to use a container to spawn the pipes in the right layer</param>
        /// <param name="gapBetweenPipes">the space between the two pipes spawned</param>
        /// <param name="overrideSpeed">the speed of the pipes moving from left to right</param>
        /// <param name="pipesInstanceNumber">the pipe instance number. Should be used when more than 1 pipe is spawned to label the physicsobject created</param>
        public Pipes(GraphicalUiElement screen, GraphicalUiElement rootPipeSpawnContainer, Vector2? spawnPosition = null, float gapBetweenPipes = DEFAULT_GAP_HEIGHT, float? overrideSpeed = null,  int pipesInstanceNumber = 0)
        {
            _camera = SystemManagers.Default.Renderer.Camera;
            _instanceNumber = pipesInstanceNumber;
            _gapBetweenPipes = gapBetweenPipes;
            GlobalPipesSpeed = overrideSpeed ?? GlobalPipesSpeed;
            _spawnPosition = spawnPosition ?? DEFAULT_SPAWN_POSITION;
            _entityScoringZone = new Entity();
            _screen = screen;

            _pipeTopContainer = rootPipeSpawnContainer.GetGraphicalUiElementByName("PipeTopContainer");
            _pipeBottomContainer = rootPipeSpawnContainer.GetGraphicalUiElementByName("PipeBottomContainer");
            _pipeTop = GumHelper.InstanciateComponent("Pipes\\PipeTop", _pipeTopContainer);
            _pipeBottom = GumHelper.InstanciateComponent("Pipes\\PipeBottom", _pipeBottomContainer);
        }

        public void onScoringZoneTriggered()
        {
            ScoreManager.Instance.IncreaseScore();
            Debug.WriteLine("Scored! Current score: " + ScoreManager.Instance.CurrentScore);
        }

        public override void LoadContent(ContentManager content)
        {
            Vector2 topleftPipeTop = new(_spawnPosition.X, _spawnPosition.Y);
            Vector2 topleftPipeBottom = new(_spawnPosition.X, _spawnPosition.Y + PIPE_HEIGHT + _gapBetweenPipes);
            Vector2 topleftScoringZone = new(_spawnPosition.X + CROP_SCORING_ZONE_WIDTH, _spawnPosition.Y + PIPE_HEIGHT);
            PhysicsObjectPipeTop = PhysicsObjectFactory.Rect(entity: this, label: $"{_instanceNumber}/pipe_top", x: topleftPipeTop.X, y: topleftPipeTop.Y, collisionType: ColliderType.Moving, width: PIPE_WIDTH, height: PIPE_HEIGHT, graphicalUiElement: _pipeTop, rootGraphicalUiElement: _pipeTopContainer, debugColor: Color.Blue);
            PhysicsObjectPipeTop.Gravity = Vector2.Zero;
            PhysicsObjectPipeBottom = PhysicsObjectFactory.Rect(entity: this, label: $"{_instanceNumber}/pipe_top", x: topleftPipeBottom.X, y: topleftPipeBottom.Y, collisionType: ColliderType.Moving, width: PIPE_WIDTH, height: PIPE_HEIGHT, graphicalUiElement: _pipeBottom, rootGraphicalUiElement: _pipeBottomContainer, debugColor: Color.DarkCyan);
            PhysicsObjectPipeBottom.Gravity = Vector2.Zero;
            ScoringZoneTriggerOnceCollider = PhysicsObjectFactory.AreaRectTriggerOnce(entity: _entityScoringZone, label: $"{_instanceNumber}/scoring_zone", x: topleftScoringZone.X, y: topleftScoringZone.Y, width: SCORING_ZONE_WIDTH, height: _gapBetweenPipes, onTrigger: onScoringZoneTriggered, rootGraphicalUiElement: _rootIngameWorld);
        }

        public override void Update(GameTime gameTime)
        {
            PhysicsObjectPipeTop.Velocity = new Vector2(-GlobalPipesSpeed, 0);
            PhysicsObjectPipeBottom.Velocity = new Vector2(-GlobalPipesSpeed, 0);
            ScoringZoneTriggerOnceCollider.Velocity = new Vector2(-GlobalPipesSpeed, 0);

            //this allows to move pipes and ignore any possible collisions
            //means that pipes dont collide to anything, but anything collides to them by using MoveAndCollide
            PhysicsObjectPipeTop.Update(gameTime);
            PhysicsObjectPipeBottom.Update(gameTime);
            ScoringZoneTriggerOnceCollider.Update(gameTime);
        }


        public void Kill(bool isEndOfGame = false)
        {
            PhysicsObjectPipeTop.Kill();
            PhysicsObjectPipeBottom.Kill();
            ScoringZoneTriggerOnceCollider.Kill();
            if (!isEndOfGame)
            {
                GumHelper.RemoveComponent(_pipeTopContainer, _pipeTop);
                GumHelper.RemoveComponent(_pipeBottomContainer, _pipeBottom);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
