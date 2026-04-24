using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenu : MonoBehaviour
{
    public GameObject LeaderboardScreen;
    public LeaderboardUI leaderboardUI;

    public void StartGame()
    {

        SceneManager.LoadScene("Main Game");

    }

    
    public void QuitGame()
    {

        Application.Quit();
        Debug.Log("Quit Game");
            

    }

    public void OpenLevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }

    public void OpenLeaderboard()
    {
        if (LeaderboardScreen != null)
        {
            LeaderboardScreen.SetActive(true);


        }

        if (leaderboardUI != null)
        {
            leaderboardUI.RefreshLeaderboard();
        }
    }

    
    public void CloseLeaderboard()
    {
        if (LeaderboardScreen != null)
        {
            LeaderboardScreen.SetActive(false);
        }
    }

}
