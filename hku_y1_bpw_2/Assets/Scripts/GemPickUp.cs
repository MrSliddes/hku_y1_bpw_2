using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickUp : MonoBehaviour
{
    /// <summary>
    /// Has the player picked this gem up? This is used so the sound can play before it is destroyed
    /// </summary>
    private bool pickedUp = false;

    // Start is called before the first frame update
    void Start()
    {     
        // Random value that decides if the gem doesnt get destroyed on load
        if(Random.value >= 0.3) Destroy(gameObject);
    }

    /// <summary>
    /// Player picks up gem, play sound and update inventory
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && pickedUp ==false)
        {
            pickedUp = true;
            GetComponent<SpriteRenderer>().enabled = false;
            ManagerUI.GotGem();
            ManagerUI.score += 50;
            GetComponent<AudioSource>().Play();
            Destroy(gameObject, 1);
        }
    }
}
