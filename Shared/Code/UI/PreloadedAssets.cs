using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Graphics;
using static Constants;

public class PreloadedAssets
{
    public bool IsLoaded { get; private set; }
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
    public Texture2DRegion PauseButton { get; private set; }
    public Texture2DRegion OkButton { get; private set; }
    public Texture2DRegion MenuButton { get; private set; }
    public Texture2DRegion BarSound { get; private set; }
    public Texture2DRegion UiSSettings { get; private set; }

    public BitmapFont mainFont { get; private set; }
    private PreloadedAssets() { }

    public void LoadContent(ContentManager content)
    {
        mainFont = content.Load<BitmapFont>("fonts/04b19");
        LoadAtlas(content);
        IsLoaded = true;
    }

    private void LoadAtlas(ContentManager content)
    {
        Texture2D atlasTexture = content.Load<Texture2D>("sprites/atlas");
        _mainGameAtlas = new Texture2DAtlas("Atlas", atlasTexture);
        PipeTop = _mainGameAtlas.CreateRegion(56, 323, Pipes.SPRITE_WIDTH, Pipes.SPRITE_HEIGHT, "PipeTop");
        PipeBottom = _mainGameAtlas.CreateRegion(84, 323, Pipes.SPRITE_WIDTH, Pipes.SPRITE_HEIGHT, "PipeBottom");
        PauseButton = _mainGameAtlas.CreateRegion((int)ATLAS_PAUSE_BUTTON.X, (int)ATLAS_PAUSE_BUTTON.Y, (int)ATLAS_SIZE_PAUSE_BUTTON.X, (int)ATLAS_SIZE_PAUSE_BUTTON.Y, "PauseButton");
        OkButton = _mainGameAtlas.CreateRegion((int)ATLAS_OK_BUTTON.X, (int)ATLAS_OK_BUTTON.Y, (int)ATLAS_SIZE_OK_BUTTON.X, (int)ATLAS_SIZE_OK_BUTTON.Y, "OkButton");
        MenuButton = _mainGameAtlas.CreateRegion((int)ATLAS_MENU_BUTTON.X, (int)ATLAS_MENU_BUTTON.Y, (int)ATLAS_SIZE_MENU_BUTTON.X, (int)ATLAS_SIZE_MENU_BUTTON.Y, "MenuButton");
        BarSound = _mainGameAtlas.CreateRegion((int)ATLAS_BAR_SOUND.X, (int)ATLAS_BAR_SOUND.Y, (int)ATLAS_SIZE_BAR_SOUND.X, (int)ATLAS_SIZE_BAR_SOUND.Y, "BarSound");
        UiSSettings = _mainGameAtlas.CreateRegion((int)ATLAS_UI_SETTINGS.X, (int)ATLAS_UI_SETTINGS.Y, (int)SIZE_UI_SETTINGS.X, (int)SIZE_UI_SETTINGS.Y, "UiSSettings");
    }
}