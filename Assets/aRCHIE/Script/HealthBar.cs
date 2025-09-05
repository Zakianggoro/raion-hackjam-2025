using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    int life = 5;
    [SerializeField] Sprite deathHeart;
    [SerializeField] Image[] heartImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void MinLife()
    {
        life--;
        heartImage[life].sprite = deathHeart;
        if (life == 0)
        {
            Time.timeScale = 0f;
        }
    }

    public int GetLife()
    {
        return life;
    }
}
