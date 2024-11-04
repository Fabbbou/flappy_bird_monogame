using flappyrogue_mg.Core;
using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System.Collections.Generic;

public class ScoreManager
{
    //singleton
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            _instance ??= new ScoreManager();
            return _instance;
        }
    }
    public int CurrentScore { get; private set; }

    private ScoreManager() {}

    private GraphicalUiElement _mainGameScreenGum;
    public void AttachScoreText(GraphicalUiElement mainGameScreenGum)
    {
        _mainGameScreenGum = mainGameScreenGum;
        _mainGameScreenGum?.SetProperty("ScoreText.Text", CurrentScore.ToString());
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        _mainGameScreenGum?.SetProperty("ScoreText.Text", CurrentScore.ToString());
    }

    public void IncreaseScore()
    {
        CurrentScore++;
        _mainGameScreenGum?.SetProperty("ScoreText.Text", CurrentScore.ToString());
        SoundManager.Instance.PlayScoreSound();
    }

    public Score SaveScores()
    {
        //store the score if the current score is greater than the 3 first high score
        List<int> scores = SettingsManager.Instance.UserSettings.Scores;
        ScoreRank rank = ScoreRank.Lower;
        int currentScore = CurrentScore;
        bool isNew = false;
        if (CurrentScore > scores[0])
        {
            scores[2] = scores[1];
            scores[1] = scores[0];
            scores[0] = CurrentScore;
            rank = ScoreRank.First;
            isNew = true;
        }
        else if (CurrentScore == scores[0])
        {
            rank = ScoreRank.First;
        }
        else if (CurrentScore > scores[1])
        {
            scores[2] = scores[1];
            scores[1] = CurrentScore;
            rank = ScoreRank.Second;
            isNew = true;
        }
        else if (CurrentScore == scores[1])
        {
            rank = ScoreRank.Second;
        }
        else if (CurrentScore > scores[2])
        {
            scores[2] = CurrentScore;
            rank = ScoreRank.Third;
            isNew = true;
        }
        else if (CurrentScore == scores[2])
        {
            rank = ScoreRank.Third;
        }
        SettingsManager.Instance.SaveSettings();
        CurrentScore = 0;
        return new(rank, isNew, currentScore, SettingsManager.Instance.UserSettings.Scores[0]);
    }

    public class Score
    {
        public ScoreRank Rank { get; private set; }
        public bool IsNewScore { get; private set; }
        public int Value { get; private set; }
        //const with fields params
        public int Best;
        public Score(ScoreRank scoreRank, bool isNewScore, int value, int best)
        {
            Rank = scoreRank;
            IsNewScore = isNewScore;
            Value = value;
            Best = best;
        }
    }
    public enum ScoreRank
    {
        First,
        Second,
        Third,
        Lower
    }
}