using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn settings")]
    [SerializeField] float interval;
    [SerializeField] GameObject pelanggan;

    [Header("Capacity settings")]
    [SerializeField] int maxPelanggan = 4;
    int currentPelanggan = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnCoroutine()
    {
        while(true)
        { if (currentPelanggan < maxPelanggan)
            {
                GameObject newPelanggan = Instantiate(pelanggan, transform.position, quaternion.identity);

                newPelanggan.GetComponent<Pelanggan>().init(this);

                currentPelanggan++;
            }

            yield return new WaitForSeconds(interval);
        }
    }



    public void PelangganKeluar()
    {
        currentPelanggan--;
    }
}
