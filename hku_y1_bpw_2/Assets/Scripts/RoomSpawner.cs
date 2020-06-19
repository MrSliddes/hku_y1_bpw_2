using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    /// <summary>
    /// The opening direction of the room spawner
    /// </summary>
    public int openingDirection; // 0 = top, 1 = right, 2 = down, 3 = left
    /// <summary>
    /// Has the room spawned
    /// </summary>
    private bool spawned = false;
    /// <summary>
    /// The templates of the rooms
    /// </summary>
    private RoomTemplates roomTemplates;
    /// <summary>
    /// Reference to dungeon generation
    /// </summary>
    private DungeonGeneration dungeonGeneration;

    private void Start()
    {
        // Get componetns
        roomTemplates = GameObject.FindWithTag("RoomTemplates").GetComponent<RoomTemplates>();
        dungeonGeneration = FindObjectOfType<DungeonGeneration>();
        // Wait a bit so Collision can happen ( yea not the best way but time is running out for the deadline)
        Invoke("SpawnRoom", 0.2f);
    }

    /// <summary>
    /// Spawn a room on the transform with the right openingDirection
    /// </summary>
    public void SpawnRoom()
    {
        spawned = true;
        if(dungeonGeneration.CanAddRoom(transform) == false)
        {
            SpawnClosed(openingDirection);
            return;
        }

        int r = 0;
        GameObject a = null;
        switch(openingDirection)
        {
            case 0:
                // top opening
                r = Random.Range(0, roomTemplates.topRooms.Length);
                a = Instantiate(roomTemplates.topRooms[r], transform.position, Quaternion.identity);
                break;
            case 1:
                // right opening
                r = Random.Range(0, roomTemplates.rightRooms.Length);
                a = Instantiate(roomTemplates.rightRooms[r], transform.position, Quaternion.identity);
                break;
            case 2:
                // bot opening
                r = Random.Range(0, roomTemplates.bottomRooms.Length);
                a = Instantiate(roomTemplates.bottomRooms[r], transform.position, Quaternion.identity);
                break;
            case 3:
                // left opening
                r = Random.Range(0, roomTemplates.leftRooms.Length);
                a = Instantiate(roomTemplates.leftRooms[r], transform.position, Quaternion.identity);
                break;
            default:Debug.LogError("youfuckedup");
                break;
        }

        a.transform.SetParent(dungeonGeneration.roomsParent);
    }

    /// <summary>
    /// Not allowed to spawn normal rooms anymore so spawn a closed room
    /// </summary>
    /// <param name="index"></param>
    public void SpawnClosed(int index)
    {
        GameObject a  = null;
        switch(index)
        {
            case 0:
                a = Instantiate(roomTemplates.topEnd, transform.position, Quaternion.identity);

                break;
            case 1:
                a = Instantiate(roomTemplates.rightEnd, transform.position, Quaternion.identity);
                break;
            case 2:
                a = Instantiate(roomTemplates.bottomEnd, transform.position, Quaternion.identity);
                break;
            case 3:
                a = Instantiate(roomTemplates.leftEnd, transform.position, Quaternion.identity);
                break;
            default:
                break;
        }
        a.transform.SetParent(dungeonGeneration.roomsParent);
    }

    /// <summary>
    /// Check if a collision happens with an allready existing room and spawn a room that connects the 2
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(collision.name);
        if(collision.CompareTag("SpawnPointRoom"))
        {
            if(collision.GetComponent<RoomSpawner>() == null) return;
            
            if(collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                // Spawn wall blocking off
                GameObject a = Instantiate(roomTemplates.closedRoom, transform.position, Quaternion.identity);
                a.transform.SetParent(dungeonGeneration.roomsParent);
            }
            Destroy(gameObject);
            spawned = true;
        }

        if(collision.name == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
