using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Sushi : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] string sushiName;
    [SerializeField] SpriteRenderer spritePlatform;
    [SerializeField] float enlargeValue;
    [SerializeField] string specialIngredient;

    Pelanggan pelanggan;

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

        if (pelanggan != null)
        {
            if (specialIngredient == pelanggan.GetFoodString())
            {
                pelanggan.MenuServed();
            }
            else { pelanggan.MenuWrong(); }
            Destroy(gameObject); 
        }
        
        transform.position = startPos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FoodContainer"))
        {
            transform.localScale = new Vector3(transform.localScale.x + enlargeValue, transform.localScale.y + enlargeValue, transform.localScale.z + enlargeValue);
            if (pelanggan == null)
            {
                pelanggan = collision.gameObject.GetComponent<Pelanggan>();
                if (specialIngredient == pelanggan.GetFoodString())
                {
                    pelanggan.RightFood();
                }
                else { pelanggan.WrongFood(); }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FoodContainer"))
        {
            transform.localScale = startSize;
            if (pelanggan != null)
            {
                pelanggan.IdleFood();
                pelanggan = null;
            }
        }
    }

    public void ModifiedIngridient(String ingredient)
    {
        specialIngredient = ingredient;
        // return specialIngredient;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
