using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const string LeaderboardDataPlayerPrefsKey = "LeaderboardData";
    public LeaderboardData leaderboardData;

    private ScoreUI scoreUI;

    private TextMeshProUGUI leaderName;
    private TextMeshProUGUI leaderScore;
    
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
        
        scoreUI = GameObject.Find("Score").GetComponentInChildren<ScoreUI>();

        leaderName = transform.Find("Leader").GetComponent<TextMeshProUGUI>();
        leaderScore = transform.Find("Highscore").GetComponent<TextMeshProUGUI>();
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
            UpdateLeaderboard();
        }
        
        // TODO: Later when player dies an entry should be added.
    }

    private LeaderboardData.LeaderboardDataEntry GetHighestScorer()
    {
        int currentHighscore = 0;

        LeaderboardData.LeaderboardDataEntry Highscorer = null;

        foreach (LeaderboardData.LeaderboardDataEntry data in leaderboardData.LeaderboardDataEntries)
        {
            if (data.score > currentHighscore)
            {
                currentHighscore = data.score;
                Highscorer = data;
            }
        }
        
        return Highscorer;
    }

    private void UpdateLeaderboard()
    {
        leaderName.text = GetHighestScorer().name;
        leaderScore.text = GetHighestScorer().score.ToString();
    }
}
