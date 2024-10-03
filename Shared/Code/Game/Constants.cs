using flappyrogue_mg.GameSpace;
using Microsoft.Xna.Framework;

public class Constants
{
    // 144 and 256 are width and height of the background image.
    // As they  are uniform, the altlas automatically find each sprite contained in the texture
    // i.e. the background image is divided in 144x256 sprites
    // it is considered a uniform grid of sprites (they are all the same size)
    // more info here: https://www.monogameextended.net/docs/features/texture-handling/texture2datlas/

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

    public static readonly Vector2 POSITION_JUMP_REGION = new(0, 25);
    public static readonly Vector2 SIZE_JUMP_REGION = new(WORLD_WIDTH, WORLD_HEIGHT-25);

    //Sprite dimensions, position
    // - Settings UI
    public static readonly Vector2 ATLAS_PAUSE_BUTTON = new(121, 306);
    public static readonly Vector2 ATLAS_SIZE_PAUSE_BUTTON = new(13,14);
    public static readonly Vector2 SPRITE_POSITION_PAUSE_BUTTON = new(121, 10);
    public static readonly Vector2 CLICK_REGION_POSITION_PAUSE_BUTTON = new(111, 0);
    public static readonly Vector2 CLICK_REGION_SIZE_PAUSE_BUTTON = new(33, 33);

    public static readonly Vector2 ATLAS_OK_BUTTON = new(462, 42);
    public static readonly Vector2 ATLAS_SIZE_OK_BUTTON = new(40, 14);
    public static readonly Vector2 SPRITE_POSITION_OK_BUTTON = new(16,179);
    public static readonly Vector2 CLICK_REGION_POSITION_OK_BUTTON = new(6,163);
    public static readonly Vector2 CLICK_REGION_SIZE_OK_BUTTON = new(59, 43);
    
    public static readonly Vector2 ATLAS_MENU_BUTTON = new(462, 26);
    public static readonly Vector2 ATLAS_SIZE_MENU_BUTTON = new(40, 14);
    public static readonly Vector2 SPRITE_POSITION_MENU_BUTTON = new(89, 179);
    public static readonly Vector2 CLICK_REGION_POSITION_MENU_BUTTON = new(78, 163);
    public static readonly Vector2 CLICK_REGION_SIZE_MENU_BUTTON = new(59, 43);

    //the bar to say the sound is ON
    public static readonly Vector2 ATLAS_BAR_SOUND = new(214, 324);
    public static readonly Vector2 ATLAS_SIZE_BAR_SOUND = new(3, 7);
    public const int SETTINGS_UI_SPACE_BETWEEN_BARS = 2;
    //position is computed in the SoundUI

    public static readonly Vector2 ATLAS_UI_SETTINGS = new(203, 264);
    public static readonly Vector2 SIZE_UI_SETTINGS = new(113, 57);
    public static readonly Vector2 SPRITE_POSITION_UI_SETTINGS = new(16, 100);

    // - Settings UI Buttons size
    public static readonly Vector2 CLICK_REGION_SIZE_MINUS_BUTTON = new(30, 22);
    public static readonly Vector2 CLICK_REGION_SIZE_PLUS_BUTTON = new(31, 22);

    public static readonly Vector2 CLICK_REGION_POSITION_MINUS_BUTTON_MUSIC = new(38, 109);
    public static readonly Vector2 CLICK_REGION_POSITION_MINUS_BUTTON_FX = new(38, 134);

    public static readonly Vector2 CLICK_REGION_POSITION_PLUS_BUTTON_MUSIC = new(98, 110);
    public static readonly Vector2 CLICK_REGION_POSITION_PLUS_BUTTON_FX = new(98, 135);

    public static readonly Vector2 CLICK_REGION_SIZE_LOGO_MUSIC = new(21, 23);
    public static readonly Vector2 CLICK_REGION_POSITION_LOGO_MUSIC = new(17, 112);
    public static readonly Vector2 POSITION_BARS_MUSIC = new(59, 121);

    public static readonly Vector2 CLICK_REGION_SIZE_LOGO_FX = new(21, 23);
    public static readonly Vector2 CLICK_REGION_POSITION_LOGO_FX = new(17, 135);
    public static readonly Vector2 POSITION_BARS_FX = new(59, 142);


}