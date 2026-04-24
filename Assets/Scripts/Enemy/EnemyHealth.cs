using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public float maxHealth = 5f;
    private float currentHealth;

    public float moneyGiven = 1f;

    private PlayerStats playerStats;
    public Image enemyHealthBarFill;


    public void Initialize()
    {

        currentHealth = maxHealth;
        playerStats = PlayerStats.Instance;

    }

    public void TakeDamage(float damage)
    {

        currentHealth -= damage;

        UpdateHealthBar();

        if (currentHealth <= 0)
        {

            EnemyDie(); //Destroys the enemy if health runs out.

        }

    }

    void EnemyDie()
    {

        if (playerStats != null)
            playerStats.money += Mathf.FloorToInt(moneyGiven * PlayerStats.Instance.moneyMultiplier); //rounds down to a whole number.

        WaveManager.Instance.RegisterEnemyDeath();

        Destroy(gameObject);

    }

    void UpdateHealthBar()
    {

        if (enemyHealthBarFill == null) return;

        float enemyHealthPercent = currentHealth / maxHealth;

        enemyHealthBarFill.fillAmount = enemyHealthPercent; //fills the bar  by the percentage of health left.

    }
}