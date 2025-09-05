using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Spawn settings")]
    [SerializeField] float interval;
    [SerializeField] GameObject[] pelanggan;

    [Header("Capacity settings")]
    [SerializeField] Seat[] seats;

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
        while (true)
        {
            Seat kursi = cariSeat();
            if (kursi != null)
            {
                int randomC = Random.Range(0, pelanggan.Length);
                GameObject newPelanggan = Instantiate(pelanggan[randomC], transform.position, quaternion.identity);

                newPelanggan.GetComponent<Pelanggan>().init(this, kursi);
            }
            yield return new WaitForSeconds(interval);
        }
    }

    Seat cariSeat()
    {
        foreach (Seat kursi in seats)
        {
            if (!kursi.isOccupied)
            {
                return kursi;
            }
        }
        return null;
    }

}
