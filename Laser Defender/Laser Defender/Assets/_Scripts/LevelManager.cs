using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void GoStartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoLoseScene()
    {
        StartCoroutine(LoadQuitScreen());
    }

    public void GoStartMenu()
    {
        Debug.Log("Start");
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadQuitScreen()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("LoseScreen");
    }
}
