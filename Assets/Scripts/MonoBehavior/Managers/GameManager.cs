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
    public LanesDatabase lanes;

    public Button pauseBtn;

    public Button restartBtn;

    public Text gamePausedTxt;

    public Sprite pauseSprite;
    public Sprite resumeSprite;

    public Button playBtn;
    public Button settingsBtn;
    public Button storeBtn;

    public Canvas mainMenuCanvas;
    public Canvas inGameCanvas;
    public Canvas endGameCanvas;

    private void OnEnable()
    {
        gameData.gameState = GameState.MainMenu;
    }

    private void Start()
    {
        lanes.ResetLanes();

        inGameCanvas.gameObject.SetActive(false);
        endGameCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);

        gamePausedTxt.gameObject.SetActive(false);

        gameData.onEnd.AddListener(EndGame);

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

    public void GameHalt()
    {
        gameData.gameState = GameState.Pause;
        gameData.onPause.Invoke();
    }

    public void GameResume()
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

}
