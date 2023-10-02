using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState currentState { get; private set; }

    public ViewportManager ViewportManager { get; private set; }

    public int score = 0;

    public Action OnDraggingUI;
    public Action OnReleaseUI;

    public Action OnMainMenu;
    public Action OnGameStarts;
    // public Action OnGamePauses;
    public Action OnGameOver;
    public Action OnGameWins;

    public enum GameState
    {
        MainMenu,
        GamePlay,
        // Pause,
        GameOver,
        Victory
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ViewportManager = GetComponentInChildren<ViewportManager>();
    }
    
    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    private void Update()
    {
        LoadScene();
    }

    public void ChangeState(string newState)
    {
        currentState = (GameState)Enum.Parse(typeof(GameState), newState);
        OnStateChange();
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        OnStateChange();
    }

    private void OnStateChange()
    {
        switch (currentState)
        {
            case GameState.MainMenu:
                // OnMainMenu();
                // Implement logic for Main Menu state
                Debug.Log("Game is in Main Menu");
                break;

            case GameState.GamePlay:
                // OnGameStarts();
                // Implement logic for GamePlay state
                Debug.Log("Gameplay started");
                break;

            // case GameState.Pause:
            //     OnGamePauses();
            //     // Implement logic for Pause state
            //     Debug.Log("Game is Paused");
            //     break;

            case GameState.GameOver:
                // OnGameOver();
                CalculateScore();
                // SceneManager.LoadScene("GameOver");
                PlayerController.instance.spaceShip.SetActive(true);
                // Implement logic for GameOver state
                Debug.Log("Game Over");
                break;

            case GameState.Victory:
                // OnGameWins();
                PlayerController.instance.spaceShip.SetActive(true);
                    // SceneManager.LoadScene("GameWins");
                CalculateScore();
                // SceneManager.LoadScene("GameWins");
                // Implement logic for Victory state
                Debug.Log("You Win!");
                break;

            default:
                break;
        }
    }

    private void CalculateScore()
    {
        score = GameObject.Find("Inventory").transform.childCount - 3;
    }

    private void LoadScene()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().name.Equals("Menu"))
            {
                // SceneManager.LoadScene("Main");
                // ChangeState(GameState.GamePlay);
                GameObject.Find("Intro Animation").GetComponent<IntroAnimation>().enabled = true;
            }
            // else if (SceneManager.GetActiveScene().name.Equals("GameOver") ||
            //          SceneManager.GetActiveScene().name.Equals("GameWins"))
            // {
            //     SceneManager.LoadScene("Menu");
            //     ChangeState(GameState.MainMenu);
            // }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (SceneManager.GetActiveScene().name.Equals("GameOver"))
            {
                GameObject.Find("Ending Animation").GetComponent<EndingAnimation>().enabled = true;
            }
        }
    }
}
