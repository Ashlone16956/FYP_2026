using UnityEngine;

public class BaseShooter : MonoBehaviour
{
    private float shotTimer;
    public GameObject projectilePrefab;
    public Transform firePoint;

    void Update()
    {

        if (GameManager.Instance.IsPaused || GameManager.Instance.IsGameOver)
            return;

        if (PlayerStats.Instance.baseShotDamage <= 0)
            return;

        shotTimer += Time.deltaTime;

        if (shotTimer >= PlayerStats.Instance.baseShotInterval)
        {

            ShootClosestEnemy();
            shotTimer = 0f;

        }

    }

    void ShootClosestEnemy() //shoots based on the closest enemy.
    {

        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None); //uses objecst with enemy health to know what to shoot.

        if (enemies.Length == 0)
            return;

        EnemyHealth closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (EnemyHealth enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {

                closestDistance = distance;
                closestEnemy = enemy;

            }
        }

        if (closestEnemy != null)
        {
            Transform spawnPoint = firePoint != null ? firePoint : transform;

            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity); //initiates the base bullet

            BaseProjectile projectileScript = projectile.GetComponent<BaseProjectile>();

            if (projectileScript != null)
            {

                projectileScript.SetTarget(
                    closestEnemy.transform,
                    PlayerStats.Instance.baseShotDamage
                );

            }
        }
    }
}
