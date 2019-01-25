using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        Destroy(gameObject);
    }
}
