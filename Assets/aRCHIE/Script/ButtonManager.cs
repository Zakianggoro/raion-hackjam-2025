using UnityEngine;
using UnityEngine.SceneManagement;

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
}
