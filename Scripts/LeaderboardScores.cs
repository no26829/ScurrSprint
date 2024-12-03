using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class LeaderboardScores : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inputScore;
    [SerializeField]
    private TMP_InputField inputName;

    public ScoreCounter scoreCounter;    // For keeping track of score
    public UnityEvent<string, int> submitScoreEvent;

    private void Start()
    {
        inputScore.text = ScoreCounter.score.ToString("#,0");
    }
    public void SubmitScore()
    {
        submitScoreEvent.Invoke(inputName.text, ScoreCounter.score);
    }
}
