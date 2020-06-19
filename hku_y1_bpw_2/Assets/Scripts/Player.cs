using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// The current hp of the player
    /// </summary>
    public int currentHP;
    /// <summary>
    /// The max hp of the player
    /// </summary>
    public int maxHP = 3;
    /// <summary>
    /// The movementspeed of the player
    /// </summary>
    public float moveSpeed = 5f;
    /// <summary>
    /// The projectile the player can shoot
    /// </summary>
    public GameObject projectilePrefab;
    /// <summary>
    /// Players rigidbody
    /// </summary>
    private Rigidbody2D rb;
    /// <summary>
    /// The movement direction of the player
    /// </summary>
    private Vector2 movement;
    /// <summary>
    /// Sound when player shoots
    /// </summary>
    public AudioSource shoot;
    /// <summary>
    /// Sound when player reaches next level
    /// </summary>
    public AudioSource nextLVL;
    /// <summary>
    /// Sound when player gets hit
    /// </summary>
    public AudioSource playerHit;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();

        // Set values
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        // Not dead
        if(currentHP <= 0)
        {
            AudioListener.volume = 0;
            return;
        }
        else
        {
            AudioListener.volume = 1;
        }

        // Movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Shooting();
    }

    private void FixedUpdate()
    {
        // Move player with given input
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Allows the player to shoot projectiles
    /// </summary>
    private void Shooting()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            // Fire projectile
            shoot.Play();
            ManagerUI.score--;
            int speed = 5;
            Vector3 shootDir = Input.mousePosition;
            shootDir.z = 0;
            shootDir = Camera.main.ScreenToWorldPoint(shootDir);
            shootDir = shootDir - transform.position;

            GameObject a = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            a.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDir.x * speed, shootDir.y * speed);
        }
    }

    /// <summary>
    /// This gets called when the player recievs damage from an enemy
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        playerHit.Play();
        currentHP -= amount;
    }
}
