using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

namespace flappyrogue_mg.Game
{
    public class GameAtlasTextures
    {
        private static GameAtlasTextures _instance;
        public static GameAtlasTextures Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameAtlasTextures();
                }
                return _instance;
            }
        }

        private Texture2DAtlas _atlas;
        public Texture2DRegion PipeTop { get; private set; }
        public Texture2DRegion PipeBottom { get; private set; }
        public void Load(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Texture2D atlasTexture = content.Load<Texture2D>("sprites/atlas");
            _atlas = new Texture2DAtlas("Atlas", atlasTexture);
            PipeTop = _atlas.CreateRegion(56, 323, Pipes.SPRITE_WIDTH, Pipes.SPRITE_HEIGHT, "PipeTop");
            PipeBottom = _atlas.CreateRegion(84, 323, Pipes.SPRITE_WIDTH, Pipes.SPRITE_HEIGHT, "PipeBottom");
        }
    }
}
