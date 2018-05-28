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

    private void OnEnable()
    {
        GameStart();
        gamePausedTxt.gameObject.SetActive(false);
        settingAnim = settingsBtn.GetComponent<Animator>();
        storeAnim = storeBtn.GetComponent<Animator>();
    }

    void PlayBtnEntered()
    {
        settingAnim.SetBool("SetBtnIsOut", false);
        storeAnim.SetBool("StoreBtnIsOut", false);
        GameResume();
    }

    void PauseBtnEntered()
    {
        gamePausedTxt.gameObject.SetActive(true);
        GameHalt();
    }

    void ResumeBtnEntered()
    {
        gamePausedTxt.gameObject.SetActive(false);
        GameResume();
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
    /// acts as pause but in the beginning of the game
    /// </summary>
    void GameStart()
    {
        inGameCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);

        gameData.gameState = GameState.startState;
        gameData.OnStart.Invoke();
    }

}
