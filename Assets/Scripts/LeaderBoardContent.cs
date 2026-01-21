using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class LeaderBoardContent : MonoBehaviour
{
    [SerializeField] private TMP_Text username;
    [SerializeField] private TMP_Text score;
    private LeaderboardEntry entry;
    public void Initialize(LeaderboardEntry entry)
    {
        this.entry = entry;
        username.text = entry.PlayerName;
        score.text = entry.Score.ToString();
    }
}
