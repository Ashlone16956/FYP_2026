using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;
    public GameObject LevelText;

    public int currentLevel = 1;
    public int maxLevel = 5;
    public int[] wavesPerLevel = { 4, 6, 8, 10, 15 };

    public TextMeshProUGUI levelText;
    public LevelCompleteScreen levelCompleteScreen;

    void Awake()
    {

        if (Instance == null) Instance = this;
        else Destroy(gameObject);

    }

    void Start()
    {

        currentLevel = GameManager.Instance.currentLevel;
        UpdateLevelUI();

    }

    public int GetRequiredWavesForLevel()
    {

        if (currentLevel - 1 < wavesPerLevel.Length) //picks the amount of waves based on where in the array matches the level number.
            return wavesPerLevel[currentLevel - 1];

        return wavesPerLevel[wavesPerLevel.Length - 1];
    }

    public bool IsLevelComplete()
    {

        return WaveManager.Instance.currentWave > GetRequiredWavesForLevel(); //checks if the current wave is greater than required waves to complete the level.

    }

    public void CompleteLevel()
    {

        if (GameManager.Instance.IsGameOver) return;

        Debug.Log("Level Complete");

        if (GameManager.Instance.currentLevel < maxLevel)
        {

            GameManager.Instance.highestUnlockedLevel = Mathf.Max(GameManager.Instance.highestUnlockedLevel, GameManager.Instance.currentLevel + 1);

            GameManager.Instance.SaveProgress(); //saves the highest unlocked level on level completion
        }

        if (GameManager.Instance != null)
        {

            GameManager.Instance.IsPaused = true;

        }

        Time.timeScale = 0f;

        

        if (levelCompleteScreen != null)
        {

            levelCompleteScreen.ShowLevelComplete();

        }
        else
        {

            Debug.LogError("LEvelCompleteScreen reference is missing on LevelMAnager");

        }
        
        

    }

    public void UpdateLevelUI()
    {

        if (levelText !=null)
        {

            levelText.text = "Level: " + currentLevel;

        }

    }

    public void GoToNextLevel()
    {

        if (currentLevel < maxLevel)
        {

            currentLevel++;
            GameManager.Instance.currentLevel = currentLevel;
            UpdateLevelUI();

        }

    }

    void Update()
    {

        if (LevelText != null)
        {
            bool showing = !GameManager.Instance.IsPaused && !GameManager.Instance.IsGameOver;
            LevelText.SetActive(showing);
        }

    }
}
