using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Graphics;

public class PreloadedAssets
{
    //singleton
    private static PreloadedAssets _instance;
    public static PreloadedAssets Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PreloadedAssets();
            }
            return _instance;
        }
    }

    private Texture2DAtlas _mainGameAtlas;
    public Texture2DRegion PipeTop { get; private set; }
    public Texture2DRegion PipeBottom { get; private set; }
    public BitmapFont mainFont { get; private set; }

    private PreloadedAssets() { }

    public void LoadContent(ContentManager content)
    {
        mainFont = content.Load<BitmapFont>("fonts/04b19");
        LoadAtlas(content); 
    }

    private void LoadAtlas(ContentManager content)
    {
        Texture2D atlasTexture = content.Load<Texture2D>("sprites/atlas");
        _mainGameAtlas = new Texture2DAtlas("Atlas", atlasTexture);
        PipeTop = _mainGameAtlas.CreateRegion(56, 323, Pipes.SPRITE_WIDTH, Pipes.SPRITE_HEIGHT, "PipeTop");
        PipeBottom = _mainGameAtlas.CreateRegion(84, 323, Pipes.SPRITE_WIDTH, Pipes.SPRITE_HEIGHT, "PipeBottom");
    }
}