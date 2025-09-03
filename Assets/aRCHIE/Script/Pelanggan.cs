using System.Collections;
using System.Timers;
using UnityEngine;

public class Pelanggan : MonoBehaviour
{
    [Header("Sprites Settings")]
    [SerializeField] SpriteRenderer charS;
    [SerializeField] SpriteRenderer dialogS;
    [SerializeField] SpriteRenderer foodS;
    [SerializeField] Sprite happyChar;
    [SerializeField] Sprite boredChar;
    [SerializeField] Sprite madChar;
    [SerializeField] Sprite foodsSprite;

    [Space]

    [Header("Waiting time")]
    [SerializeField] float waitTime = 15.0f;

    [Space]

    [Header("Location Setting")]
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 targetPos;

    [Space]

    [Header("Move settings")]
    [SerializeField] float moveSpeed;

    [Space]

    [Header("Boolean Settings")]
    bool isOnFinalPosition = false;
    bool isLeaving = false;
    bool isServed = false;

    Spawner spawner;
    Seat kursiPelanggan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        dialogS.enabled = false;
        foodS.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnFinalPosition && !isLeaving)
        {
            charS.flipX = true;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPos) < 0.01f)
            {
                charS.flipX = false;
                isOnFinalPosition = true;
                dialogS.enabled = true;
                foodS.color = new Color(foodS.color.r, foodS.color.g, foodS.color.b, 0.8f);
                foodS.enabled = true;
                StartCoroutine(timerCoroutine());
            }
        }
        else if (isLeaving || isServed)
        {
            if (isLeaving && !isServed)
            {
                charS.sprite = madChar;
                dialogS.enabled = false;
                foodS.enabled = false;
            }
            else if (isServed)
            {
                charS.sprite = happyChar;
                dialogS.enabled = false;
                foodS.enabled = false;
            }

            transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, startPos) < 0.01f)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator timerCoroutine()
    {
        float timeSpend = 0f;
        while (timeSpend < waitTime)
        {
            if (timeSpend >= (waitTime / 2))
            {
                charS.sprite = boredChar;
            }
            if (isServed) { yield break; }
            timeSpend += Time.deltaTime;
            yield return null;
        }

        isLeaving = true;
    }

    public void MenuServed()
    {
        isServed = true;
    }

    public void init(Spawner s, Seat kursi)
    {
        spawner = s;
        kursiPelanggan = kursi;
        kursiPelanggan.Dipakai();
        targetPos = kursi.transform.position;
    }

    void OnDestroy()
    {
        if (kursiPelanggan != null) kursiPelanggan.Kosong();        
    }
}
