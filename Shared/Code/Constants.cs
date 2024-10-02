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

    public static readonly Vector2 ATLAS_LOGO_FB = new(351, 91);
    public static readonly Vector2 SIZE_LOGO_FB = new(89, 24);

    //Sprite dimensions, position
    // - Settings UI
    public static readonly Vector2 ATLAS_PAUSE_BUTTON = new(121, 306);
    public static readonly Vector2 SIZE_PAUSE_BUTTON = new(13, 14);
    public static readonly Vector2 POSITION_PAUSE_BUTTON = new(WORLD_WIDTH - SIZE_PAUSE_BUTTON.Y - 5, 5);

    public static readonly Vector2 ATLAS_OK_BUTTON = new(462, 42);
    public static readonly Vector2 SIZE_OK_BUTTON = new(40, 14);
    public static readonly Vector2 POSITION_OK_BUTTON = new(24,164);
    
    public static readonly Vector2 ATLAS_MENU_BUTTON = new(462, 26);
    public static readonly Vector2 SIZE_MENU_BUTTON = new(40, 14);
    public static readonly Vector2 POSITION_MENU_BUTTON = new(81,164);

    //the bar to say the sound is ON
    public static readonly Vector2 SIZE_BAR_SOUND = new(3, 6);
    public static readonly Vector2 ATLAS_BAR_SOUND = new(214, 325);
    public const int SETTINGS_UI_SPACE_BETWEEN_BARS = 2;
    //position is computed in the SoundUI

    public static readonly Vector2 ATLAS_UI_SETTINGS = new(203, 264);
    public static readonly Vector2 SIZE_UI_SETTINGS = new(113, 57);
    public static readonly Vector2 POSITION_UI_SETTINGS = new(16, 100);

    // - Settings UI Buttons size
    public static readonly Vector2 SIZE_MINUS_BUTTON = new(5, 3);
    public static readonly Vector2 POSITION_MINUS_BUTTON_FX = new(45, 141);
    public static readonly Vector2 POSITION_PLUS_BUTTON_FX = new(106, 140);

    public static readonly Vector2 SIZE_PLUS_BUTTON = new(5, 5);
    public static readonly Vector2 POSITION_MINUS_BUTTON_MUSIC = new(45, 124);
    public static readonly Vector2 POSITION_PLUS_BUTTON_MUSIC = new(106, 123);


    public static readonly Vector2 SIZE_LOGO_FX = new(9, 10);
    public static readonly Vector2 POSITION_LOGO_FX = new(29, 137);
    public static readonly Vector2 POSITION_FX_BARS = new(54, 139);

    public static readonly Vector2 SIZE_LOGO_MUSIC = new(9, 10);
    public static readonly Vector2 POSITION_LOGO_MUSIC = new(29, 120);
    public static readonly Vector2 POSITION_MUSIC_BARS = new(54, 122);








}