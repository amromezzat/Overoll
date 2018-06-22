using UnityEngine;

public class LBManagerScript : MonoBehaviour
{
    public static LBManagerScript Instance { get; private set; }

    public ScoreManager scoreManager;

    [HideInInspector]
    public int LB_score;

    void Start()
    {
        Instance = this;
        LB_score = scoreManager.score;
    }
    
    public void RestartGame()
    {
        PlayGameScript.AddScoreToLeaderboard(GPGSIds.leaderboard_leaderboard, LB_score);
      
        //LBUIscript.Instance.UpdatePointsTxt();
    }
}
