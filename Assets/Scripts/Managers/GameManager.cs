﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Gameplay,
    Pause,
    GameOver
};

/// <summary>
/// Handles In game management and Main menu UI management
/// </summary>
public class GameManager : MonoBehaviour
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

    AudioManager audioManager;

    private void OnEnable()
    {
        gameData.gameState = GameState.MainMenu;
    }

    private void Start()
    {
        inGameCanvas.gameObject.SetActive(false);
        endGameCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);

        gamePausedTxt.gameObject.SetActive(false);
        settingAnim = settingsBtn.GetComponent<Animator>();
        storeAnim = storeBtn.GetComponent<Animator>();

        // will be used later with fx
        audioManager = GetComponent<AudioManager>();
;

        gameData.onEnd.AddListener(EndGame);

    }

    public void PlayBtnEntered()
    {
        settingAnim.SetBool("SetBtnIsOut", false);
        storeAnim.SetBool("StoreBtnIsOut", false);
        StartCoroutine(WaitforStart());

        GameStart();
    }

    public void RestartBtnEntered()
    {
        SceneManager.LoadScene("Main");
        endGameCanvas.gameObject.SetActive(false);
    }

    public void PauseBtnEntered()
    {
        switch (gameData.gameState)
        {
            case GameState.Gameplay:
                gamePausedTxt.gameObject.SetActive(true);
                pauseBtn.GetComponent<Image>().sprite = resumeSprite;
                GameHalt();
                break;

            case GameState.Pause:
                gamePausedTxt.gameObject.SetActive(false);
                pauseBtn.GetComponent<Image>().sprite = pauseSprite;
                GameResume();
                break;
        }
    }

    void GameHalt()
    {
        gameData.gameState = GameState.Pause;
        gameData.onPause.Invoke();
    }

    void GameResume()
    {
        gameData.gameState = GameState.Gameplay;
        gameData.OnResume.Invoke();
    }

    void GameStart()
    {
        gameData.gameState = GameState.Gameplay;
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

        gameData.gameState = GameState.GameOver;
       
    }


    IEnumerator WaitforStart()
    {
        yield return new WaitForSeconds(0.5f);
        mainMenuCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(true);
    }
}
