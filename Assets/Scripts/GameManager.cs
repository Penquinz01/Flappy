using System;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private string username;
    private string batch;
    public GameObject pipe;
    public float timer = 2;
    float time = 0;
    public int dist = 3;
    int score = 0;
    public TMP_Text text;
    public GameObject gameOver;
    public  bool started { get; private set; } = false;
    private bool isEndingGame = false;
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private Transform leaderBoardContentParent;
    [SerializeField] private GameObject leaderBoardContent;

    [SerializeField]private TMP_InputField nameField;
    [SerializeField]private TMP_InputField batchField;
    
    [Serializable]
    public struct ScoreMetaData
    {
        public string username;
        public string batch;
        public int score;
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        time = 0;
        instance = this;
    }

    private void Start()
    {
        _startScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timer  &&started)
        {
            Instantiate(pipe, transform.position + new Vector3(0,Random.Range(-dist,dist),0), Quaternion.identity);
            time = 0;
        }
        
    }
    public void UpdateText() {
        score++;
        text.text = score.ToString();
    }
    public async void EndGame() {
        if (isEndingGame) return; // Prevent multiple calls
        isEndingGame = true;
        
        gameOver.SetActive(true);
        
        try
        {
            await LeaderBoard.instance.AddtoLeaderboard(score, username, batch);
            
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error in EndGame: {e.Message}");
        }
        finally
        {
            isEndingGame = false;
        }
    }

    public void StartGame()
    {
        _startScreen.SetActive(false);
        username =nameField.text;
        batch = batchField.text;
        started = true;
    }
    public void Restart()
    {
        score = 0;
        gameOver.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartGame();
    }
}

