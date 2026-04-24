using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public int damagePerHit = 5;
    public float attackInterval = 1f;

    private float attackTimer;
    private BaseHealth baseHealth;

    void OnCollisionStay2D(Collision2D collision) //runs every frame wjile the enemy is touching a collider.
    {
        

        if (collision.gameObject.CompareTag("Base"))
        {
            if (!collision.gameObject.CompareTag("Base"))
                return;

            if (PlayerStats.Instance.currentBaseHealth <= 0 || GameManager.Instance.IsPaused || GameManager.Instance.IsGameOver)
                return; //Stops attacking if the base is destroyed or if the game is paused.

            if (baseHealth == null)
            {
                baseHealth = collision.gameObject.GetComponent<BaseHealth>();
            }

            attackTimer += Time.deltaTime;

            if (attackTimer >= attackInterval) //checks if ebough time has passed so it can attack again.
            {

                if (baseHealth != null)
                {

                    baseHealth.TakeDamage(damagePerHit);

                    if (PlayerStats.Instance.thornsDamage > 0) //Enemy takes damage every attack interval once if thorns is above 0.
                    {
                        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();

                        if (enemyHealth != null)
                        {

                            enemyHealth.TakeDamage(PlayerStats.Instance.thornsDamage);

                        }

                    }

                }

                attackTimer = 0f; //resets the timer to wait another second.
            }

            

        }

    }

    void OnCollisionExit2D(Collision2D collision) //Called when an enemy stops touching the base
    {

        if (GameManager.Instance.IsPaused) return;
        if (GameManager.Instance.IsGameOver) return;

        if (collision.gameObject.CompareTag("Base"))
        {

            attackTimer = 0f; // resets the timer so the enemy doesnt instantly do damage if it touches again.

        }

    }

}
