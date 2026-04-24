using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScreen : MonoBehaviour
{

    public GameObject levelCompleteScreen;

    public void ShowLevelComplete() //Shows the level complete screen
    {

        Debug.Log("ShowLevelComplete called");

        if (levelCompleteScreen != null)
        {

            levelCompleteScreen.SetActive(true);

        }
        else
        {

            Debug.LogError("levelCompleteScreen GameObject is not assigned.");

        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {

            GameManager.Instance.IsPaused = false;
            GameManager.Instance.IsGameOver = false;

        }

        if (LevelManager.Instance != null)
        {

            LevelManager.Instance.GoToNextLevel();

        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {

        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {

            GameManager.Instance.IsPaused = false;
            GameManager.Instance.IsGameOver = false;

        }

        SceneManager.LoadScene(0);


    }

}
