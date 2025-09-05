using UnityEngine;

public class Rice : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    
    void Start()
    {
        mainCamera = Camera.main;
    }
    
    void OnMouseDown()
    {
        isDragging = true;
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        offset = transform.position - mousePos;
        Debug.Log("Started dragging rice");
    }
    
    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos + offset;
        }
    }
    
    void OnMouseUp()
    {
        isDragging = false;
        Debug.Log($"Rice dropped at position {transform.position}");
    }
    
    // When rice enters the plate trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Rice entered trigger: {other.gameObject.name} with tag: {other.tag}");
        
        if (other.CompareTag("Plate"))
        {
            SushiPlate plate = other.GetComponent<SushiPlate>();
            if (plate != null)
            {
                Debug.Log("Adding rice to plate and destroying prefab");
                plate.AddRice();
                Destroy(gameObject);
            }
        }
    }
}