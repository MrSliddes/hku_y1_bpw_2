using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    /// <summary>
    /// The damage the projectile does
    /// </summary>
    public int damage = 1;
    
    /// <summary>
    /// When the projectile hits an enemy: deal damage, else destory itself
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            // Do nothing ( this is here cause if it hits a wall it will be destroyed with the code under here)
        }
        else
        {
            if(collision.transform.tag == "Enemy")
            {
                collision.transform.GetComponent<Enemy>().ReceiveDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
