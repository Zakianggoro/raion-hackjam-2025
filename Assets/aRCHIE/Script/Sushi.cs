using UnityEngine;
using UnityEngine.UIElements;

public class Sushi : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] string sushiName;
    [SerializeField] SpriteRenderer spritePlatform;
    [SerializeField] float enlargeValue;
    [SerializeField] string specialIngredient;

    bool isDrag = false;
    Vector2 startPos;
    Vector3 startSize;

    void Start()
    {
        startPos = transform.position;
        startSize = transform.localScale;
    }

    void Update()
    {
        if (isDrag)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseDown()
    {
        isDrag = true;
        Debug.Log("Mouse clicked down and holding");
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse releasing");
        isDrag = false;
        transform.position = startPos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FoodContainer"))
        {
            transform.localScale = new Vector3(transform.localScale.x + enlargeValue, transform.localScale.y + enlargeValue, transform.localScale.z + enlargeValue);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FoodContainer"))
        {
            transform.localScale = startSize;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
