using UnityEngine;
using System.Collections;

public class Knife : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Topping topping = other.GetComponent<Topping>();
        if (topping != null && !topping.isCut)
        {
            topping.CutTopping();
        }
    }
}