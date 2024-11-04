using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Gum.Wireframe;
using GumFormsSample;

public class GameOverScreen : GameScreen
{
    private BackgroundGumHandler _backgroundHandler;
    private GraphicalUiElement _gumScreen;

    private ScoreManager.Score _score;

    public GameOverScreen(Game game) : base(game) { }

    public override void Initialize()
    {
        _gumScreen = MainRegistry.I.ChangeScreen("GameOverScreen");
        _backgroundHandler = new BackgroundGumHandler(Game.Window, _gumScreen);
        _backgroundHandler.InitAndResizeOnce();

        GumTransparentButton.AttachButton("MenuButton", _gumScreen,  OnClickMenu);
        GumTransparentButton.AttachButton("PlayButton", _gumScreen, OnClickPlay);

        _score = ScoreManager.Instance.SaveScores();
        switch(_score.Rank)
        {
            case ScoreManager.ScoreRank.First:
                _gumScreen.SetProperty("GoldenMedal.Visible", true);
                break;
            case ScoreManager.ScoreRank.Second:
                _gumScreen.SetProperty("SilverMedal.Visible", true);
                break;
            case ScoreManager.ScoreRank.Third:
                _gumScreen.SetProperty("BronzeMedal.Visible", true);
                break;
        }
        if (_score.IsNewScore)
        {
            _gumScreen.SetProperty("New.Visible", true);
        }
        _gumScreen.SetProperty("CurrentScoreText.Text", _score.Value.ToString());
        _gumScreen.SetProperty("BestScoreText.Text", _score.Best.ToString());
    }
    public override void Dispose()
    {
        _backgroundHandler.Dispose();
        base.Dispose();
    }

    public override void Update(GameTime gameTime) 
    {
        _backgroundHandler.AnimateParallax(gameTime);
    }
    public override void Draw(GameTime gameTime){}

    private void OnClickPlay()
    {
        MainRegistry.I.SceneRegistry.LoadScene(SceneName.MainGameScreen);
    }

    private void OnClickMenu()
    {
        MainRegistry.I.SceneRegistry.LoadScene(SceneName.MenuScreen);
    }
}