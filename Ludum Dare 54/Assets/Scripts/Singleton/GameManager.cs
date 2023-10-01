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


    public Action OnDragging;
    public Action OnRelease;
    
    public enum GameState
    {
        MainMenu,
        GamePlay,
        Pause,
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
        ViewportManager = GetComponentInChildren<ViewportManager>();
    }
    
    private void Start()
    {
        ChangeState(GameState.MainMenu);
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
                // Implement logic for Main Menu state
                Debug.Log("Game is in Main Menu");
                break;

            case GameState.GamePlay:
                // Implement logic for GamePlay state
                Debug.Log("Gameplay started");
                break;

            case GameState.Pause:
                // Implement logic for Pause state
                Debug.Log("Game is Paused");
                break;

            case GameState.GameOver:
                // Implement logic for GameOver state
                Debug.Log("Game Over");
                break;

            case GameState.Victory:
                // Implement logic for Victory state
                Debug.Log("You Win!");
                break;

            default:
                break;
        }
    }
}
