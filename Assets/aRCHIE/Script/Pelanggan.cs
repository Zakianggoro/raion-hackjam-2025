using System;
using System.Collections;
using System.Timers;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pelanggan : MonoBehaviour
{
    [Header("Sprites Settings")]
    [SerializeField] SpriteRenderer charS;
    [SerializeField] SpriteRenderer dialogS;
    [SerializeField] SpriteRenderer foodS;
    [SerializeField] SpriteRenderer emoteS;
    [SerializeField] Sprite happyChar;
    [SerializeField] Sprite boredChar;
    [SerializeField] Sprite madChar;
    [SerializeField] Sprite marahEmot;
    [SerializeField] Sprite happyEmot;
    [SerializeField] Sprite[] dialogText;
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
    [SerializeField] Transform emotTr;
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
    bool isFoodSprite = false;
    bool isFoodDialog = false;
    bool isActivEmot = false;

    Spawner spawner;
    Seat kursiPelanggan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emoteS.enabled = false;
        int randFoD = Random.Range(0, 2);
        switch (randFoD)
        {
            case 0:
                isFoodDialog = true;
                randomFoodDialog();
                break;
            case 1:
                isFoodSprite = true;
                randomFoodSprite();
                break;
        }
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
                if (isFoodSprite)
                {
                    foodS.color = new Color(foodS.color.r, foodS.color.g, foodS.color.b, 0.8f);
                    foodS.enabled = true;
                }
                StartCoroutine(timerCoroutine());
            }
        }
        else if (isLeaving || isServed)
        {
            activateEmote();
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

    IEnumerator emotCoroutine()
    {
        emoteS.enabled = true;
        while (true)
        {
            emotTr.rotation = quaternion.Euler(0f, 0f, -14.467f);
            yield return new WaitForSeconds(0.55f);
            emotTr.rotation = quaternion.Euler(0f, 0f, 14.467f);
            yield return new WaitForSeconds(0.55f);
        }
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
        startPos = s.transform.position;
    }

    void OnDestroy()
    {
        if (kursiPelanggan != null) kursiPelanggan.Kosong();
    }

    void randomFoodSprite()
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
        dialogS.enabled = false;
        foodS.enabled = false;
    }

    void randomFoodDialog()
    {
        foodS.enabled = false;
        int randomD = Random.Range(0, dialogText.Length);
        dialogS.sprite = dialogText[randomD];

        switch (randomD)
        {
            case 0:
                foodsString = "Crab Stick";
                break;
            case 1:
                foodsString = "Alpukat";
                break;
            case 2:
                foodsString = "Unagi";
                break;
            case 3:
                foodsString = "Salmon";
                break;
            case 4:
                foodsString = "Udang Tempura";
                break;
            case 5:
                foodsString = "Timun";
                break;
            case 6:
                foodsString = "Unasaus";
                break;
            case 7:
                foodsString = "Sesame";
                break;
            case 8:
                foodsString = "Nori";
                break;
            case 9:
                foodsString = "Udang Tempura";
                break;
            case 10:
                foodsString = "Timun";
                break;
            case 11:
                foodsString = "Alpukat";
                break;
            case 12:
                foodsString = "Crab Stick";
                break;
            case 13:
                foodsString = "Unagi";
                break;
            case 14:
                foodsString = "Salmon";
                break;
        }
        dialogS.enabled = false;
    }

    void activateEmote()
    {
        emoteS.enabled = true;
        if (isLeaving)
        {
            emoteS.sprite = marahEmot;
        }
        if (isServed)
        {
            emoteS.sprite = happyEmot;
        }
        if (!isActivEmot)
        {
            isActivEmot = true;
            StartCoroutine(emotCoroutine());
        }
        
    }
}
