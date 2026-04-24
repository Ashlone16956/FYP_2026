using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabLogin : MonoBehaviour
{
    public static PlayFabLogin Instance;

    private const string CustomIdKey = "PlayFabCustomId";
    private const string DisplayNameKey = "PlayFabDisplayName";

    public string CustomId;
    public string DisplayName;
    public bool IsLoggedIn;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

    }

    void Start()
    {

        Login();

    }

    public void Login()
    {
        CustomId = GetOrCreateCustomId();

        var request = new LoginWithCustomIDRequest
        {
            CustomId = CustomId,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    string GetOrCreateCustomId() //The custom id for the leaderboard.
    {
        if (PlayerPrefs.HasKey(CustomIdKey)) //Checks id the player already has an id.
        {

            return PlayerPrefs.GetString(CustomIdKey);

        }

        string newId = Guid.NewGuid().ToString(); //Generates a random id.
        PlayerPrefs.SetString(CustomIdKey, newId); //Saves the id locally so it wont change every loadup.
        PlayerPrefs.Save();

        Debug.Log("Created new Custom ID: " + newId);
        return newId;

    }

    public string GetSavedDisplayName()
    {

        return PlayerPrefs.GetString(DisplayNameKey, "");

    }

    void OnLoginSuccess(LoginResult result)
    {

        IsLoggedIn = true;

        Debug.Log("PlayFab login successful.");

    }

    void OnLoginFailure(PlayFabError error)
    {

        Debug.LogError("PlayFab login failed: " + error.GenerateErrorReport());

    }

    public void SetDisplayName(string newDisplayName, System.Action<string> onSuccess = null)
    {
        if (!IsLoggedIn)
        {
            Debug.LogWarning("Cannot set display name before PlayFab login completes.");
            return;
        }

        if (string.IsNullOrWhiteSpace(newDisplayName))
        {

            Debug.LogWarning("Display name cannot be empty.");
            return;

        }

        var request = new UpdateUserTitleDisplayNameRequest
        {

            DisplayName = newDisplayName.Trim()

        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(
            request,
            result =>
            {

                DisplayName = result.DisplayName;
                PlayerPrefs.SetString(DisplayNameKey, DisplayName);
                PlayerPrefs.Save();

                Debug.Log("Display name updated to: " + DisplayName);

                onSuccess?.Invoke(DisplayName);
            },
            error =>
            {
                Debug.LogError("Failed to update display name: " + error.GenerateErrorReport());
            }
        );
    }
}