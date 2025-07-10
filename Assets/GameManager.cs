using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pipe;
    public float timer = 2;
    float time = 0;
    public int dist = 3;
    int score = 0;
    public Text text;
    public GameObject gameOver;
    bool isEnded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timer && !isEnded) {
            Instantiate(pipe, transform.position + new Vector3(0,Random.Range(-dist,dist),0), Quaternion.identity);
            time = 0;
        }
        
    }
    public void UpdateText() {
        score++;
        text.text = score.ToString();
    }
    public void EndGame() { 
        gameOver.SetActive(true);
        isEnded = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
