using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const string LeaderboardDataPlayerPrefsKey = "LeaderboardData";
    public LeaderboardData leaderboardData;

    private ScoreUI scoreUI;

    private void Awake()
    {
        // load leaderboard
        string json = PlayerPrefs.GetString(LeaderboardDataPlayerPrefsKey, null);

        if (string.IsNullOrEmpty(json))
        {
            leaderboardData = new LeaderboardData();
        }
        else
        {
            leaderboardData = LeaderboardData.FromJson(json);
        }
        
        scoreUI = GameObject.Find("ScoreUI").GetComponentInChildren<ScoreUI>();
    }

    private void OnApplicationQuit()
    {
        // store leaderboard
        string json = leaderboardData.ToJson();
        PlayerPrefs.SetString(LeaderboardDataPlayerPrefsKey, json);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            leaderboardData.LeaderboardDataEntries.Add(new LeaderboardData.LeaderboardDataEntry("Utz", scoreUI.GetCurrentScore()));
        }
        
        // TODO: Later when player dies an entry should be added.
    }
}
