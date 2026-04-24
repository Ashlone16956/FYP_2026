using UnityEngine;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
public class PlayFabStats : MonoBehaviour
{

    public static PlayFabStats Instance;

    private const string HighestWaveStatName = "Highest Waves";
    private const string LocalBestWaveKey = "LocalBestWave";

    public int LocalBestWave;

    void Awake()
    {

        if (Instance == null)
        {

            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLocalBestWave();

        }
        else
        {

            Destroy(gameObject);

        }
    }


    void LoadLocalBestWave()
    {

        LocalBestWave = PlayerPrefs.GetInt(LocalBestWaveKey, 0); //Loads the saved best wave achieved

    }

    void SaveLocalBestWave()
    {

        PlayerPrefs.SetInt(LocalBestWaveKey, LocalBestWave);
        PlayerPrefs.Save();

    }

    public void SubmitHighestWave(int reachedWave)
    {

        if (reachedWave <= LocalBestWave) //Makes sure the run is only submitted it its the highest achieved wave.
        {

            Debug.Log("Wave not higher than local best. No submission needed.");
            return;

        }

        LocalBestWave = reachedWave;
        SaveLocalBestWave();

        if (PlayFabLogin.Instance == null || !PlayFabLogin.Instance.IsLoggedIn)
        {

            Debug.LogWarning("Cannot submit highest wave before PlayFab login completes.");
            return;

        }

        var request = new UpdatePlayerStatisticsRequest
        {

            Statistics = new List<StatisticUpdate> //Used for the leaderboard.
            {

                new StatisticUpdate
                {

                    StatisticName = HighestWaveStatName,
                    Value = reachedWave

                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(
            request,
            result =>

            {

                Debug.Log("Highest Waves submitted successfully: " + reachedWave);

            },
            error =>
            {

                Debug.LogError("Failed to submit Highest Waves: " + error.GenerateErrorReport());

            }
        );
    }

    public void GetTopLeaderboard(int maxResults = 10)
    {
        if (PlayFabLogin.Instance == null || !PlayFabLogin.Instance.IsLoggedIn)
        {

            Debug.LogWarning("Cannot fetch leaderboard before PlayFab login completes.");
            return;

        }

        var request = new GetLeaderboardRequest
        {

            StatisticName = HighestWaveStatName,
            StartPosition = 0,
            MaxResultsCount = maxResults,
            ProfileConstraints = new PlayerProfileViewConstraints
            {

                ShowDisplayName = true

            }
        };

        PlayFabClientAPI.GetLeaderboard(
            request,
            result =>
            {

                Debug.Log("Leaderboard fetched successfully.");

                foreach (var entry in result.Leaderboard)
                {

                    string name = string.IsNullOrWhiteSpace(entry.DisplayName)
                        ? entry.PlayFabId
                        : entry.DisplayName;

                    Debug.Log($"{entry.Position + 1}. {name} - Wave {entry.StatValue}");

                }
            },
            error =>
            {

                Debug.LogError("Failed to fetch leaderboard: " + error.GenerateErrorReport());

            }
        );

    }
}
