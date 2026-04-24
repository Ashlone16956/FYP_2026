using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{

    public PlayerStats playerStats;

    public TMP_Text levelText;
    public TMP_Text costText;

    public int baseCost = 10;
    public float costMultiplier = 1.5f;
    public float increaseAmount = 1f;
    private int level = 0;
    private int currentCost;
    public enum UpgradeType //All of the upgrades
    {

        ClickDamage,
        MoneyMultiplier,
        BaseHealth,
        Thorns,
        BaseShotDamage,
        BaseShotSpeed,

    }

    public UpgradeType upgradeType;

    void Start()
    {

        currentCost = baseCost;
        UpdateUI();

    }

    public void BuyUpgrade()
    {

        if (playerStats.money < currentCost)
            return;

        playerStats.money -= currentCost;
        level++; //iterates the level by 1 if the players money is more than the cost of the upgrade, and purchases the upgrade.

        ApplyUpgrade();

        currentCost = Mathf.FloorToInt(baseCost * Mathf.Pow(costMultiplier, level)); //increases the cost

        UpdateUI();


    }

    void ApplyUpgrade()
    {

        switch (upgradeType) //The different upgrades to be applied
        {

            case UpgradeType.ClickDamage:
                playerStats.clickDamage += increaseAmount;
                break;

            case UpgradeType.MoneyMultiplier:
                playerStats.moneyMultiplier += increaseAmount;
                break;

            case UpgradeType.BaseHealth:
                playerStats.maxBaseHealth += Mathf.RoundToInt(increaseAmount);
                playerStats.currentBaseHealth = playerStats.maxBaseHealth;
                break;

            case UpgradeType.Thorns:
                playerStats.thornsDamage += Mathf.RoundToInt(increaseAmount);
                break;

            case UpgradeType.BaseShotDamage:
                playerStats.baseShotDamage += Mathf.RoundToInt(increaseAmount);
                break;

            case UpgradeType.BaseShotSpeed:
                playerStats.baseShotInterval -= increaseAmount;

               
                if (playerStats.baseShotInterval < playerStats.minimumBaseShotInterval) //Makes sure the shots dont get too fast
                {
                    playerStats.baseShotInterval = playerStats.minimumBaseShotInterval;
                }
                break;


        }

    }

    void UpdateUI()
    {

        levelText.text = "Level: " + level;
        costText.text = "Cost: " + Mathf.Round(currentCost);

    }
}
