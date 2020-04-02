using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //Singleton----------------------------------------------------

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    //-------------------------------------------------------------

    //Configuration Parameters
    [SerializeField] TextMeshProUGUI scoreText = null;

    //Cache Variables
    int player1Score = 0;
    int player2Score = 0;

    string winner = "";


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SplashLoad());
    }

    public void IncreaseP1Score()
    {
        player1Score++;
        UpdateScore();

        if (player1Score == 5)
        {
            winner = "PLAYER 1";
            LoadNextScene();
        }
    }

    public void IncreaseP2Score()
    {
        player2Score++;
        UpdateScore();

        if (player2Score == 5)
        {
            winner = "PLAYER 2";
            LoadNextScene();
        }
    }

    void UpdateScore()
    {
        if (SceneManager.GetActiveScene().name == "Main") {
            scoreText = FindObjectOfType<TextMeshProUGUI>();
            scoreText.text = player1Score + " | " + player2Score;

        }

    }

    void UpdateWinner()
    {
        if (SceneManager.GetActiveScene().name == "End")
        {
            scoreText = FindObjectOfType<TextMeshProUGUI>();
            scoreText.text = winner + " WINS";
        }

    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void StartGame()
    {
        player1Score = 0;
        player2Score = 0;
        winner = "";
        SceneManager.LoadScene("Main");
    }

    public void SetCanvas(TextMeshProUGUI text)
    {
        scoreText = text;
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 3)
        {
            UpdateWinner();
            SoundManager.Instance.PlayClapping();
            FindObjectOfType<Button>().onClick.AddListener(StartGame);
        }

        if(level == 1)
        {
            FindObjectOfType<Button>().onClick.AddListener(StartGame);
        }
    }

    IEnumerator SplashLoad()
    {
        yield return new WaitForSeconds(4);
        LoadNextScene();
    }
}
