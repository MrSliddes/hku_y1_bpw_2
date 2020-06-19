using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingRoom : MonoBehaviour
{
    /// <summary>
    /// Wall that gets destroyed
    /// </summary>
    public GameObject wall;
    /// <summary>
    /// Has the collider collided?
    /// </summary>
    public bool hasCollided;

    // Update is called once per frame
    void Update()
    {
        // Check for a collision if there is still a wall
        if(wall != null)
        {
            CheckRoom();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if there is a room on the transform
        if(collision.CompareTag("SpawnPointRoom"))
        {
            hasCollided = true;
        }
    }

    /// <summary>
    /// Check for the room, and if there is a room destroy the wall (create an opening)
    /// </summary>
    public void CheckRoom()
    {
        if(hasCollided)
        {
            Destroy(wall.gameObject);
        }
    }
}
