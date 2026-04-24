using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public GameObject statsScreen;
    public TextMeshProUGUI statsText;
    

    void Update()
    {
        if (statsScreen.activeSelf)
        {

            UpdateStats();

        }
    }

    public void OpenStats()
    {

        statsScreen.SetActive(true);
        UpdateStats();

        GameManager.Instance.IsInMenu = true;
        Time.timeScale =  0f;

    }

    public void CloseStats()
    {

        statsScreen.SetActive(false);

        GameManager.Instance.IsInMenu = false;
        Time.timeScale = 1f;

    }

    void UpdateStats()
    {

        if (PlayerStats.Instance == null) return;

        var stats = PlayerStats.Instance;

        float attackSpeed = stats.baseShotInterval > 0 ? 1f / stats.baseShotInterval : 0f;

        statsText.text =
            $"Money: {stats.money}\n" +
            $"Click Damage: {stats.clickDamage}\n" +
            $"Base Health: {stats.currentBaseHealth}/{stats.maxBaseHealth}\n" +
            $"Thorns: {stats.thornsDamage}\n" +
            $"Base Shot Damage: {stats.baseShotDamage}\n" +
            $"Attack Speed: {attackSpeed:F2}/s";

    }
}
