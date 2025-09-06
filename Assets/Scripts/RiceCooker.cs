using UnityEngine;
using System.Collections;

public class RiceCooker : MonoBehaviour
{
    [Header("Rice Creation")]
    public GameObject ricePrefab;
    public Transform riceSpawnPoint;
    
    private int tapCount = 0;
    private bool canUse = true;
    bool isPlayingSound = false;
    [SerializeField] MusicManager mManager;
    
    void OnMouseDown()
    {
        if (canUse)
        {
            tapCount++;
            if (!isPlayingSound)
            {
                isPlayingSound = true;
                mManager.PlayRiceSound();
                isPlayingSound = false;
            }
            Debug.Log($"Rice Cooker tapped: {tapCount}/5");
            
            if (tapCount >= 5)
            {
                CreateRice();
                ResetCooker();
            }
        }
    }
    
    void CreateRice()
    {
        GameObject rice = Instantiate(ricePrefab, riceSpawnPoint.position, Quaternion.identity);
        Debug.Log("Rice created!");
    }
    
    void ResetCooker()
    {
        tapCount = 0;
        canUse = false;
        StartCoroutine(CooldownTimer());
    }
    
    IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(1f);
        canUse = true;
    }
}