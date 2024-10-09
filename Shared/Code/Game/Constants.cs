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
    public static readonly Color DEFAULT_DEBUG_COLOR_GIZMOS = Color.Yellow;

    // Flappy bird title sprite
    public static readonly Vector2 ATLAS_POSITION_LOGO_FLAPPYBIRD = new(351, 91);
    public static readonly Vector2 ATLAS_SIZE_LOGO_FLAPPYBIRD = new(89, 24);
    public static readonly Vector2 SPRITE_POSITION_SOUNDUI_LOGO_FLAPPYBIRD = new(28, 30);
    public static readonly Vector2 SPRITE_POSITION_MAIN_SCREEN_LOGO_FLAPPYBIRD = new(28, 51);

    //floor sprite
    public static readonly Vector2 ATLAS_POSITION_FLOOR = new(292, 0);
    public static readonly Vector2 ATLAS_SIZE_FLOOR = new(168, 56);
    public static readonly float PLAYABLE_WORLD_HEIGHT = WORLD_HEIGHT - ATLAS_SIZE_FLOOR.Y;
    public static readonly Vector2 SPRITE_POSITION_FLOOR = new(0, 200);

    //Pipes sprite
    public static readonly Vector2 ATLAS_POSITION_PIPE_TOP = new(56, 323);
    public static readonly Vector2 ATLAS_POSITION_PIPE_BOTTOM = new(84, 323);
    public static readonly Vector2 ATLAS_SIZE_PIPE = new(26, 160);

    // GetReady title sprite
    public static readonly Vector2 ATLAS_POSITION_GETREADY_TITLE = new(295, 59);
    public static readonly Vector2 ATLAS_SIZE_GETREADY_TITLE = new(92, 25);
    public static readonly Vector2 SPRITE_POSITION_GETREADY_TITLE = new(26, 55);

    //TAPScreen title sprite
    public static readonly Vector2 ATLAS_POSITION_TAPSCREEN_TITLE = new(292, 91);
    public static readonly Vector2 ATLAS_SIZE_TAPSCREEN_TITLE = new(57, 49);
    public static readonly Vector2 SPRITE_POSITION_TAPSCREEN_TITLE = new(44, 84);

    //Game over sprite
    public static readonly Vector2 ATLAS_POSITION_GAMEOVER = new(395, 59);
    public static readonly Vector2 ATLAS_SIZE_GAMEOVER = new(96, 21);
    public static readonly Vector2 SPRITE_POSITION_GAMEOVER = new(24, 45);

    // Jump click region
    public static readonly Vector2 CLICK_REGION_POSITION_JUMP_REGION = new(0, 25);
    public static readonly Vector2 CLICK_REGION_SIZE_JUMP_REGION = new(WORLD_WIDTH, WORLD_HEIGHT-25);

    //ScoreUI
    public static readonly Vector2 ATLAS_POSITION_SCORE_UI = new(3, 259);
    public static readonly Vector2 ATLAS_SIZE_SCORE_UI = new(113, 57);
    public static readonly Vector2 SPRITE_POSITION_SCORE_UI = new(16, 99);

    public static readonly Vector2 TEXT_POSITION_SCORE_CURRENT = new(91, 114);
    public static readonly Vector2 TEXT_POSITION_SCORE_BEST = new(91, 135);

    //Score badge
    public static readonly Vector2 ATLAS_POSITION_SCORE_BADGE_FIRST = new(121, 282);
    public static readonly Vector2 ATLAS_POSITION_SCORE_BADGE_SECOND = new(112, 453);
    public static readonly Vector2 ATLAS_POSITION_SCORE_BADGE_THIRD = new(112, 477);
    public static readonly Vector2 ATLAS_SIZE_SCORE_BADGE = new(22, 22);
    public static readonly Vector2 SPRITE_POSITION_SCORE_BADGE = new(29, 120);

    //NEW badge
    public static readonly Vector2 ATLAS_POSITION_NEW_BADGE = new(112, 501);
    public static readonly Vector2 ATLAS_POSITION_NEW_BADGE_SIZE = new(16, 7);
    public static readonly Vector2 SPRITE_POSITION_NEW_BADGE = new(53, 119);

    //Sprite dimensions, position
    // - Settings UI
    public static readonly Vector2 ATLAS_PAUSE_BUTTON = new(121, 306);
    public static readonly Vector2 ATLAS_SIZE_PAUSE_BUTTON = new(13,14);
    public static readonly Vector2 SPRITE_POSITION_PAUSE_BUTTON = new(121, 10);
    public static readonly Vector2 CLICK_REGION_POSITION_PAUSE_BUTTON = new(111, 0);
    public static readonly Vector2 CLICK_REGION_SIZE_PAUSE_BUTTON = new(33, 33);

    //OK button
    public static readonly Vector2 ATLAS_OK_BUTTON = new(462, 42);
    public static readonly Vector2 ATLAS_SIZE_OK_BUTTON = new(40, 14);
    public static readonly Vector2 SPRITE_POSITION_OK_BUTTON = new(16,179);
    public static readonly Vector2 CLICK_REGION_POSITION_OK_BUTTON = new(6,163);
    public static readonly Vector2 CLICK_REGION_SIZE_OK_BUTTON = new(59, 43);
    
    //Menu button
    public static readonly Vector2 ATLAS_MENU_BUTTON = new(462, 26);
    public static readonly Vector2 ATLAS_SIZE_MENU_BUTTON = new(40, 14);
    public static readonly Vector2 SPRITE_POSITION_MENU_BUTTON_SOUND_UI = new(89, 179);
    public static readonly Vector2 SPRITE_POSITION_MENU_BUTTON_GAMEOVER = new(52, 149);
    public static readonly Vector2 CLICK_REGION_SOUND_UI_POSITION_MENU_BUTTON = new(78, 163);
    public static readonly Vector2 CLICK_REGION_SOUND_UI_SIZE_MENU_BUTTON = new(59, 43);
    public static readonly Vector2 CLICK_REGION_POSITION_GAMEOVER_MENU_BUTTON = new(44, 139);
    public static readonly Vector2 CLICK_REGION_SIZE_GAMEOVER_MENU_BUTTON = new(55, 33);

    // Play button sprite
    public static readonly Vector2 ATLAS_POSITION_PLAY_BUTTON = new(354, 118);
    public static readonly Vector2 ATLAS_SIZE_PLAY_BUTTON = new(52, 29); //same size for click region
    public static readonly Vector2 SPRITE_POSITION_PLAY_BUTTON_GAMEOVER = new(46, 177); //same position for click region
    public static readonly Vector2 SPRITE_POSITION_PLAY_BUTTON_MENU = new(42, 123); //same position for click region

    // Score button sprite
    public static readonly Vector2 ATLAS_POSITION_SCORE_BUTTON = new(414, 118);
    public static readonly Vector2 ATLAS_SIZE_SCORE_BUTTON = new(52, 29); //same size for click region
    public static readonly Vector2 SPRITE_POSITION_SCORE_BUTTON_GAMEOVER = new(76, 126); //same position for click region
    public static readonly Vector2 SPRITE_POSITION_SCORE_BUTTON_MENU = new(76, 123); //same position for click region

    // - SoundUI
    public static readonly Vector2 ATLAS_UI_SETTINGS = new(203, 264);
    public static readonly Vector2 ATLAS_SIZE_UI_SETTINGS = new(113, 57);
    public static readonly Vector2 SPRITE_POSITION_UI_SETTINGS = new(16, 100);

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

    //the bar to say the sound is ON
    public static readonly Vector2 ATLAS_BAR_SOUND = new(214, 324);
    public static readonly Vector2 ATLAS_SIZE_BAR_SOUND = new(3, 7);
    public const int SETTINGS_UI_SPACE_BETWEEN_BARS = 2;
    //position is computed in the SoundUI


    // DEBUG

    // Debug world dimensions
    public const int DEBUG_WORLD_WIDTH = 500;
    public const int DEBUG_WORLD_HEIGHT = 500;
    //Debug screen dimensions
    public const int DEBUG_SCREEN_WIDTH = 1000;
    public const int DEBUG_SCREEN_HEIGHT = 1000;
}