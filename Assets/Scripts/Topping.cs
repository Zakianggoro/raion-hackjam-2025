using UnityEngine;

public class Topping : MonoBehaviour
{
    [Header("Topping Settings")]
    public Sprite uncutSprite;
    public Sprite cutSprite;
    
    private ToppingType toppingType;
    private bool isCut = false;
    private bool canDrag = false;
    private SpriteRenderer spriteRenderer;
    
    // Dragging variables
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    public MusicManager musicManager;
    
    void Start()
    {
        musicManager = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Start with uncut sprite
        if (uncutSprite) spriteRenderer.sprite = uncutSprite;
    }
    
    public void SetToppingType(ToppingType type)
    {
        toppingType = type;
    }
    
    public void SetCuttable(bool cuttable)
    {
        canDrag = cuttable;
    }
    
    public void CutTopping()
{
    if (!isCut)
    {
        isCut = true;

        if (cutSprite)
        {
            musicManager.PlayCutSound();
            Vector3 oldSize = spriteRenderer.bounds.size;
            spriteRenderer.sprite = cutSprite;
            Vector3 newSize = spriteRenderer.bounds.size;

            if (newSize.x > 0 && newSize.y > 0)
            {
                Vector3 scale = transform.localScale;
                scale.x *= oldSize.x / newSize.x;
                scale.y *= oldSize.y / newSize.y;
                transform.localScale = scale;
            }
        }

        Debug.Log($"{toppingType} has been cut and sprite changed");
    }
}

    
    void OnMouseDown()
    {
        // Only allow dragging if cut and can drag
        if (isCut && canDrag)
        {
            isDragging = true;
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            offset = transform.position - mousePos;
            Debug.Log($"Started dragging cut {toppingType}");
        }
        else if (!isCut)
        {
            Debug.Log("Topping needs to be cut first!");
        }
        else if (!canDrag)
        {
            Debug.Log("Topping is not ready for dragging yet!");
        }
    }
    
    void OnMouseDrag()
    {
        if (isDragging && isCut && canDrag)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos + offset;
        }
    }
    
    void OnMouseUp()
    {
        isDragging = false;
        if (isCut && canDrag)
        {
            Debug.Log($"Topping dropped at position {transform.position}");
        }
    }
    
    // When topping enters the plate trigger - FIXED: Removed !isDragging condition
    void OnTriggerEnter2D(Collider2D other)
    {
        // Only trigger when topping is cut and can be dragged
        if (isCut && canDrag)
        {
            Debug.Log($"Topping entered trigger: {other.gameObject.name} with tag: {other.tag}");
            
            if (other.CompareTag("Plate"))
            {
                SushiPlate plate = other.GetComponent<SushiPlate>();
                if (plate != null)
                {
                    Debug.Log($"Adding {toppingType} to plate and destroying prefab");
                    plate.AddTopping(toppingType);
                    
                    // Find and reset the button that created this topping
                    ToppingButton[] buttons = FindObjectsOfType<ToppingButton>();
                    foreach(ToppingButton button in buttons)
                    {
                        if (button.toppingType == toppingType)
                        {
                            button.ResetButton();
                            break;
                        }
                    }
                    
                    Destroy(gameObject);
                }
            }
        }
    }
}