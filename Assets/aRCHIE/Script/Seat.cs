using UnityEngine;

public class Seat : MonoBehaviour
{
    public bool isOccupied = false;

    public void Dipakai() { isOccupied = true; }
    public void Kosong() { isOccupied = false; }
}
