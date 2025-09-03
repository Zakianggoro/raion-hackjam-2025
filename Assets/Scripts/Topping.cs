using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Topping : MonoBehaviour
{
    [Header("Topping Settings")]
    public ToppingType toppingType;
    public Sprite uncutSprite;
    public Sprite cutSprite;
    public bool isCut = false;

    private SpriteRenderer spriteRenderer;
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = uncutSprite;
    }

    void OnMouseDown()
    {
        if (isCut) // Only draggable when cut
        {
            isDragging = true;
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            offset = transform.position - mousePos;
        }
    }

    void OnMouseDrag()
    {
        if (isDragging && isCut)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos + offset;
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            CheckDropLocation();
        }
    }

    void CheckDropLocation()
    {
        if (!isCut) return;

        Collider2D hit = Physics2D.OverlapPoint(transform.position);
        if (hit != null)
        {
            // Check if dropped on rice (for nigiri)
            if (hit.CompareTag("Rice"))
            {
                CreateNigiri(hit.gameObject);
            }
            // Check if dropped on rolling mat for maki
            else if (hit.CompareTag("RollingMat"))
            {
                RollingMat mat = hit.GetComponent<RollingMat>();
                if (mat.hasSeaweed && mat.hasRice)
                {
                    mat.AddTopping(toppingType);
                    Destroy(gameObject);
                    Debug.Log($"{toppingType} added to maki");
                }
            }
        }
    }

    void CreateNigiri(GameObject riceObj)
    {
        // Create nigiri by adding topping sprite on top of rice
        GameObject toppingOnRice = new GameObject($"Nigiri_{toppingType}");
        toppingOnRice.transform.position = riceObj.transform.position + Vector3.up * 0.2f;
        toppingOnRice.transform.SetParent(riceObj.transform);

        SpriteRenderer toppingSR = toppingOnRice.AddComponent<SpriteRenderer>();
        toppingSR.sprite = cutSprite;
        toppingSR.sortingOrder = 1;

        // Add completed sushi component to rice
        CompletedSushi completedSushi = riceObj.AddComponent<CompletedSushi>();
        completedSushi.sushiType = SushiType.Nigiri;
        completedSushi.topping = toppingType;

        Destroy(gameObject); // Remove the dragged topping
        Debug.Log($"Nigiri with {toppingType} created!");
    }

    // Called by knife when cutting
    public void CutTopping()
    {
        if (!isCut)
        {
            isCut = true;
            spriteRenderer.sprite = cutSprite;
            Debug.Log($"{toppingType} has been cut");
        }
    }
}
