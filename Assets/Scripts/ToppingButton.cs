using UnityEngine;
using UnityEngine.UI;

public class ToppingButton : MonoBehaviour
{
    [Header("Topping Settings")]
    public ToppingType toppingType;
    public GameObject toppingPrefab;  // The draggable topping prefab
    public Transform spawnPoint;      // Where topping appears on cutting board
    
    [Header("UI References")]
    public GameObject sliderUI;       // The cutting slider UI
    public Slider cuttingSlider;      // The actual slider component
    public Button thisButton;         // This button component
    
    private GameObject currentTopping; // Current topping being cut
    private bool isSliderActive = false;
    
    // Static variables to track global cutting state
    private static bool isAnyCuttingActive = false;
    private static ToppingButton activeCuttingButton = null;
    
    void Start()
    {
        // Hide slider at start
        if (sliderUI) sliderUI.SetActive(false);
        
        // Set up button click
        if (thisButton) thisButton.onClick.AddListener(SpawnTopping);
        
        // Set up slider
        if (cuttingSlider)
        {
            cuttingSlider.minValue = 0f;
            cuttingSlider.maxValue = 100f;
            cuttingSlider.value = 0f;
            cuttingSlider.onValueChanged.AddListener(OnSliderChange);
        }
    }
    
    void Update()
    {
        // Update button state based on global cutting status
        if (thisButton)
        {
            thisButton.interactable = !isAnyCuttingActive;
        }
        
        // Check if currentTopping was destroyed externally (safety check)
        if (currentTopping == null && activeCuttingButton == this)
        {
            ResetButton();
        }
    }
    
    public void SpawnTopping()
    {
        // Don't spawn if any cutting is active
        if (isAnyCuttingActive) return;
        
        // Spawn uncut topping
        currentTopping = Instantiate(toppingPrefab, spawnPoint.position, Quaternion.identity);
        
        // Set it as uncut and non-draggable initially
        Topping toppingScript = currentTopping.GetComponent<Topping>();
        if (toppingScript)
        {
            toppingScript.SetToppingType(toppingType);
            toppingScript.SetCuttable(false); // Can't drag yet
        }
        
        // Show slider UI
        if (sliderUI) sliderUI.SetActive(true);
        if (cuttingSlider) cuttingSlider.value = 0f;
        isSliderActive = true;
        
        // SET GLOBAL CUTTING STATE - This will disable ALL buttons
        isAnyCuttingActive = true;
        activeCuttingButton = this;
        
        Debug.Log($"{toppingType} spawned on cutting board - ALL buttons disabled until cutting is complete!");
    }
    
    void OnSliderChange(float value)
    {
        if (currentTopping == null) return;
        
        // When slider reaches 100, topping is cut
        if (value >= 100f)
        {
            Topping toppingScript = currentTopping.GetComponent<Topping>();
            if (toppingScript)
            {
                toppingScript.CutTopping(); // Change to cut sprite
                toppingScript.SetCuttable(true); // Now can be dragged
            }
            
            // Hide slider
            if (sliderUI) sliderUI.SetActive(false);
            isSliderActive = false;
            
            // CLEAR GLOBAL CUTTING STATE - This will re-enable ALL buttons
            isAnyCuttingActive = false;
            activeCuttingButton = null;
            
            Debug.Log($"{toppingType} is now cut and draggable! All buttons re-enabled.");
        }
    }
    
    // Reset if topping is destroyed or used
    public void ResetButton()
    {
        // Only reset if this is the active cutting button
        if (activeCuttingButton == this)
        {
            currentTopping = null;
            isSliderActive = false;
            if (sliderUI) sliderUI.SetActive(false);
            if (cuttingSlider) cuttingSlider.value = 0f;
            
            // CLEAR GLOBAL CUTTING STATE - This will re-enable ALL buttons
            isAnyCuttingActive = false;
            activeCuttingButton = null;
            
            Debug.Log($"{toppingType} button reset - All buttons re-enabled!");
        }
    }
}