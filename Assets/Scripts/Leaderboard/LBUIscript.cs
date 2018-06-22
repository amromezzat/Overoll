using UnityEngine;
using UnityEngine.UI;

public class LBUIscript : MonoBehaviour
{
    public static LBUIscript Instance { get; private set; }

    [SerializeField]
    private Text pointsTxt;

    LBManagerScript LBManagerscript;

    private void Start()
    {
        Instance = this;
        LBManagerscript = GetComponent<LBManagerScript>();
    }

    public void Restart()
    {
        LBManagerScript.Instance.RestartGame();
    }

    // those will be used when there is achievments!!!

    //public void Increment()
    //{
    //    PlayGameScript.IncrementAchievment(GPGSIds.achievmnet_incremntal_achievment,5);
    //}

    //public void Unlock()
    //{
    //    PlayGameScript.UnlockAchievment(GPGSIds.achievemet_standard_achievment);
    //}

    //public void ShowAchievments()
    //{
    //    PlayGameScript.ShowAchievmentsUI();
    //}

    public void ShowLeaderboards()
    {
        PlayGameScript.ShowLeaderboardUI();
    }

    //public void UpdatePointsTxt()
    //{
    //    pointsTxt.text = LBManagerscript.LB_score.ToString();
    //}
}
