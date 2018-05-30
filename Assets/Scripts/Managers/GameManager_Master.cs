using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles In game management and Main menu UI management
/// </summary>
public class GameManager_Master : MonoBehaviour
{
    public GameData gameData;
    public Button pauseBtn;

    public Button restartBtn;

    public Text gamePausedTxt;

    public Sprite pauseSprite;
    public Sprite resumeSprite;

    public Button playBtn;
    public Button settingsBtn;
    public Button storeBtn;

    Animator settingAnim;
    Animator storeAnim;

    public Canvas mainMenuCanvas;
    public Canvas inGameCanvas;
    public Canvas endGameCanvas;

    private void Start()
    {
        GameStart();
        gamePausedTxt.gameObject.SetActive(false);
        settingAnim = settingsBtn.GetComponent<Animator>();
        storeAnim = storeBtn.GetComponent<Animator>();
    }

    public void PlayBtnEntered()
    {
        settingAnim.SetBool("SetBtnIsOut", false);
        storeAnim.SetBool("StoreBtnIsOut", false);
        StartCoroutine(WaitforStart());
        
        GameResume();
    }

    public void RestartBtnEntered()
    {
        StartCoroutine(WaitforStart());

        GameResume();
    }

    public void PauseBtnEntered()
    {
        switch (gameData.gameState)
        {
            case GameState.gamePlayState:
                gamePausedTxt.gameObject.SetActive(true);
                pauseBtn.GetComponent<Image>().sprite = resumeSprite;
                GameHalt();
                break;

            case GameState.pauseState:
                gamePausedTxt.gameObject.SetActive(false);
                pauseBtn.GetComponent<Image>().sprite = pauseSprite;
                GameResume();
                break;
        }
    }

    void GameHalt()
    {
        gameData.gameState = GameState.pauseState;
        gameData.onPause.Invoke();
    }

    void GameResume()
    {
        gameData.gameState = GameState.gamePlayState;
        gameData.OnResume.Invoke();
    }


    /// <summary>
    /// acts as pause but at the beginning of the game
    /// </summary>
    void GameStart()
    {
        inGameCanvas.gameObject.SetActive(false);
        endGameCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);

        gameData.gameState = GameState.startState;
        gameData.OnStart.Invoke();
    }

    /// <summary>
    /// acts as a pause but at the end of the game
    /// </summary>
    void EndGame()
    {
        inGameCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(false);
        endGameCanvas.gameObject.SetActive(true);

        gameData.gameState = GameState.gameEnded;
        //gameData.onEnd.Invoke();
    }


    IEnumerator WaitforStart()
    {
        yield return new WaitForSeconds(0.5f);
        mainMenuCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(true);
    }
}
