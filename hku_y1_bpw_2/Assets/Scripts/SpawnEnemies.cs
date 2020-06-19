using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn a random enemy from an array
/// </summary>
public class SpawnEnemies : MonoBehaviour
{
    /// <summary>
    /// Spawn a gem?
    /// </summary>
    public GameObject gem;
    /// <summary>
    /// The enemies to choose from
    /// </summary>
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn a random enemy
        GameObject a = Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, Quaternion.identity);
        a.transform.SetParent(FindObjectOfType<DungeonGeneration>().roomsParent);
        // Spawn a gem
        a = Instantiate(gem, transform.position, Quaternion.identity);
        a.transform.SetParent(FindObjectOfType<DungeonGeneration>().roomsParent);

        Destroy(gameObject);
    }
}
