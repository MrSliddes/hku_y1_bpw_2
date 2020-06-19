using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDestroyer : MonoBehaviour
{
    /// <summary>
    /// Keeps the spawn room clear of other overlaying rooms
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "SpawnPointRoom")
        {
            Destroy(collision.gameObject);
        }
    }
}
