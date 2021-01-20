using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool Open;
    public GameObject pauseMenuUI;
    public GameObject firstOne, closeOption, openOption, closeExit, closeRestart, openRestart, openExit;
    public GameObject pause, option, exit, restart;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Open == false)
        {
            if (GameIsPaused)
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
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstOne);

    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Option()
    {
        pause.SetActive(false);
        option.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(openOption);
    }

    public void LeaveOption()
    {
        pause.SetActive(true);
        option.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeOption);
    }

    public void Restart()
    {
        pause.SetActive(false);
        restart.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(openRestart);
    }

    public void LeaveRestart()
    {
        pause.SetActive(true);
        restart.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeRestart);
    }

    public void Exit()
    {
        pause.SetActive(false);
        exit.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(openExit);
    }

    public void LeaveExit()
    {
        pause.SetActive(true);
        exit.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeExit);
    }
}
