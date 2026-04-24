using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public int currentWave = 1;

    public float baseSpawnInterval = 2f;
    public float spawnIntervalDecreasePerWave = 0.1f;
    public float minimumSpawnInterval = 0.5f;

    public TextMeshProUGUI waveText;

    public int animalsSpawned = 0;
    public int baseAnimalsPerWave = 5;
    public int addedAnimalsPerWave = 2;
    

    private int animalsAlive = 0;
    public int animalsRemaining = 0;

    public GameObject WaveInfoText;

    void Awake()
    {

        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        StartWave();
    }
    
    void Update()
    {

        if (WaveInfoText == null) return; 

        bool showing = !GameManager.Instance.IsPaused && !GameManager.Instance.IsGameOver; //Makes the text not show if not in the main game.

        WaveInfoText.SetActive(showing);

    }

    public void StartWave()
    {

        animalsSpawned = 0;
        animalsAlive = 0;
        animalsRemaining = GetAnimalsThisWave();
        UpdateWaveUI();

    }
    public void NextWave()
    {

        currentWave++; // increments the current wave counter
        Debug.Log("Advanced to wave: " + currentWave);

        if (LevelManager.Instance != null && LevelManager.Instance.IsLevelComplete())
        {

            Debug.Log("Level complete conditions met");
            LevelManager.Instance.CompleteLevel();
            return;


        }

        StartWave();

    }

    public float GetCurrentSpawnInterval()
    {

        float interval = baseSpawnInterval - ((currentWave - 1) * spawnIntervalDecreasePerWave); // decreases the spawn interval for every wave until a minimum of 0.5 seconds.
        return Mathf.Max(minimumSpawnInterval, interval);

    }

    void UpdateWaveUI()
    {

        if (waveText != null)
        {

            waveText.text = "Wave: " + currentWave + "\nAnimals Left: " + animalsRemaining; //The text displaying the current wave

        }

        

    }

    public int GetAnimalsThisWave()
    {

        return baseAnimalsPerWave + ((currentWave - 1) * addedAnimalsPerWave);

    }

    public void RegisterAnimalSpawn()
    {

        animalsSpawned++;
        animalsAlive++;

        UpdateWaveUI();

    }

    public void RegisterEnemyDeath()
    {

        animalsAlive--;
        animalsRemaining--;

        if (animalsRemaining < 0)
            animalsRemaining = 0;
        UpdateWaveUI();

        if (animalsRemaining <= 0 && animalsAlive <=0) //Checks if all enemies are dead and all have been spawned to progress to the next wave
        {

            animalsSpawned = 0;
            NextWave();

        }

    }
}
