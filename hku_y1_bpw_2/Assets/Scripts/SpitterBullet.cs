using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterBullet : MonoBehaviour
{
    /// <summary>
    /// The speed of the bullet
    /// </summary>
    public float speed = 7f;
    // Components
    Rigidbody2D rb;
    Player player;
    Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        // Put the bullet in the direction of the player and destroy when alive for 3 seconds
        moveDir = (player.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(gameObject, 3f);
    }

    /// <summary>
    /// Check if it hit player and deal damage, else destroy itself
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Player>().TakeDamage(1);
        }
        Destroy(gameObject);
    }

}
