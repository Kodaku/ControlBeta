using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject commandsMenuUI;
    public GameObject optionsMenuUI;
    public GameObject confirmMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))    
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        commandsMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        confirmMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        commandsMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        confirmMenuUI.SetActive(false);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void CommandsMenu()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        commandsMenuUI.SetActive(true);
        confirmMenuUI.SetActive(false);
    }

    public void OptionsMenu()
    {
        optionsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        commandsMenuUI.SetActive(false);
        confirmMenuUI.SetActive(false);
    }

    public void ConfirmMenu()
    {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        commandsMenuUI.SetActive(false);
        confirmMenuUI.SetActive(true);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
