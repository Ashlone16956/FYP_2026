using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 1;

    private Transform target;

    public void SetTarget(Transform newTarget, int newDamage)
    {

        target = newTarget;
        damage = newDamage;
    }

    void Update()
    {
        if (target == null)
        {
            FindNewTarget(); //keeps checking for the closest target.

            if (target == null)
            {
                Destroy(gameObject);
                return;
            }
        }

        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        float distance = Vector2.Distance(transform.position, target.position);

        if (distance < 0.15f)
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
    
    void FindNewTarget() // Used if enemy dies before bullet collides.
        {
            EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);

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
                target = closestEnemy.transform;
            }
        }
}


