using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelCompleted;
    public static bool isGameStarted;
    public static bool mute = false;

    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject gamePlayPanel;
    public GameObject startMenuPanel;
    public Slider gameProgressSlider;

    public static int currentLevelIndex;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public static int numberOfPassedRings;
    public static int score = 0; //static variables persist through levels

    //public GameObject players;

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameOver = false;
        levelCompleted = false;
        isGameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Update our UI
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();

        scoreText.text = score.ToString();
        highScoreText.text = "Best Score\n" + PlayerPrefs.GetInt("HighScore", 0);

        int progress = numberOfPassedRings * 100 / FindObjectOfType<HelixManager>().numberOfRings;
        gameProgressSlider.value = progress;

        //Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && 
        if (Input.GetMouseButtonDown(0) && !isGameStarted)
        {
            //Input.GetTouch(0).fingerId
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            isGameStarted = true;
            gamePlayPanel.SetActive(true);
            startMenuPanel.SetActive(false);
        }

        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

            if(score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
            score = 0;

            if(Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("Level");
            }
        }

        if (levelCompleted)
        {
            levelCompletedPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex + 1);
                SceneManager.LoadScene("Level");
            }
        }
    }

    /* public void SpawnMorePlayers() 
    {
     
    }*/
}
