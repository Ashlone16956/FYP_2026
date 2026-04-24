using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    private PlayerStats playerStats;
    public GameOverPanel gameOverScreen;
    void Start()
    {

        playerStats = PlayerStats.Instance;
        Debug.Log("Test log is working");

    }

    void Update()
    {

        

    }

    public void TakeDamage(int damage)
    {
        
        Debug.Log("Damage received: " + damage);
        playerStats.currentBaseHealth -= damage; //Adjusts the current health by the damage taken.
        Debug.Log("Base Health: " + playerStats.currentBaseHealth); //Tells the console the current health.


        if (playerStats.currentBaseHealth <= 0) 
        {

            playerStats.currentBaseHealth = 0; //Makes sure the base cant go into the negatives in health
            GameManager.Instance.IsGameOver = true;
            BaseDestroyed();
        
        }


    }

    void BaseDestroyed()
    {



        Debug.Log("Base Destroyed"); // Tells the console the base has been destroyed when health is less than or equal to 0.
        GameManager.Instance.IsGameOver = true;

        if (PlayFabStats.Instance !=null && WaveManager.Instance !=null)
        {

            PlayFabStats.Instance.SubmitHighestWave(WaveManager.Instance.currentWave); //Submits the highest wave when you die.

        }

        if (gameOverScreen != null)
        {

            gameOverScreen.ShowGameOver();

        }
        else
        {
            Debug.LogError("GameOverPanel reference is missing on BaseHealth.");
        }

    }

    
}
