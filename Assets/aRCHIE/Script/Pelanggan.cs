using System;
using System.Collections;
using System.Timers;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pelanggan : MonoBehaviour
{
    [Header("Sprites Settings")]
    [SerializeField] SpriteRenderer charS;
    [SerializeField] SpriteRenderer dialogS;
    [SerializeField] SpriteRenderer foodS;
    [SerializeField] Sprite happyChar;
    [SerializeField] Sprite boredChar;
    [SerializeField] Sprite madChar;
    [SerializeField] Sprite[] foodsSprite;
    [SerializeField] String foodsString;

    [Space]

    [Header("Waiting time")]
    [SerializeField] float waitTime = 15.0f;

    [Space]

    [Header("Location Setting")]
    [SerializeField] Vector2 startPos;
    [SerializeField] Vector2 targetPos;

    [Space]

    [Header("Move settings")]
    [SerializeField] Transform charTr;
    [SerializeField] float moveSpeed;
    [SerializeField] float intervalStep;
    [SerializeField] int degreeStep;

    [Space]

    [Header("Boolean Settings")]
    bool isOnFinalPosition = false;
    bool isLeaving = false;
    bool isServed = false;
    bool walkStart = false;

    Spawner spawner;
    Seat kursiPelanggan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int random = Random.Range(0, foodsSprite.Length);
        foodS.sprite = foodsSprite[random];

        switch (random)
        {
            case 0:
                foodsString = "Maki";
                break;
            case 1:
                foodsString = "Nigiri";
                break;
        }
        startPos = transform.position;
        dialogS.enabled = false;
        foodS.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnFinalPosition && !isLeaving)
        {
            if (!walkStart)
            {
                walkStart = true;
                StartCoroutine(walkCoroutine());
            }

            charS.flipX = true;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPos) < 0.01f)
            {
                walkStart = false;
                charTr.rotation = Quaternion.Euler(0f, 0f, 0f);
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
                if (!walkStart)
                {
                    walkStart = true;
                    StartCoroutine(walkCoroutine());
                }
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

    IEnumerator walkCoroutine()
    {
        while (!isOnFinalPosition || isLeaving)
        {
            charTr.rotation = Quaternion.Euler(0f, 0f, degreeStep);
            yield return new WaitForSeconds(intervalStep);
            charTr.rotation = Quaternion.Euler(0f, 0f, -degreeStep);
            yield return new WaitForSeconds(intervalStep);
        }
        charTr.rotation = Quaternion.Euler(0f, 0f, 0f);
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
