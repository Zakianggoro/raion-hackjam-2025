using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    int currentScore = 0;
    int highScore = 0;

    [Header("Game Over Settings")]
    [SerializeField] GameObject gameOverOverlay;
    [SerializeField] GameObject highScoreStick;
    [SerializeField] TextMeshProUGUI lastScore;
    [SerializeField] TextMeshProUGUI hiScore;

    [Space]

    [Header("In Game Text")]
    [SerializeField] TextMeshProUGUI textScore;

    [Space]

    [Header("Etc")]
    [SerializeField] int addPoint;
    [SerializeField] HealthBar barHealth;

    bool isOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "" + currentScore;

        if (barHealth.GetLife() == 0 && (!isOver))
        {
            isOver = true;
            Time.timeScale = 0f;
            gameOverOverlay.SetActive(true);
            lastScore.text = "Your Score: \n" + currentScore.ToString();
            hiScore.text = "Highscore: \n" + highScore.ToString();
            if (currentScore > highScore)
            {
                highScoreStick.SetActive(true);
            }
        }
    }

    public void AddPoint()
    {
        currentScore += addPoint;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void ReduceLife()
    {
        barHealth.MinLife();
        Debug.Log("Nyawa sisa: " + barHealth.GetLife());
    }
}
