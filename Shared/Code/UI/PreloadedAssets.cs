using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Graphics;
using static Constants;

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
    public Texture2DRegion PauseButton { get; private set; }
    public Texture2DRegion MinusButton { get; private set; }
    public Texture2DRegion PlusButton { get; private set; }
    public Texture2DRegion BarSound { get; private set; }
    public Texture2DRegion BarSoundEmpty { get; private set; }
    public Texture2DRegion LogoMusic { get; private set; }
    public Texture2DRegion LogoFx { get; private set; }
    public Texture2DRegion OkButton { get; private set; }


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
        PauseButton = _mainGameAtlas.CreateRegion(121, 306, (int)SIZE_PAUSE_BUTTON.X, (int)SIZE_PAUSE_BUTTON.Y, "PauseButton");
        MinusButton = _mainGameAtlas.CreateRegion(SPRITE_MINUS_ATLAS_X, SPRITE_MINUS_ATLAS_X,
            SPRITE_MINUS_BUTTON_WIDTH, SPRITE_MINUS_BUTTON_HEIGHT, "MinusButton");
        PlusButton = _mainGameAtlas.CreateRegion(SPRITE_PLUS_ATLAS_X, SPRITE_PLUS_ATLAS_X,
            SPRITE_PLUS_BUTTON_WIDTH, SPRITE_PLUS_BUTTON_HEIGHT, "PlusButton");
        BarSound = _mainGameAtlas.CreateRegion(SPRITE_BAR_SOUND_ATLAS_X, SPRITE_BAR_SOUND_ATLAS_Y,
            (int)SIZE_BAR_SOUND.X, (int)SIZE_BAR_SOUND.Y, "BarSound");
        BarSoundEmpty = _mainGameAtlas.CreateRegion(SPRITE_BAR_SOUND_ATLAS_X, SPRITE_BAR_SOUND_ATLAS_Y,
            (int)SIZE_BAR_EMPTY_SOUND.X, (int)SIZE_BAR_EMPTY_SOUND.Y, "BarSoundEmpty");
        LogoMusic = _mainGameAtlas.CreateRegion(SPRITE_LOGO_MUSIC_ATLAS_X, SPRITE_LOGO_MUSIC_ATLAS_Y,
            (int)SIZE_LOGO_MUSIC.X, (int)SIZE_LOGO_MUSIC.Y, "LogoMusic");
        LogoFx = _mainGameAtlas.CreateRegion(SPRITE_LOGO_FX_ATLAS_X, SPRITE_LOGO_FX_ATLAS_Y,
            (int)SIZE_LOGO_FX.X, (int)SIZE_LOGO_FX.Y, "LogoFx");
        OkButton = _mainGameAtlas.CreateRegion(SPRITE_OK_ATLAS_X, SPRITE_OK_ATLAS_Y,
            (int)SIZE_OK_BUTTON.X, (int)SIZE_OK_BUTTON.Y, "OkButton");
    }
}