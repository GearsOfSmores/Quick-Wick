using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsSet;
    public GameObject quitButton;
    public GameObject creditsButton;
    public GameObject backButton;

    public void StartButton()
    {
        SceneManager.LoadScene("TutorialBegin");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        creditsButton.SetActive(false);
        quitButton.SetActive(false);
        backButton.SetActive(true);
        creditsSet.SetActive(true);
    }

    public void Back()
    {
        backButton.SetActive(false);
        creditsSet.SetActive(false);
        creditsButton.SetActive(true);
        quitButton.SetActive(true);
    }
}
