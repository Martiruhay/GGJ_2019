using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int id;
    public float lifetime;
    public float maxRotationSpeed;

    public Rigidbody2D rb;
    public GameObject splashParticles;

    private void Start()
    {
        Destroy(gameObject, lifetime);
        rb.angularVelocity = Random.Range(-maxRotationSpeed, maxRotationSpeed);
    }

    private void Update()
    {

    }


    private void Explode()
    {
        Debug.Log("BOUM!");
        // Bomb explosion sound

        // Particles
        GameObject g = Instantiate(splashParticles, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter: " + collision.gameObject.name);
        if (collision.CompareTag("Bullet"))
        {
            Bullet b = collision.GetComponent<Bullet>();
            if (b.id != id)
            {
                Explode();
            }
        }
        else if (collision.CompareTag("Bomb"))
        {
            Bomb b = collision.GetComponent<Bomb>();
            if (b.id != id)
            {
                Explode();
            }
        }
        else if (collision.CompareTag("Home"))
        {
            Home b = collision.GetComponent<Home>();
            if (b.id != id)
            {
                Explode();
            }
        }
        else if (collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();
            if (p.playerNum != id)
            {
                p.HitBomb();
                Explode();
            }
        }
        else
        {
            Explode();
        }
    }
}
