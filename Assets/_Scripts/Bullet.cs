using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int id;
    public float lifetime;
    
    public Rigidbody2D rb;
    public GameObject splashParticles;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void HitBullet()
    {
        Debug.Log("Hit bullet!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter: " + collision.gameObject.name);
        if (collision.CompareTag("Bullet"))
        {
            HitBullet();
        }
        if (collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();
            if (p.playerNum != id)
                p.Hit();
        }
        // Particles
        //Time.timeScale = 0.1f;
        GameObject g = Instantiate(splashParticles, transform.position, Quaternion.identity);
        g.transform.up = -rb.velocity.normalized;

        Destroy(gameObject);
    }
}
