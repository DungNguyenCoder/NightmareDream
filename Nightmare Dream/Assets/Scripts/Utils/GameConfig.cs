using UnityEngine;

public class GameConfig
{
    //Panel
    public static string PANEL_INSTRUCTION = "InstuctionPanel";
    public static string PANEL_MODE = "ModePanel";
    public static string PANEL_SETTING = "SettingPanel";
    public static string PANEL_PAUSE = "PausePanel";
    public static string PANEL_WIN = "WinPanel";

    //Music - SFX
    public static string MUSIC_VOLUME_KEY = "MusicVolume";
    public static string SFX_VOLUME_KEY = "SFXVolume";

    //Gamemode
    public static string GAMEMODE_KEY = "Gamemode";

    //Tag
    public static string TAG_PLAYER = "Player";
    public static string TAG_ENEMY = "Enemy";
    public static string TAG_OBSTACLE = "Obstacle";

    //Position
    public static Vector3 POSITION_DEAD_ROOM = new Vector3(60f, 0f, -10f);
    public static Vector3 POSITION_WIN_ROOM = new Vector3(60f, 12f, -10f);

    //Animator Condition
    public static string ANIM_COL_JUMP = "Jump";
    public static string ANIM_COL_DOUBLE_JUMP = "DoubleJump";
    public static string ANIM_COL_DASHING = "IsDashing";
    public static string ANIM_COL_RUN = "Run";
    public static string ANIM_COL_YVERLOCITY = "YVelocity";
    public static string ANIM_COL_IS_GROUNDED = "IsGrounded";
    public static string ANIM_COL_IS_DEATH = "IsDeath";

    //Scene
    public static string SCENE_MAIN_MENU = "MainMenu";
    public static string SCENE_PLAY = "GameScene";
    public static string SCENE_STORY = "StoryScene";

    //URL
    public static string URL = "https://github.com/DungNguyenCoder/NightmareDream";
}