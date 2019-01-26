using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int id;
    public float lifetime;
    public float maxRotationSpeed;

    public Rigidbody2D rb;
    public GameObject splashParticles, splashAnim;

    private Collider2D col;
    private Color myColor;
    private Material mat;

    private void Start()
    {
        Destroy(gameObject, lifetime);
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }

    public void SetColor(Color c)
    {
        myColor = c;
        GetComponent<SpriteRenderer>().color = myColor;
        mat = GetComponent<SpriteRenderer>().material;
        if (mat == null)
        {
            Debug.Log("lkdjkhjadn");
        }
        mat.SetColor("_ColorOutline", myColor);
        mat.SetColor("_Color", myColor);

    }

    public void Shoot(Vector3 newVel)
    {
        transform.parent = null;
        rb.simulated = true;
        rb.angularVelocity = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rb.velocity = newVel;
        StartCoroutine(EnableCol());
    }

    IEnumerator EnableCol()
    {
        yield return new WaitForSeconds(0.1f);
        col.enabled = true;
    }


    private void Explode()
    {
        Debug.Log("BOUM!");
        // Bomb explosion sound
        AudioManager.instance.Play("explode_bomb");

        // Particles
        GameObject g = Instantiate(splashParticles, transform.position, Quaternion.identity);
        ParticleSystem.MainModule settings = g.GetComponent<ParticleSystem>().main;
        settings.startColor = myColor;

        g = Instantiate(splashAnim, transform.position, Quaternion.identity);
        g.GetComponent<SpriteRenderer>().color = myColor;

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
