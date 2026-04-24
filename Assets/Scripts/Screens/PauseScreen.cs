using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseScreen : MonoBehaviour
{

    public GameObject optionsScreen;
    public TextMeshProUGUI fullscreenButtonText;

    private bool isPaused = false;
    private bool isFullscreen;
    // Update is called once per frame

    void Start()
    {

        isFullscreen = Screen.fullScreen;
        UpdateFullscreenText();

    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            TogglePause(); //GIves the option to use the esc key to pause as well

        }

    }

    public void TogglePause()
    {

        isPaused = !isPaused;

        optionsScreen.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        GameManager.Instance.IsPaused = isPaused;
        UpdateFullscreenText(); //changes the text based on windowed or Fullscreen.

    }

    public void ResumeGame()
    {

        isPaused = false;
        optionsScreen.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.IsPaused = false;

    }

    public void BackToMainMenu()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu"); //Goes back to the main menu when the button is clicked.

    }

    public void ToggleFullScreen()
    {

        isFullscreen = !isFullscreen;
        Screen.fullScreen = isFullscreen; //toggles fullscreen or windowed.
        UpdateFullscreenText();

    }

    void UpdateFullscreenText()
    {

        if (isFullscreen)
            fullscreenButtonText.text = "Enter Windowed";
        else
            fullscreenButtonText.text = "Enter Fullscreen";

    }
}
