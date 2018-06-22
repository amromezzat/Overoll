using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class PlayGameScript : MonoBehaviour
{
	void Start ()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
	}

    void SignIn()
    {
        Social.localUser.Authenticate(success => { });
    }


    #region Achievments
    public static void UnlockAchievment(string id)
    {
        Social.ReportProgress(id, 100, sucess => { });
    }

    public static void IncrementAchievment (string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }

    public static void ShowAchievmentsUI()
    {
        Social.ShowAchievementsUI();
    }
    #endregion Achievments

    #region Leaderboard
    public static void AddScoreToLeaderboard(string leaderboardID, long score)
    {
        Social.ReportScore(score, leaderboardID, success => { });
    }

    public static void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }
    #endregion Leaderboard
}
