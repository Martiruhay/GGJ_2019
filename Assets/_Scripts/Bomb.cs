﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    public int id;
    public float lifetime;
    public float maxRotationSpeed;

    public Rigidbody2D rb;
    public GameObject splashParticles, splashAnim;
    public GameObject stainPrefab;

    public float minStainSize, maxStainSize;

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

    public void Shoot(Vector3 newVel)
    {
        transform.parent = null;
        rb.simulated = true;
        rb.angularVelocity = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        GetComponent<Animator>().enabled = false;
        transform.DOShakeScale(10f, 0.15f, 10, 90.0f, fadeOut: false);
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
            Home h = collision.GetComponent<Home>();
            if (h.id != id)
            {
                h.HitBomb();
                InstantiateStain();
                Explode();
            }
        }
        else if (collision.CompareTag("Player"))
        {
            Player p = collision.GetComponent<Player>();
            if (p.playerNum != id)
            {
                p.HitBomb();
                InstantiateStain();
                Explode();
            }
        }
        else
        {
            InstantiateStain();
            Explode();
        }
    }
}
