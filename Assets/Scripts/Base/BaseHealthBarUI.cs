using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BaseHealthBarUI : MonoBehaviour
{

 
    public Image healthFill;
    public TextMeshProUGUI healthText;

    
    void Update()
    {

        
        int currentHealth = PlayerStats.Instance.currentBaseHealth;
        int maxHealth = PlayerStats.Instance.maxBaseHealth;


        healthFill.fillAmount = (float)currentHealth / maxHealth; //fills the bar  by the percentage of health left.

        healthText.text = Mathf.Clamp(currentHealth, 0, maxHealth) + " / " + maxHealth; //Changes the text on the healthbar to match the current health.

    }
}
