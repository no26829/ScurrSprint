using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.SocialPlatforms.Impl;
public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;


    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        Leaderboards.ScurrySprint.GetEntries(entries =>
        {
            //gets rid of error saying not enough names in leaderboard
            int loopLength = Mathf.Min(names.Count, entries.Length);
            //get names and scores
            for (int i = 0; 0 < loopLength; ++i)
            {
                names[i].text = entries[i].Username;
                scores[i].text = entries[i].Score.ToString();
            }
        });


           
        
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        Leaderboards.ScurrySprint.UploadNewEntry(username, score, isSuccessful =>
        {
            if (isSuccessful)
                GetLeaderboard();
        });

        Leaderboards.ScurrySprint.ResetPlayer();

    }
    
}
