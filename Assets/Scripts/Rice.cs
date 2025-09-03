using UnityEngine;
using System.Collections;

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
        CheckDropLocation();
    }
    
    void CheckDropLocation()
    {
        Collider2D hit = Physics2D.OverlapPoint(transform.position);
        if (hit != null)
        {
            // Check if dropped on plate
            if (hit.CompareTag("Plate"))
            {
                transform.position = hit.transform.position;
                transform.SetParent(hit.transform);
                GetComponent<Rice>().enabled = false; // Lock in place
                Debug.Log("Rice placed on plate");
            }
            // Check if dropped on rolling mat with seaweed
            else if (hit.CompareTag("RollingMat"))
            {
                RollingMat mat = hit.GetComponent<RollingMat>();
                if (mat.hasSeaweed)
                {
                    mat.AddRice();
                    Destroy(gameObject);
                    Debug.Log("Rice added to maki");
                }
            }
        }
    }
}