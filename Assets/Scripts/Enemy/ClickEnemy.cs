using UnityEngine;

public class ClickEnemy : MonoBehaviour
{
    private EnemyHealth enemyHealth;
    private PlayerStats playerStats;
    private BaseHealth baseHealth;

    void Start()
    {
        
        enemyHealth = GetComponent<EnemyHealth>();
        playerStats = PlayerStats.Instance;
        baseHealth = FindAnyObjectByType<BaseHealth>();

    }

    void OnMouseDown()
    {
        if (GameManager.Instance.IsPaused) return;
        if (GameManager.Instance.IsGameOver) return;
        if (PlayerStats.Instance.currentBaseHealth <= 0) return;
        
        if (enemyHealth != null && playerStats != null) //checks if the base has health, enemy health is available and player stats too
        {

            enemyHealth.TakeDamage(playerStats.clickDamage);
            Debug.Log("Hit enemy for " + playerStats.clickDamage + " damage!");


        }

    }

}
