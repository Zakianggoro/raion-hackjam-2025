using UnityEngine;

public class SushiPlate : MonoBehaviour
{
    [Header("Final Prefabs")]
    public GameObject salmonSushiPrefab;
    public GameObject cucumberSushiPrefab;
    public GameObject shrimpSushiPrefab;
    public GameObject avocadoSushiPrefab;
    public GameObject unagiSushiPrefab;
    public GameObject crabstickSushiPrefab;
    public GameObject noriSushiPrefab;

    [Space]

    [Header("Plate Sprites")]
    public GameObject riceSprite;        // Rice sprite child object
    public GameObject salmonSprite;      // Salmon topping sprite child object
    public GameObject cucumberSprite;    // Cucumber topping sprite child object
    public GameObject shrimpSprite;      // Shrimp topping sprite child object
    public GameObject avacodSprite;      // Avocado topping sprite child object
    public GameObject unaginSprite;      // Unagi topping sprite child object
    public GameObject crabstickSprite;   // Crabstick topping sprite child object
    public GameObject noriSprite;        // Nori topping sprite child object

    [Header("Plate State")]
    public bool hasRice = false;
    public ToppingType currentTopping = ToppingType.None;

    void Start()
    {
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
        if (noriSprite) noriSprite.SetActive(false);
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

        // Hide all sprites dulu
        SetAllSpritesInactive();

        // Pilih prefab yang sesuai
        GameObject prefabToSpawn = null;
        switch (toppingType)
        {
            case ToppingType.Salmon:    prefabToSpawn = salmonSushiPrefab; break;
            case ToppingType.Cucumber:  prefabToSpawn = cucumberSushiPrefab; break;
            case ToppingType.Shrimp:    prefabToSpawn = shrimpSushiPrefab; break;
            case ToppingType.Avocado:   prefabToSpawn = avocadoSushiPrefab; break;
            case ToppingType.Unagi:     prefabToSpawn = unagiSushiPrefab; break;
            case ToppingType.Crabstick: prefabToSpawn = crabstickSushiPrefab; break;
            case ToppingType.Nori:      prefabToSpawn = noriSushiPrefab; break;
        }

        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            Debug.Log($"Spawned final sushi prefab: {toppingType}");
        }
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
    Avocado,
    Nori
}
