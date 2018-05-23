using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Master : MonoBehaviour
{

    public GameState gamestat;
    Button pauseBtn;
    public Text pauseBtnTxt;
    public Text gamePausedTxt;

    //public


    public void ExchangeState()
    {

        if (gamestat.isPaused)
        {
            GameResume();
            pauseBtnTxt.text = "Pause";
            
        }
        else
        {
            GamePaused();
            pauseBtnTxt.text = "Resume";
            
        }
    }

    public void GamePaused()
    {
        Time.timeScale = 0;
        gamestat.isPaused = true;
        gamePausedTxt.gameObject.SetActive(true);
        gamestat.onPause.Invoke();
    }

    public void GameResume()
    {
        Time.timeScale = 1;
        gamestat.isPaused = false;
        gamePausedTxt.gameObject.SetActive(false);
        gamestat.OnResume.Invoke();
    }

    private void OnApplicationQuit()
    {
        
    }
}
