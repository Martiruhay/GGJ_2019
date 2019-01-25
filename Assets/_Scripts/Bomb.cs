using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int id;
    public float lifetime;

    public Rigidbody2D rb;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {

    }

    private void HitBomb()
    {
        Debug.Log("Hit bullet!");
    }

    private void HitBullet()
    {
        Debug.Log("Hit bullet!");
    }

    private void Explode()
    {
        Debug.Log("BOUM!");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter: " + collision.gameObject.name);
        if (collision.CompareTag("Bullet"))
        {
            HitBullet();
        }
        else if (collision.CompareTag("Bomb"))
        {
            HitBomb();
        }
        else
        {
        }
        Explode();
    }
}
