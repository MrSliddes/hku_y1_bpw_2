using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// The current enemy state
    /// </summary>
    public EnemyState currentState;
    /// <summary>
    /// The damage the enemy does to the player
    /// </summary>
    public int damage = 1;
    /// <summary>
    /// The movementspeed of the enemy
    /// </summary>
    public float movementSpeed = 3;
    /// <summary>
    /// The agro range (when is the enemy chasing the player?)
    /// </summary>
    public float agroRange = 2f;
    /// <summary>
    /// The range in which the enemy can attack
    /// </summary>
    public float attackRange = 0.5f;
    /// <summary>
    /// The hitpoints of the enemy
    /// </summary>
    public int hp = 1;
    /// <summary>
    /// Used for entering new states (1time check)
    /// </summary>
    [HideInInspector] public bool hasEnterdNewState = false;
    /// <summary>
    /// The player transform
    /// </summary>
    [HideInInspector] public Transform player;
    /// <summary>
    /// Rigidbody 2d of enemy
    /// </summary>
    private Rigidbody2D rb;
    /// <summary>
    /// The Ai path of the enemy
    /// </summary>
    [HideInInspector] public AIPath aiPath;
    /// <summary>
    /// Sound that plays when enemy gets hurt
    /// </summary>
    public AudioSource hurt;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
        GetComponent<AIDestinationSetter>().target = player;
        // Set random speed for extra difficulty
        movementSpeed = Random.Range(2, 5);
        aiPath.maxSpeed = movementSpeed;
        // Enter the idle state
        EnterNewEnemyState(EnemyState.idle);
    }

    // Update is called once per frame
    void Update()
    {
        // Update enemy state
        UpdateEnemyState();
    }

    /// <summary>
    /// Used to enter a new state
    /// </summary>
    /// <param name="state">What state to enter</param>
    public void EnterNewEnemyState(EnemyState state)
    {
        currentState = state;
        hasEnterdNewState = false;
    }

    /// <summary>
    /// Updates the Enemy state
    /// </summary>
    public virtual void UpdateEnemyState()
    {
        switch(currentState)
        {
            case EnemyState.idle:
                StateIdle();
                break;
            case EnemyState.chasing:
                StateChasing();
                break;
            case EnemyState.attack:
                StateAttack();
                break;
            case EnemyState.death:
                StateDeath();
                break;
            default:
                break;
        }
    }

    public virtual void StateIdle()
    {
        // Enter
        if(!hasEnterdNewState)
        {
            rb.velocity = Vector2.zero;
            aiPath.canMove = false;
        }

        // Exit
        if(Vector2.Distance(transform.position, player.position) <= agroRange)
        {
            EnterNewEnemyState(EnemyState.chasing);
        }
    }

    public virtual void StateChasing()
    {
        // Enter
        if(!hasEnterdNewState)
        {
            aiPath.canMove = true;
        }

        // Exit
        if(Vector2.Distance(transform.position, player.position) > agroRange)
        {
            EnterNewEnemyState(EnemyState.idle);
        }
    }

    public virtual void StateAttack()
    {
        // This is handeld in OnCollisionEnter2D
    }

    public virtual void StateDeath()
    {
        // Enter
        if(!hasEnterdNewState)
        {
            ManagerUI.score += 10;
            ManagerUI.KilledEnemy();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// This is called when the enemy recieves damage by the player
    /// </summary>
    /// <param name="amount">The amount of damage</param>
    public virtual void ReceiveDamage(int amount)
    {
        hp -= amount;
        hurt.Play();
        if(hp <= 0)
        {
            EnterNewEnemyState(EnemyState.death);
        }
    }

    /// <summary>
    /// Check if the enemie is hurting the player
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Player>().TakeDamage(damage);
        }
    }
}

/// <summary>
/// The different enemy states
/// </summary>
public enum EnemyState
{
    idle,
    chasing,
    attack,
    death
}
