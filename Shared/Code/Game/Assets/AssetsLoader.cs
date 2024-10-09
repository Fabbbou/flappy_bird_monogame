using AsepriteDotNet.Aseprite;
using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Graphics;
using static Constants;
public class AssetsLoader
{
    //singleton
    public bool IsLoaded = false;
    private static AssetsLoader _instance;
    public static AssetsLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AssetsLoader();
            }
            return _instance;
        }
    }
    private AssetsLoader(){}

    public BitmapFont Font { get; private set; }
    // Bird aseprite animated
    public AsepriteFile BirdAsepriteFile { get; private set; }
    
    //atlas png
    private Texture2DAtlas _mainGameAtlas;
    public Texture2DRegion Background { get; private set; }
    public Texture2DRegion Floor { get; private set; }
    public Texture2DRegion GetReadyTitle { get; private set; }
    public Texture2DRegion TapScreenTitle { get; private set; }
    public Texture2DRegion GameOver { get; private set; }
    public Texture2DRegion PipeTop { get; private set; }
    public Texture2DRegion PipeBottom { get; private set; }
    public Texture2DRegion PauseButton { get; private set; }
    public Texture2DRegion OkButton { get; private set; }
    public Texture2DRegion MenuButton { get; private set; }
    public Texture2DRegion BarSound { get; private set; }
    public Texture2DRegion UiSSettings { get; private set; }
    public Texture2DRegion FlappyBirdLogo { get; private set; }
    public Texture2DRegion PlayButton { get; private set; }
    public Texture2DRegion ScoreButton { get; private set; }
    public Texture2DRegion UIScore { get; private set; }
    public Texture2DRegion ScoreBadgeFirst { get; private set; }
    public Texture2DRegion ScoreBadgeSecond { get; private set; }
    public Texture2DRegion ScoreBadgeThird { get; private set; }
    public Texture2DRegion BadgeNew { get; private set; }


    //Sounds
    public SoundEffectInstance DieSound { get; private set; }
    public SoundEffectInstance JumpSound { get; private set; }
    public SoundEffectInstance HitSound { get; private set; }
    public SoundEffectInstance ScoreSound { get; private set;  }


    public void LoadContent(ContentManager content)
    {
        //tag created in aseprite file selecting the frames to be animated
        BirdAsepriteFile = content.Load<AsepriteFile>("sprites/bird");
        
        //load fonts
        Font = content.Load<BitmapFont>("fonts/04b19");
                
        Texture2D atlasTexture = content.Load<Texture2D>("sprites/atlas");
        _mainGameAtlas = new Texture2DAtlas("Atlas", atlasTexture);
        Background = _mainGameAtlas.CreateRegion(0, 0, WORLD_WIDTH, WORLD_HEIGHT, "Background");
        Floor = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_FLOOR.X, (int)ATLAS_POSITION_FLOOR.Y, (int)ATLAS_SIZE_FLOOR.X, (int)ATLAS_SIZE_FLOOR.Y, "Floor");
        GameOver = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_GAMEOVER.X, (int)ATLAS_POSITION_GAMEOVER.Y, (int)ATLAS_SIZE_GAMEOVER.X, (int)ATLAS_SIZE_GAMEOVER.Y, "GameOver");
        PipeTop = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_PIPE_TOP.X, (int)ATLAS_POSITION_PIPE_TOP.Y, (int)ATLAS_SIZE_PIPE.X, (int)ATLAS_SIZE_PIPE.Y, "PipeTop");
        PipeBottom = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_PIPE_BOTTOM.X, (int)ATLAS_POSITION_PIPE_BOTTOM.Y, (int)ATLAS_SIZE_PIPE.X, (int)ATLAS_SIZE_PIPE.Y, "PipeBottom");
        PauseButton = _mainGameAtlas.CreateRegion((int)ATLAS_PAUSE_BUTTON.X, (int)ATLAS_PAUSE_BUTTON.Y, (int)ATLAS_SIZE_PAUSE_BUTTON.X, (int)ATLAS_SIZE_PAUSE_BUTTON.Y, "PauseButton");
        OkButton = _mainGameAtlas.CreateRegion((int)ATLAS_OK_BUTTON.X, (int)ATLAS_OK_BUTTON.Y, (int)ATLAS_SIZE_OK_BUTTON.X, (int)ATLAS_SIZE_OK_BUTTON.Y, "OkButton");
        MenuButton = _mainGameAtlas.CreateRegion((int)ATLAS_MENU_BUTTON.X, (int)ATLAS_MENU_BUTTON.Y, (int)ATLAS_SIZE_MENU_BUTTON.X, (int)ATLAS_SIZE_MENU_BUTTON.Y, "MenuButton");
        BarSound = _mainGameAtlas.CreateRegion((int)ATLAS_BAR_SOUND.X, (int)ATLAS_BAR_SOUND.Y, (int)ATLAS_SIZE_BAR_SOUND.X, (int)ATLAS_SIZE_BAR_SOUND.Y, "BarSound");
        UiSSettings = _mainGameAtlas.CreateRegion((int)ATLAS_UI_SETTINGS.X, (int)ATLAS_UI_SETTINGS.Y, (int)ATLAS_SIZE_UI_SETTINGS.X, (int)ATLAS_SIZE_UI_SETTINGS.Y, "UiSSettings");
        FlappyBirdLogo = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_LOGO_FLAPPYBIRD.X, (int)ATLAS_POSITION_LOGO_FLAPPYBIRD.Y, (int)ATLAS_SIZE_LOGO_FLAPPYBIRD.X, (int)ATLAS_SIZE_LOGO_FLAPPYBIRD.Y, "FlappyBirdLogo");
        PlayButton = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_PLAY_BUTTON.X, (int)ATLAS_POSITION_PLAY_BUTTON.Y, (int)ATLAS_SIZE_PLAY_BUTTON.X, (int)ATLAS_SIZE_PLAY_BUTTON.Y, "PlayButton");
        ScoreButton = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_SCORE_BUTTON.X, (int)ATLAS_POSITION_SCORE_BUTTON.Y, (int)ATLAS_SIZE_SCORE_BUTTON.X, (int)ATLAS_SIZE_SCORE_BUTTON.Y, "ScoreButton");
        GetReadyTitle = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_GETREADY_TITLE.X, (int)ATLAS_POSITION_GETREADY_TITLE.Y, (int)ATLAS_SIZE_GETREADY_TITLE.X, (int)ATLAS_SIZE_GETREADY_TITLE.Y, "GetReadyTitle");
        TapScreenTitle = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_TAPSCREEN_TITLE.X, (int)ATLAS_POSITION_TAPSCREEN_TITLE.Y, (int)ATLAS_SIZE_TAPSCREEN_TITLE.X, (int)ATLAS_SIZE_TAPSCREEN_TITLE.Y, "TapScreenTitle");
        UIScore = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_SCORE_UI.X, (int)ATLAS_POSITION_SCORE_UI.Y, (int)ATLAS_SIZE_SCORE_UI.X, (int)ATLAS_SIZE_SCORE_UI.Y, "UIScore");
        ScoreBadgeFirst = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_SCORE_BADGE_FIRST.X, (int)ATLAS_POSITION_SCORE_BADGE_FIRST.Y, (int)ATLAS_SIZE_SCORE_BADGE.X, (int)ATLAS_SIZE_SCORE_BADGE.Y, "ScoreBadgeFirst");
        ScoreBadgeSecond = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_SCORE_BADGE_SECOND.X, (int)ATLAS_POSITION_SCORE_BADGE_SECOND.Y, (int)ATLAS_SIZE_SCORE_BADGE.X, (int)ATLAS_SIZE_SCORE_BADGE.Y, "ScoreBadgeSecond");
        ScoreBadgeThird = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_SCORE_BADGE_THIRD.X, (int)ATLAS_POSITION_SCORE_BADGE_THIRD.Y, (int)ATLAS_SIZE_SCORE_BADGE.X, (int)ATLAS_SIZE_SCORE_BADGE.Y, "ScoreBadgeThird");
        BadgeNew = _mainGameAtlas.CreateRegion((int)ATLAS_POSITION_NEW_BADGE.X, (int)ATLAS_POSITION_NEW_BADGE.Y, (int)ATLAS_POSITION_NEW_BADGE_SIZE.X, (int)ATLAS_POSITION_NEW_BADGE_SIZE.Y, "BadgeNew");

        //load sounds
        JumpSound = content.Load<SoundEffect>("sounds/sfx_wing").CreateInstance();
        ScoreSound = content.Load<SoundEffect>("sounds/sfx_point").CreateInstance();
        HitSound = content.Load<SoundEffect>("sounds/sfx_hit").CreateInstance();
        DieSound = content.Load<SoundEffect>("sounds/sfx_die").CreateInstance();

        IsLoaded = true;
    }

    /// <summary>
    /// Load the bird sprite sheet and create the animated sprite
    /// to be called during the loadcontent
    /// </summary>
    /// <param name="graphicsDevice"></param>
    /// <returns></returns>
    public MonoGame.Aseprite.AnimatedSprite CreateBirdSprite(GraphicsDevice graphicsDevice)
    {
        return BirdAsepriteFile.CreateSpriteSheet(graphicsDevice).CreateAnimatedSprite("idle");
    }
}