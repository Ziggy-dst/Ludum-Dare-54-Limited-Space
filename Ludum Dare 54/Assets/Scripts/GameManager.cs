using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // [LabelText("Managers")]

    // [LabelText("Actions")]

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        // LevelManager = GetComponentInChildren<LevelManager>();
    }

    private void Update()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        // reset when mouse middle button is pressed
        if (Input.GetMouseButtonDown(2)) SceneManager.LoadScene(0);
    }
}
