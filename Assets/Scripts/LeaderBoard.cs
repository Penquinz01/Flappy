using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using Unity.Services.Leaderboards;
using Unity.Services.Core;
using Unity.Services.Leaderboards.Models;

public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard instance;
    private const string LEADERBOARD_ID = "dhruva_flappy";
    async void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async Task ResetLogin()
    {
        AuthenticationService.Instance.SignOut();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    public async Task ChangeName(string newName) => await AuthenticationService.Instance.UpdatePlayerNameAsync(newName);

    public async Task AddtoLeaderboard(int score,string username,string batch)
    {
        var data = new GameManager.ScoreMetaData { username = username,batch = batch,score = score };
        var ScoreResponse =
            await LeaderboardsService.Instance.AddPlayerScoreAsync(LEADERBOARD_ID, score,
                new AddPlayerScoreOptions { Metadata = data });
    }

    public async Task GetScores(Transform leaderboardContentParent,GameObject leaderBoardContent)
    {

        var page = await LeaderboardsService.Instance.GetScoresAsync(LEADERBOARD_ID);
        foreach(Transform t in  leaderboardContentParent)
        {
            Destroy(t.gameObject);
        }

        foreach (var entry in page.Results)
        {   
            GameObject newObject = Instantiate(leaderBoardContent, leaderboardContentParent);
            newObject.SetActive(true);
            LeaderBoardContent newContent = newObject.GetComponent<LeaderBoardContent>();
            newContent.Initialize(entry);
        }
    }
}
