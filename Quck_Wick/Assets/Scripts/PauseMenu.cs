using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject resumeButton, controlsButton, mainMenuButton, quitButton, backButton;

    public GameObject controlsText;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Controls()
    {
        resumeButton.SetActive(false);
        controlsButton.SetActive(false);
        mainMenuButton.SetActive(false);
        quitButton.SetActive(false);
        backButton.SetActive(true);
        controlsText.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        resumeButton.SetActive(true);
        controlsButton.SetActive(true);
        mainMenuButton.SetActive(true);
        quitButton.SetActive(true);
        backButton.SetActive(false);
        controlsText.SetActive(false);
    }
}
