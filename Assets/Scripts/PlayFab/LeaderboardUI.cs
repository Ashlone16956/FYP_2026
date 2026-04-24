using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ProgressionModels;

public class LeaderboardUI : MonoBehaviour
{
    public TextMeshProUGUI[] leaderboardSlots;

    void Start()
    {

        ClearSlots();
        

    }

    public void RefreshLeaderboard()
    {

        if (PlayFabLogin.Instance == null || !PlayFabLogin.Instance.IsLoggedIn)
        {

            Debug.LogWarning("Cannot fetch leaderboard before Playfab login is complete.");
            return;

        }

        var request = new GetLeaderboardRequest
        {

            StatisticName = "Highest Waves", //uses the highest waves leaderboard for the data to display.
            StartPosition = 0,
            MaxResultsCount = leaderboardSlots.Length,
            ProfileConstraints = new PlayerProfileViewConstraints
            {

                ShowDisplayName = true

            }

        };

        PlayFabClientAPI.GetLeaderboard(

            request, result =>
            {

                ClearSlots();

                for (int i = 0; i < result.Leaderboard.Count && i < leaderboardSlots.Length; i++) //iterates for every leaderboard slot.
                {

                    var entry = result.Leaderboard[i];

                    string playerName = string.IsNullOrWhiteSpace(entry.DisplayName) //uses the display name for their leaderboard slot.
                        ? "Anonymous"
                        : entry.DisplayName;

                    leaderboardSlots[i].text = $"{entry.Position + 1}. {playerName} - Wave {entry.StatValue}";

                }

            },
            error =>
            {

                Debug.LogError("Failed to fetch leaderboard: " + error.GenerateErrorReport());

            }

        );

    }

    void ClearSlots()
    {

        for (int i = 0; i < leaderboardSlots.Length; i++)
        {

            if (leaderboardSlots[i] != null)
            {

                leaderboardSlots[i].text = $"{i + 1}. ---";

            }

        }

    }

    void OnEnable()
    {

        RefreshLeaderboard();

    }
}
