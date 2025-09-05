using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    int score;

    [SerializeField] TextMeshProUGUI textScore;
    [SerializeField] int addPoint;
    [SerializeField] HealthBar barHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "" + score;
    }

    public void AddPoint()
    {
        score += addPoint;
    }

    public void ReduceLife()
    {
        barHealth.MinLife();
        Debug.Log("Nyawa sisa: " + barHealth.GetLife());
    }
}
