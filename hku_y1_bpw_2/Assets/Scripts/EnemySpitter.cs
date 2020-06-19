using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpitter : Enemy
{
    /// <summary>
    /// The bullet the spitter shoots
    /// </summary>
    public GameObject spitterBullet;
    /// <summary>
    /// The rate at which the enemy shoots
    /// </summary>
    public float fireRate = 2f;
    /// <summary>
    /// The timer that keeps track when enemy is allowed to shoot
    /// </summary>
    public float fireRateTimer;

    /// <summary>
    /// New overriden chace state
    /// </summary>
    public override void StateChasing()
    {
        // Enter
        if(!hasEnterdNewState)
        {
            aiPath.canMove = true;
        }

        // Update
        Fire();

        // Exit
        if(Vector2.Distance(transform.position, player.position) > agroRange)
        {
            EnterNewEnemyState(EnemyState.idle);
        }
    }

    /// <summary>
    /// Shoot a bullet towords the player
    /// </summary>
    public void Fire()
    {
        if(fireRateTimer > 0)
        {
            fireRateTimer -= Time.deltaTime;
        }
        else
        {
            // shoot
            fireRateTimer = fireRate;
            Instantiate(spitterBullet, transform.position, Quaternion.identity);
        }

    }
}
