using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 80f;
    public float lifeTime = 2f;
    public int damage = 20;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, lifeTime); // Destroy the bullet after 'lifeTime' seconds
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bullet collided with " + collision.gameObject.name);

            // Check if the collided object has the Enemy component
            Target enemy = collision.gameObject.GetComponent<Target>();
            if (enemy != null)
            {
                Debug.Log("Hit enemy: " + collision.gameObject.name); // Add this line

                // Cause damage to the enemy
                enemy.TakeDamage(damage);
            }
            // Destroy the bullet upon collision
            Destroy(gameObject);
        }
    }
}


