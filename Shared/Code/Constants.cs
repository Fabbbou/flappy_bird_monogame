using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;

public class Constants
{
    // Ingame world dimensions
    public const int WORLD_WIDTH = 144;
    public const int WORLD_HEIGHT = 256;
    public const float WORLD_MIDDLE_SCREEN_WIDTH = WORLD_WIDTH * .5f;
    public const float WORLD_MIDDLE_SCREEN_HEIGHT = WORLD_HEIGHT * .5f;

    // Debug world dimensions
    public const int DEBUG_WORLD_WIDTH = 500;
    public const int DEBUG_WORLD_HEIGHT = 500;
    //Debug screen dimensions
    public const int DEBUG_SCREEN_WIDTH = 1000;
    public const int DEBUG_SCREEN_HEIGHT = 1000;

    public const float PLAYABLE_WORLD_HEIGHT = WORLD_HEIGHT - Floor.SPRITE_HEIGHT;
    public static readonly Color DEFAULT_DEBUG_COLOR_GIZMOS = Color.Yellow;

    //Sprite dimensions, position
    // - Pause screen UI
    public static readonly Vector2 SIZE_PAUSE_BUTTON = new(13, 14);
    public static readonly Vector2 POSITION_PAUSE_BUTTON = new(WORLD_WIDTH - SIZE_PAUSE_BUTTON.Y - 5, 5);

    public const int SPRITE_OK_ATLAS_X = 462;
    public const int SPRITE_OK_ATLAS_Y = 42;
    public static readonly Vector2 SIZE_OK_BUTTON = new(40, 14);
    public static readonly Vector2 POSITION_OK_BUTTON = new(WORLD_WIDTH*0.5f - SIZE_OK_BUTTON.X * 0.5f, WORLD_HEIGHT - 100);

    public const int SPRITE_MINUS_BUTTON_WIDTH = 5;
    public const int SPRITE_MINUS_BUTTON_HEIGHT = 3;
    public const int SPRITE_MINUS_ATLAS_X = 502;
    public const int SPRITE_MINUS_ATLAS_Y = 85;

    public const int SPRITE_PLUS_BUTTON_WIDTH = 5;
    public const int SPRITE_PLUS_BUTTON_HEIGHT = 5;
    public const int SPRITE_PLUS_ATLAS_X = 496;
    public const int SPRITE_PLUS_ATLAS_Y = 84;

    public const int SPRITE_BAR_SOUND_ATLAS_X = 488;
    public const int SPRITE_BAR_SOUND_ATLAS_Y = 107;
    public static readonly Vector2 SIZE_BAR_SOUND = new(3, 6);

    public const int SPRITE_BAR_EMPTY_SOUND_ATLAS_X = 493;
    public const int SPRITE_BAR_EMPTY_SOUND_ATLAS_Y = 107;
    public static readonly Vector2 SIZE_BAR_EMPTY_SOUND = new(3, 6);

    public const int SPRITE_LOGO_FX_ATLAS_X = 165;
    public const int SPRITE_LOGO_FX_ATLAS_Y = 320;
    public static readonly Vector2 SIZE_LOGO_FX = new(9, 10);

    public const int SPRITE_LOGO_MUSIC_ATLAS_X = 165;
    public const int SPRITE_LOGO_MUSIC_ATLAS_Y = 308;
    public static readonly Vector2 SIZE_LOGO_MUSIC = new(9, 10);




}