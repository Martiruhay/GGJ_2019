using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Bullet : MonoBehaviour
{
    public static int stainOrderLayer = 0;

    public int id;
    public float lifetime;
    
    public Rigidbody2D rb;
    public GameObject splashParticles1, splashParticles2;
    public GameObject stainPrefab;

    public float minStainSize, maxStainSize;

    private Color myColor;
    private Material mat;

    public float mag, rough, fadeI, fadeO;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void HitBullet()
    {
        Debug.Log("Hit bullet!");
        CameraShaker.Instance.ShakeOnce(mag * 0.5f, rough * 0.5f, fadeI, fadeO);
    }

    public void SetColor(Color c)
    {
        myColor = c;
        GetComponent<SpriteRenderer>().color = myColor;
        GetComponent<TrailRenderer>().material.color = myColor;
        mat = GetComponent<SpriteRenderer>().material;
        if (mat == null)
        {
            Debug.Log("lkdjkhjadn");
        }
        mat.SetColor("_ColorOutline", myColor);
        mat.SetColor("_Color", myColor);
    }

    private void InstantiateStain()
    {
        // Stain
        float rd = Random.Range(0, maxStainSize);
        Vector3 pos = transform.position + (Vector3)rb.velocity.normalized * rd;
        GameObject s = Instantiate(stainPrefab, pos, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        SpriteRenderer sr = s.GetComponent<SpriteRenderer>();
        sr.color = myColor;
        sr.sortingOrder = ++Bullet.stainOrderLayer;
        float r = Random.Range(minStainSize, maxStainSize);
        s.transform.localScale = new Vector3(r, r, r);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            HitBullet();
        }
        else if (collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();
            if (p.playerNum != id)
            {
                p.HitBullet();
                CameraShaker.Instance.ShakeOnce(mag, rough, fadeI, fadeO);
            }
        }
        else if (collision.CompareTag("Home"))
        {
            Home h = collision.GetComponent<Home>();
            if (h.id != id)
                h.HitBullet();

            InstantiateStain();
        }
        else
        {
            InstantiateStain();
        }


        // Bullet hit sound
        AudioManager.instance.Play("bullet_hit");
        // Particles
        GameObject g = Instantiate(id == 1 ? splashParticles1 : splashParticles2, transform.position, Quaternion.identity);
        //ParticleSystem.MainModule settings = g.GetComponent<ParticleSystem>().main;
        //float H, S, V;
        //Color.RGBToHSV(myColor, out H, out S, out V);
        //settings.startColor = Random.ColorHSV(H, H, S, 0, V, V);

        //ParticleSystem.MinMaxGradient grad = new ParticleSystem.MinMaxGradient(myColor, Color.white);
        
        //grad.mode = ParticleSystemGradientMode.RandomColor;
        // then set this grad variable to some particle module
        //settings.startColor = grad;

        g.transform.up = -rb.velocity.normalized;

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
