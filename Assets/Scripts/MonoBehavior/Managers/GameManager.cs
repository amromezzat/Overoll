/*Licensed to the Apache Software Foundation (ASF) under one
or more contributor license agreements.  See the NOTICE file
distributed with this work for additional information
regarding copyright ownership.  The ASF licenses this file
to you under the Apache License, Version 2.0 (the
"License"); you may not use this file except in compliance
with the License.  You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing,
software distributed under the License is distributed on an
"AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
KIND, either express or implied.  See the License for the
specific language governing permissions and limitations
under the License.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    public GameState gameState;

    public IntField difficulty;

    public LanesDatabase lanes;
    
    public Text gamePausedTxt;
    public GameObject PauseMenu;
    public GameObject SettingMenu;

    public Sprite pauseSprite;
    public Sprite resumeSprite;

    public Button pauseBtn;
    public Button restartBtn;
    public Button playBtn;
    public Button settingsBtn;
    public Button storeBtn;

    [HideInInspector]
    public UnityEvent OnStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnResume;
    [HideInInspector]
    public UnityEvent onPause;
    [HideInInspector]
    public UnityEvent onEnd;

    public Canvas mainMenuCanvas;
    public Canvas inGameCanvas;
    public Canvas endGameCanvas;
    bool fromMenu = false;
    bool fromPause = false;
    Image pauseButtonImg;

    private void Awake()
    {
        pauseButtonImg = pauseBtn.GetComponent<Image>();

        gameState = GameState.MainMenu;
#if !UNITY_EDITOR
        difficulty.Value= PlayerPrefs.GetInt("PlayedTutorial");

#endif
    }

    private void Start()
    {
        lanes.ResetLanes();
        AudioManager.instance.PlayMusic("Title music");
        inGameCanvas.gameObject.SetActive(false);
        endGameCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
        PauseMenu.gameObject.SetActive(false);
        SettingMenu.gameObject.SetActive(false);
        gamePausedTxt.gameObject.SetActive(false);

        onEnd.AddListener(EndGame);
    }

    public void PlayBtnEntered()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(true);
        GameStart();
    }

    public void RestartBtnEntered()
    {
        SceneManager.LoadScene("Main");
        endGameCanvas.gameObject.SetActive(false);
    }

    public void PauseBtnEntered()
    {
        switch (gameState)
        {
            case GameState.Gameplay:
                //  gamePausedTxt.gameObject.SetActive(true);
                PauseMenu.SetActive(true);
                pauseButtonImg.sprite = resumeSprite;
                inGameCanvas.gameObject.SetActive(false);
                GameHalt();
                break;

            case GameState.Pause:
                //  gamePausedTxt.gameObject.SetActive(false);
                PauseMenu.SetActive(false);
                pauseButtonImg.sprite = pauseSprite;
                GameResume();
                break;
        }
    } 

    public void EnteredSettingBtnfromMainMenu()
    {
        fromMenu = true;
        PauseMenu.gameObject.SetActive(false);
        SettingMenu.gameObject.SetActive(true);
        mainMenuCanvas.gameObject.SetActive(false);
    }
    public void EnteredSettingBtnfromPausedMenu()
    {
        fromPause = true;
        PauseMenu.gameObject.SetActive(false);
        SettingMenu.gameObject.SetActive(true);
    }
    public void ExitSettingMenu()
    {
        if (fromPause)
        {
            PauseMenu.gameObject.SetActive(true);
            SettingMenu.gameObject.SetActive(false);
        }
        if (fromMenu)
        {
            SettingMenu.gameObject.SetActive(false);
            mainMenuCanvas.gameObject.SetActive(true);
        }
    }
  
    public void GameHalt()
    {
        AudioManager.instance.PlaySound("Title music");

        gameState = GameState.Pause;
        SpeedManager.Instance.speed.Value = 0;
        onPause.Invoke();
    }

    public void GameResume()
    {
        AudioManager.instance.PlaySound("Overoll music");
        PauseMenu.gameObject.SetActive(false);
        gameState = GameState.Gameplay;
        SpeedManager.Instance.ResetSpeed();
        inGameCanvas.gameObject.SetActive(true);
        pauseButtonImg.sprite = pauseSprite;
        OnResume.Invoke();
    }

    void GameStart()
    {
        AudioManager.instance.PlayMusic("Overoll music");

        gameState = GameState.Gameplay;
        SpeedManager.Instance.ResetSpeed();
        OnStart.Invoke();
    }

    /// <summary>
    /// acts as a pause but at the end of the game
    /// </summary>
    void EndGame()
    {
        AudioManager.instance.PlaySound("Lose the game");

        SpeedManager.Instance.speed.Value = 0;

        //AudioManager.instance.PlaySound("Lose the game");

        inGameCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(false);
        endGameCanvas.gameObject.SetActive(true);

        gameState = GameState.GameOver;
       
    } 
}
