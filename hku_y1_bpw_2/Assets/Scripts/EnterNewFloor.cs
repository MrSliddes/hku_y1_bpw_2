using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterNewFloor : MonoBehaviour
{
    /// <summary>
    /// Triggers going into a new dungeon level
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //StartCoroutine(FindObjectOfType<DungeonGeneration>().GenerateDungeon()); This apperently doesnt work, call the coroutine from the script itself
            collision.GetComponent<Player>().nextLVL.Play();
            DungeonGeneration.Instance.StartCoroutine(DungeonGeneration.Instance.GenerateDungeon());
            Destroy(gameObject);
        }
    }
}
