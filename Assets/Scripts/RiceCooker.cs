using UnityEngine;
using System.Collections;

public class RiceCooker : MonoBehaviour
{
    [Header("Rice Creation")]
    public GameObject ricePrefab;
    public Transform riceSpawnPoint;
    
    private int tapCount = 0;
    private bool canUse = true;
    
    void OnMouseDown()
    {
        if (canUse)
        {
            tapCount++;
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