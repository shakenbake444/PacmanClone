
using System;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            print("esc pressed");
            if (gameIsPaused)
            {
                Resume();
            } else 
            {
                PauseTheGame();
            }
        }
        
    }

    public void Resume()
    {
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void PauseTheGame()
    {
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
