using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    public float spawnInterval = 2f;

    private float timer;
    private Transform targetBase;

    public EnemyData[] animalTypes;

    
    void Start()
    {

        targetBase = GameObject.FindGameObjectWithTag("Base").transform;

    }

    
    void Update()
    {
        if (PlayerStats.Instance.currentBaseHealth <= 0)
            return; //Stops spawning enemies if the base dies

        if (WaveManager.Instance.animalsSpawned >= WaveManager.Instance.GetAnimalsThisWave()) //Stops spawning enemies if all the enemies in the wave are spawned
            return;


        timer += Time.deltaTime;

        float currentSpawnInterval = WaveManager.Instance.GetCurrentSpawnInterval();

        if (timer >= currentSpawnInterval)
        {

            SpawnEnemy();
            timer = 0f;

            WaveManager.Instance.RegisterAnimalSpawn();
        }
    }

    void SpawnEnemy()
    {

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector2 spawnPosition = (Vector2)targetBase.position + randomDirection * spawnRadius;

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        EnemyData SpawnRandomEnemy() //Picks a random animal to spawn, higher weights being more common, lower being rarer.
        {

            int totalWeight = 0;

            foreach (EnemyData enemy in animalTypes)
            {

                totalWeight += enemy.spawnWeight;

            }

            int randomValue = Random.Range(0, totalWeight);

            int currentWeight = 0;

            foreach (EnemyData enemy in animalTypes)
            {

                currentWeight += enemy.spawnWeight;

                if (randomValue < currentWeight)
                {

                    return enemy;

                }

            }

            return animalTypes[0]; //fallback

        }

        EnemyData randomEnemy = SpawnRandomEnemy();

        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        EnemyAttack attack = enemy.GetComponent<EnemyAttack>();
        SpriteRenderer sprite = enemy.GetComponent<SpriteRenderer>(); //Applie sthe stats and sprite

        int wave = WaveManager.Instance.currentWave;
        
        float healthGrowthRate = 0.15f;
        float damageGrowthRate = 0.10f;
        float rewardGrowthRate = 0.08f; //rates in which stats grow.

        int scaledHealth = randomEnemy.maxHealth;
        int scaledDamage = randomEnemy.damage;
        int scaledMoneyGiven = randomEnemy.moneyGiven;

        if (wave > 1)
        {
            scaledHealth += Mathf.Max(1, Mathf.FloorToInt(randomEnemy.maxHealth * healthGrowthRate * (wave - 1))); //ensures at least +1 every wave.
            scaledDamage += Mathf.Max(1, Mathf.FloorToInt(randomEnemy.damage * damageGrowthRate * (wave - 1)));
            scaledMoneyGiven += Mathf.Max(1, Mathf.FloorToInt(randomEnemy.moneyGiven * rewardGrowthRate * (wave - 1)));
        }

        if (health != null)
        {
            health.maxHealth = scaledHealth;
            health.moneyGiven = scaledMoneyGiven;
            health.Initialize();
        }

        if (attack != null)
        {
            attack.damagePerHit = scaledDamage;
        }

        if (sprite != null)
        {
            sprite.sprite = randomEnemy.sprite;
        }

    }
}
