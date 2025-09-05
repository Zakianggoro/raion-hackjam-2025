using UnityEngine;

public class UnagiSauce : MonoBehaviour
{
    Vector2 startPos;
    bool isDrag = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isDrag)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        isDrag = true;
    }

    void OnMouseUp()
    {
        isDrag = false;
        transform.position = startPos;
    }
}
