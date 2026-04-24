using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{

    public GameObject GameOverScreen;

    public void ShowGameOver() //Shows the game over screen when killed.
    {
        
        Debug.Log("ShowGameOver called");
        Debug.Log("Panel ref: " + GameOverScreen);

        GameOverScreen.SetActive(true);
        Time.timeScale = 0f;

        if (GameManager.Instance != null)
        {

            GameManager.Instance.IsGameOver = true;
            GameManager.Instance.IsPaused = true;

        }

    }

    public void RestartGame() // Restarts on the screen when the button is clicked
    {

        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {

            GameManager.Instance.IsGameOver = false;
            GameManager.Instance.IsPaused = false;

        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void ReturnToMainMenu()
    {

        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {

            GameManager.Instance.IsGameOver = false;
            GameManager.Instance.IsPaused = false;

        }

        SceneManager.LoadScene(0);

    }

}
