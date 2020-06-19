using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class DungeonGeneration : MonoBehaviour
{
    /// <summary>
    /// For reference
    /// </summary>
    public static DungeonGeneration Instance { get; set; }
    /// <summary>
    /// The max amount of rooms its allowed to generate
    /// </summary>
    public int maxAmountOfRooms = 10;
    /// <summary>
    /// The current amount of rooms that are generated (this number will be higher than maxAmountOfRooms since it needs to close off rooms)
    /// </summary>
    public int currentRooms = 0;
    /// <summary>
    /// The current level the player is in
    /// </summary>
    public static int currentLvl = 0;
    /// <summary>
    /// Do you want a random seed when the dungeon is generated?
    /// </summary>
    public bool randomSeed = true;
    /// <summary>
    /// The random seed value
    /// </summary>
    public int dungeonSeed = 0;
    /// <summary>
    /// The parent of all the rooms, used to destroy rooms when generating a new lvl
    /// </summary>
    public Transform roomsParent;
    /// <summary>
    /// The start room where the player is spawned in
    /// </summary>
    public GameObject startRoom;
    /// <summary>
    /// The room the player needs to reach for the next lvl
    /// </summary>
    public GameObject endRoom;
    /// <summary>
    /// The endposition used to spawn the endRoom on
    /// </summary>
    private Vector3 endPos;

    private void Awake()
    {
        // Get instance
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set values
        currentLvl = 0;
        if(randomSeed)
        {
            dungeonSeed = Random.Range(0, 999999);
        }

        // Start generating the dungeon
        StartCoroutine(GenerateDungeon());        
    }

    public void Update()
    {
        // If the dungeon generation fked up reset to get a new one (or you know, fix the bug, but unfortunatly I dont have the time for that :( )
        if(Input.GetButtonDown("Input R"))
        {
            currentLvl--;
            StartCoroutine(GenerateDungeon());
        }
    }

    /// <summary>
    /// Check if the script is allowed to add another room
    /// </summary>
    /// <param name="pos">The position where the room is placed</param>
    /// <returns>True: generate a normal room, False: generate a closed room</returns>
    public bool CanAddRoom(Transform pos)
    {
        endPos = pos.position;
        currentRooms++;
        if(currentRooms > maxAmountOfRooms) return false;
        return true;
    }

    /// <summary>
    /// Spawns the endroom
    /// </summary>
    public void SpawnEndRoom()
    {
        GameObject a = Instantiate(endRoom, endPos, Quaternion.identity);
        a.transform.SetParent(roomsParent);
    }
    
    /// <summary>
    /// Main function that generates the dungeon
    /// </summary>
    /// <returns></returns>
    public IEnumerator GenerateDungeon()
    {
        Debug.Log("Generate new rooms");
        // reset
        currentRooms = 0;
        currentLvl++;
        maxAmountOfRooms++;
        Random.InitState(dungeonSeed + currentLvl);

        bool isEmpty = false;

        // Wait for children to get destoryed
        while(isEmpty == false)
        {
            // Destroy all childs
            foreach(Transform child in roomsParent.transform)
            {
                Destroy(child.gameObject);
                yield return null;
            }
            print("Waiting");
            if(roomsParent.transform.childCount == 0) isEmpty = true;
            yield return null;
        }

        yield return new WaitForEndOfFrame();
        yield return null;

        // All old stuff is deleted, now create the startroom again
        GameObject a = Instantiate(startRoom, Vector3.zero, Quaternion.identity);
        a.transform.SetParent(roomsParent);
        // Spawn the endroom in 3 seconds
        Invoke("SpawnEndRoom", 3f);
        // Update the AI Path
        GetComponentInChildren<AstarPath>().UpdateGraphs(new Bounds(Vector3.zero, new Vector3(256, 256, 1)), 1.2f);
        // Set player pos
        FindObjectOfType<Player>().transform.position = Vector3.zero;
        // Finished
        Debug.Log("Generated new lvl");
        yield break;
    }
}
