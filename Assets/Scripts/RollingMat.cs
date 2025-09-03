using UnityEngine;
using System.Collections;

public class RollingMat : MonoBehaviour
{
    [Header("Maki Creation")]
    public GameObject makiPrefab;

    public bool hasSeaweed = false;
    public bool hasRice = false;
    private ToppingType currentTopping;
    private bool hasTopping = false;
    private bool isRolling = false;

    void Update()
    {
        // Hold W for 5 seconds to roll maki
        if (Input.GetKeyDown(KeyCode.W) && CanRoll())
        {
            StartCoroutine(RollMaki());
        }
    }

    bool CanRoll()
    {
        return hasSeaweed && hasRice && hasTopping && !isRolling;
    }

    public void AddSeaweed()
    {
        hasSeaweed = true;
        Debug.Log("Seaweed added to rolling mat");
    }

    public void AddRice()
    {
        if (hasSeaweed)
        {
            hasRice = true;
            Debug.Log("Rice added to rolling mat");
        }
    }

    public void AddTopping(ToppingType topping)
    {
        if (hasSeaweed && hasRice && !hasTopping)
        {
            hasTopping = true;
            currentTopping = topping;
            Debug.Log($"{topping} added to rolling mat");
        }
    }

    IEnumerator RollMaki()
    {
        isRolling = true;
        Debug.Log("Hold W to roll maki...");

        float rollTime = 0f;
        while (Input.GetKey(KeyCode.W) && rollTime < 5f)
        {
            rollTime += Time.deltaTime;
            // Optional: Show progress bar here
            yield return null;
        }

        if (rollTime >= 5f)
        {
            // Successfully rolled maki
            GameObject maki = Instantiate(makiPrefab, transform.position, Quaternion.identity);
            CompletedSushi completedSushi = maki.GetComponent<CompletedSushi>();
            completedSushi.sushiType = SushiType.Maki;
            completedSushi.topping = currentTopping;

            Debug.Log($"Maki with {currentTopping} created!");
            ResetMat();
        }
        else
        {
            Debug.Log("Rolling cancelled - didn't hold W long enough");
        }

        isRolling = false;
    }

    void ResetMat()
    {
        hasSeaweed = false;
        hasRice = false;
        hasTopping = false;
    }
}