using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField] string playScene;
    [SerializeField] string tutorScene;
    [SerializeField] string menuScene;
    [SerializeField] string settingScene;

    [Space]

    [SerializeField] MusicManager manager;
    public GameObject pauseOverlay;
    public Button pauseButton;

    public void ToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void ToPlay()
    {
        SceneManager.LoadScene(playScene);
    }

    public void ToSetting()
    {
        SceneManager.LoadScene(settingScene);
    }

    public void ToTutorial()
    {
        SceneManager.LoadScene(tutorScene);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        manager.playMainBGM();
        Time.timeScale = 1f;
        pauseButton.enabled = true;
        pauseOverlay.SetActive(false);
    }
}
