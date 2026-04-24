using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public float clickDamage = 1f;
    public int money = 0;
    public float moneyMultiplier = 1f;
    public int maxBaseHealth = 100;
    public int currentBaseHealth;
    public int thornsDamage = 0;
    public int baseShotDamage = 0;
    public float baseShotInterval = 1.5f;
    public float minimumBaseShotInterval = 0.2f;

    void Start()
    {

        currentBaseHealth = maxBaseHealth;

    }

    void Awake()
    {

        if (Instance == null) Instance = this;
        else Destroy(gameObject);

    }
}