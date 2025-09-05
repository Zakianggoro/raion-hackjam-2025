using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [Header("Pause Overlay")]
    [SerializeField] GameObject pauseOverlay;
    [SerializeField] Button pauseButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseOverlay.SetActive(false);
    }

    public void PauseClicked()
    {
        pauseOverlay.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.enabled = false;
    }
}
