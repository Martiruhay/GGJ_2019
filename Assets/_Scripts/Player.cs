using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Controller controller;

    public int playerNum;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpSpeed;
    public int maxJumps;
    public LayerMask groundMask;

    [Header("Shooting")]
    public float bulletSpeed;
    public float bulletRefillTime;
    public int maxBulletAmmo;
    public int bulletAmmo;
    public float bombSpeed;
    public float bombRefillTime;
    public int maxBombAmmo;
    public int bombAmmo;

    [Header("References")]
    public Transform rayEnd;
    public Transform aimer;
    public Transform bulletSpawn;

    [Header("Prefabs")]
    public GameObject bulletPrefab;
    public GameObject bombPrefab;

    // Private
    Rigidbody2D rb;
    private float horizontal;
    private bool jump, fire1, fire2;
    private int nJumps;
    public bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        nJumps = maxJumps;
        bulletAmmo = maxBulletAmmo;
        StartCoroutine(BulletRefill());
        StartCoroutine(BombRefill());
    }

    private void Update()
    {
        HandleInput();
        CheckFloor();
        HandleMovement();
        HandleShoot();
    }

    private void HandleMovement()
    {
        Vector2 vel = rb.velocity;
        vel.x = horizontal * moveSpeed;

        // Jump
        if (jump && nJumps > 0)
        {
            --nJumps;
            vel.y = jumpSpeed;
            // Play jump sound
        }
        rb.velocity = vel;
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal_P" + playerNum);
        jump = Input.GetButtonDown("Jump_P" + playerNum);
        fire1 = Input.GetButtonDown("Fire1_P" + playerNum);
        fire2 = Input.GetButtonDown("Fire2_P" + playerNum);
    }

    private void CheckFloor()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, rayEnd.position, groundMask);

        // If it hits something...
        if (hit.collider != null)
        {
            if (!grounded)
            {
                HandleLanding();
            }
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void HandleLanding()
    {
        nJumps = maxJumps;
    }

    private void HandleShoot()
    {
        // Bullet
        if (fire1 && bulletAmmo > 0)
        {
            // SHOOT
            --bulletAmmo;
            // Bullet shoot sound
            Vector3 dir = aimer.up;
            GameObject g = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            Bullet b = g.GetComponent<Bullet>();
            b.id = playerNum;
            b.rb.velocity = bulletSpeed * dir;
        }

        // Bomb
        if (fire2 && bombAmmo > 0)
        {
            // SHOOT
            --bombAmmo;
            // Bomb shoot sound
            Vector3 dir = aimer.up;
            GameObject g = Instantiate(bombPrefab, bulletSpawn.position, Quaternion.identity);
            Bomb b = g.GetComponent<Bomb>();
            b.id = playerNum;
            b.rb.velocity = bombSpeed * dir;
        }
    }

    private IEnumerator BulletRefill()
    {
        while (true)
        {
            if (bulletAmmo < maxBulletAmmo)
                ++bulletAmmo;
            yield return new WaitForSeconds(bulletRefillTime);
        }
    }

    private IEnumerator BombRefill()
    {
        while (true)
        {
            if (bombAmmo < maxBombAmmo)
                ++bombAmmo;
            yield return new WaitForSeconds(bombRefillTime);
        }
    }

    public void HitBullet()
    {

    }

    public void HitBomb()
    {

    }
}
