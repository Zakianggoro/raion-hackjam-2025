using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutor : MonoBehaviour
{
    public Image background;
    public Sprite startImage;
    public Sprite nextImage;
    int countPage = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startImage = background.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (countPage > 0)
        {
            countPage = 1;
            background.sprite = nextImage;
        }
        else if (countPage == 0)
        {
            background.sprite = startImage;
        }
        else if (countPage < 0)
        {
            SceneManager.LoadScene("Home");
        }
    }

    public void NextImage()
    {
        countPage++;
    }

    public void BeforeImage()
    {
        countPage--;
    }
}
