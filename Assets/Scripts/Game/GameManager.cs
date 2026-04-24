using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public bool IsPaused = false;
    public bool IsGameOver = false;
    public int currentLevel = 1;
    public int highestUnlockedLevel = 1;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void SaveProgress() //saves the highest unlocked level so levels are unlocked upon closing and opening the game.
    {
        PlayerPrefs.SetInt("HighestUnlockedLevel", highestUnlockedLevel);
        PlayerPrefs.Save();
    }

    public void LoadProgress() //loads the data when called.
    {
        highestUnlockedLevel = PlayerPrefs.GetInt("HighestUnlockedLevel", 1);
    }

    public void ResetProgress() //Resets the level progress.
    {
        highestUnlockedLevel = 1;
        currentLevel = 1;

        PlayerPrefs.DeleteKey("HighestUnlockedLevel");
        PlayerPrefs.Save();
    }
}


