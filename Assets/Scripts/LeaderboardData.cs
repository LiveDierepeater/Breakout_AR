using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LeaderboardData
{
    [Serializable]
    public class LeaderboardDataEntry
    {
        public string name;
        public int data;
        
        public LeaderboardDataEntry(string name, int data)
        {
            this.name = name;
            this.data = data;
        }
    }

    public List<LeaderboardDataEntry> LeaderboardDataEntries = new List<LeaderboardDataEntry>();

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static LeaderboardData FromJson(string json)
    {
        return JsonUtility.FromJson<LeaderboardData>(json);
    }
}
