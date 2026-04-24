using TMPro;
using UnityEngine;

public class DisplayNameUI : MonoBehaviour
{
    public TMP_InputField displayNameInput;
    public TextMeshProUGUI displayNameText;

    void Start()
    {
        if (PlayFabLogin.Instance != null)
        {

            string savedName = PlayFabLogin.Instance.GetSavedDisplayName();

            displayNameInput.text = PlayFabLogin.Instance.GetSavedDisplayName();
            UpdateDisplayText(savedName);

        }
    }

    public void SaveDisplayName()
    {

        if (PlayFabLogin.Instance == null) return;

        PlayFabLogin.Instance.SetDisplayName(displayNameInput.text, (updatedName) =>
        {

            UpdateDisplayText(updatedName); //Makes sure the name updates in real time in the text.

        });

    }

    void UpdateDisplayText(string name) //Used to update the display name text shown on screen.
    {
        if (displayNameText != null)
        {

            if (string.IsNullOrWhiteSpace(name))
                displayNameText.text = "Display Name: (not set)";
            else
                displayNameText.text = "Display Name: " + name;

        }
    }

}