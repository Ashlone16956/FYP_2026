using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelectScreen : MonoBehaviour
{
    public Button[] levelButtons;
    public TMP_Text[] levelButtonTexts;

    void Start()
    {

        UpdateLevelButtons();

    }

    void UpdateLevelButtons()
    {

        int highestUnlocked = GameManager.Instance.highestUnlockedLevel;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelNumber = i + 1;
            bool unlocked = levelNumber <= highestUnlocked; //checks if the level is locked or unlocked

            levelButtons[i].interactable = unlocked;

            if (levelButtonTexts != null && i < levelButtonTexts.Length)
            {

                levelButtonTexts[i].text = unlocked ? "Level " + levelNumber : "Locked"; //displays locked if the level hasnt been unlocked yet.

            }
        }
    }

    public void LoadLevel(int levelNumber)
    {

        if (levelNumber > GameManager.Instance.highestUnlockedLevel)
            return;

        GameManager.Instance.currentLevel = levelNumber;
        GameManager.Instance.IsPaused = false;
        GameManager.Instance.IsGameOver = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene("Levels");

    }

    public void BackToMainMenu()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }
}
