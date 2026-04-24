using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{

    public TextMeshProUGUI moneyText;
    private PlayerStats playerStats;

    void Start()
    {

        playerStats = PlayerStats.Instance;

    }

    
    void Update()
    {

        if (playerStats != null)
        {

            moneyText.text = "Money: " + Mathf.FloorToInt(playerStats.money);

        }

    }
}
