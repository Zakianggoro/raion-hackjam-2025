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
        if (life > 0)
        {
            life--;
            heartImage[life].sprite = deathHeart;
        }
    }

    public int GetLife()
    {
        return life;
    }
}
