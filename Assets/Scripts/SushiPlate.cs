using UnityEngine;

public class SushiPlate : MonoBehaviour
{
    [Header("Plate Sprites")]
    public GameObject riceSprite;      // Rice sprite child object
    public GameObject salmonSprite;    // Salmon topping sprite child object  
    public GameObject cucumberSprite;  // Cucumber topping sprite child object
    public GameObject shrimpSprite;    // Shrimp topping sprite child object
    public GameObject avacodSprite;    // Avocado topping sprite child object
    public GameObject unaginSprite;    // Unagi topping sprite child object
    public GameObject crabstickSprite; // Crabstick topping sprite child object
    
    [Header("Plate State")]
    public bool hasRice = false;
    public ToppingType currentTopping = ToppingType.None;
    
    void Start()
    {
        // Hide all sprites at start
        SetAllSpritesInactive();
    }
    
    void SetAllSpritesInactive()
    {
        if (riceSprite) riceSprite.SetActive(false);
        if (salmonSprite) salmonSprite.SetActive(false);
        if (cucumberSprite) cucumberSprite.SetActive(false);
        if (shrimpSprite) shrimpSprite.SetActive(false);
        if (avacodSprite) avacodSprite.SetActive(false);
        if (unaginSprite) unaginSprite.SetActive(false);
        if (crabstickSprite) crabstickSprite.SetActive(false);
    }
    
    public void AddRice()
    {
        if (!hasRice)
        {
            hasRice = true;
            if (riceSprite) riceSprite.SetActive(true);
            Debug.Log("Rice sprite activated on plate");
        }
    }
    
    public void AddTopping(ToppingType toppingType)
    {
        Debug.Log($"AddTopping() called with {toppingType}");
        
        if (!hasRice)
        {
            Debug.Log("Need rice first before adding topping!");
            return;
        }
        
        if (currentTopping != ToppingType.None)
        {
            Debug.Log("Plate already has a topping!");
            return;
        }
        
        currentTopping = toppingType;
        
        // Activate the appropriate topping sprite
        switch (toppingType)
        {
            case ToppingType.Salmon:
                Debug.Log($"Activating salmon sprite. Reference: {(salmonSprite != null ? "Found" : "NULL")}");
                if (salmonSprite != null)
                {
                    salmonSprite.SetActive(true);
                    Debug.Log($"Salmon sprite activated. Active: {salmonSprite.activeSelf}");
                }
                break;
            case ToppingType.Cucumber:
                Debug.Log($"Activating cucumber sprite. Reference: {(cucumberSprite != null ? "Found" : "NULL")}");
                if (cucumberSprite != null)
                {
                    cucumberSprite.SetActive(true);
                    Debug.Log($"Cucumber sprite activated. Active: {cucumberSprite.activeSelf}");
                }
                break;
            case ToppingType.Shrimp:
                Debug.Log($"Activating shrimp sprite. Reference: {(shrimpSprite != null ? "Found" : "NULL")}");
                if (shrimpSprite != null)
                {
                    shrimpSprite.SetActive(true);
                    Debug.Log($"Shrimp sprite activated. Active: {shrimpSprite.activeSelf}");
                }
                break;
            case ToppingType.Avocado:
                Debug.Log($"Activating avocado sprite. Reference: {(avacodSprite != null ? "Found" : "NULL")}");
                if (avacodSprite != null)
                {
                    avacodSprite.SetActive(true);
                    Debug.Log($"Avocado sprite activated. Active: {avacodSprite.activeSelf}");
                }
                break;
            case ToppingType.Unagi:
                Debug.Log($"Activating unagi sprite. Reference: {(unaginSprite != null ? "Found" : "NULL")}");
                if (unaginSprite != null)
                {
                    unaginSprite.SetActive(true);
                    Debug.Log($"Unagi sprite activated. Active: {unaginSprite.activeSelf}");
                }
                break;
            case ToppingType.Crabstick:
                Debug.Log($"Activating crabstick sprite. Reference: {(crabstickSprite != null ? "Found" : "NULL")}");
                if (crabstickSprite != null)
                {
                    crabstickSprite.SetActive(true);
                    Debug.Log($"Crabstick sprite activated. Active: {crabstickSprite.activeSelf}");
                }
                break;
        }
        
        Debug.Log($"{toppingType} processing complete");
    }
    
    public void ResetPlate()
    {
        hasRice = false;
        currentTopping = ToppingType.None;
        SetAllSpritesInactive();
        Debug.Log("Plate reset");
    }
}

public enum ToppingType
{
    None,
    Salmon,
    Cucumber, 
    Shrimp,
    Unagi,
    Crabstick,
    Avocado
}