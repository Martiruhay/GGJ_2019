using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Controller controller;

    public int playerNum;
    public Color myColor;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpSpeed;
    public int maxJumps;
    public LayerMask groundMask;
    public float fallMult, lowJumpMult;
    public int nJumpParticles;

    [Header("Shooting")]
    public float bulletSpeed;
    public float bulletRefillTime;
    public int maxBulletAmmo;
    public int bulletAmmo;
    public float bombSpeed;
    public float bombRefillTime;
    public int maxBombAmmo;
    public int bombAmmo;
    public float bombShootT;

    [Header("References")]
    public Transform rayEnd;
    public Aimer aimer;
    public Transform bulletSpawn;
    public Transform bombSpawn;
    public Animator animAttack, animMove, gunAnim;
    public Ammo ammo;
    public ParticleSystem jumpPS, landPS, shootPS;
    public GameObject stunObject;

    [Header("Prefabs")]
    public GameObject bulletPrefab;
    public GameObject bombPrefab;

    // Private
    Rigidbody2D rb;
    private float horizontal;
    private bool jump, fire1, fire2;
    private int nJumps;
    public bool grounded;
    private bool stuned;
    private float stunTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        nJumps = maxJumps;
        bulletAmmo = maxBulletAmmo;

        ammo.show = myColor;

        StartCoroutine(BulletRefill());
        StartCoroutine(BombRefill());
        StartCoroutine(Stun());
        StartCoroutine(WalkingParticles());
    }

    private void Update()
    {
        HandleInput();
        CheckFloor();
        HandleMovement();
        HandleShoot();
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal_P" + playerNum.ToString());
        jump = Input.GetButtonDown("Jump_P" + playerNum.ToString());
        fire1 = Input.GetButtonDown("Fire1_P" + playerNum.ToString());
        fire2 = Input.GetButtonDown("Fire2_P" + playerNum.ToString());
    }

    private void HandleMovement()
    {
        Vector2 vel = rb.velocity;
        if (!stuned)
            vel.x = horizontal * moveSpeed;

        // Jump
        if (jump && nJumps > 0 && !stuned)
        {
            vel.y = nJumps == maxJumps ? jumpSpeed : jumpSpeed - jumpSpeed / 4;
            --nJumps;
            // Play jump sound
            AudioManager.instance.Play("jump_boi");

            jumpPS.Emit(nJumpParticles);
        }
        rb.velocity = vel;

        if (rb.velocity.y < 0) //If we are falling
        {
            //We apply more force downwards to fall faster
            rb.velocity -= Vector2.down * Physics2D.gravity.y * fallMult * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && (stuned || !Input.GetButton("Jump_P" + playerNum))) //If we are going up and not pressing the jump button
        {
            //We apply more force downwards to fall faster
            rb.velocity -= Vector2.down * Physics2D.gravity.y * lowJumpMult * Time.deltaTime;
        }

        animMove.SetFloat("speed", Mathf.Abs(rb.velocity.x));
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
        animMove.SetBool("grounded", grounded);
    }

    private void HandleLanding()
    {
        nJumps = maxJumps;
        landPS.Emit(nJumpParticles);
        AudioManager.instance.Play("landing");
    }

    private void HandleShoot()
    {
        // Bullet
        if (fire1 && bulletAmmo > 0)
        {
            // SHOOT
            --bulletAmmo;
            // Bullet shoot sound
            AudioManager.instance.Play("bullet_shoot");
            Vector3 dir = aimer.transform.up;
            GameObject g = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            Bullet b = g.GetComponent<Bullet>();
            b.SetColor(myColor);
            b.id = playerNum;
            b.rb.velocity = bulletSpeed * dir;
            gunAnim.SetTrigger("shoot");
            shootPS.Emit(20);
        }

        // Bomb
        if (fire2 && bombAmmo > 0)
        {
            // SHOOT
            --bombAmmo;
            // Bomb shoot sound
            AudioManager.instance.Play("throw_bomb");
            Vector3 dir = aimer.transform.up;
            GameObject g = Instantiate(bombPrefab, bombSpawn.position, Quaternion.identity);
            g.transform.parent = bombSpawn;
            Bomb b = g.GetComponent<Bomb>();
            b.SetColor(myColor);
            b.id = playerNum;
            StartCoroutine(ShootBalloon(b, bombSpeed * dir));
            animAttack.SetTrigger("balloon");
        }

        ammo.SetAmmo(bulletAmmo, bombAmmo);
    }

    IEnumerator ShootBalloon(Bomb b, Vector3 velocity)
    {
        yield return new WaitForSeconds(bombShootT);
        b.Shoot(velocity);
    }



    private IEnumerator BulletRefill()
    {
        while (true)
        {
            while (bulletAmmo >= maxBulletAmmo)
                yield return null;
            yield return new WaitForSeconds(bulletRefillTime);
            ++bulletAmmo;
        }
    }

    private IEnumerator BombRefill()
    {
        while (true)
        {
            while (bombAmmo >= maxBombAmmo)
                yield return null;
            yield return new WaitForSeconds(bombRefillTime);
            ++bombAmmo;
        }
    }

    private IEnumerator Stun()
    {
        while (true)
        {
            stuned = false;
            stunObject.SetActive(false);
            aimer.speed = controller.aimerSpeed;
            while (stunTimer > 0)
            {   // STUNED
                stuned = true;
                stunTimer -= Time.deltaTime;
                stunObject.SetActive(true);
                aimer.speed = 0;
                yield return null;
            }
            stunTimer = Mathf.Max(0, stunTimer);
            yield return null;
        }
    }

    private IEnumerator WalkingParticles()
    {
        while (true)
        {
            while (!grounded || Mathf.Abs(rb.velocity.x) < .2f)
                yield return null;
            landPS.Emit(2);
            yield return new WaitForSeconds(.2f);
        }
    }

    public void HitBullet()
    {
        stunTimer += controller.bulletStunDuration;
    }

    public void HitBomb()
    {
        stunTimer += controller.bombStunDuration;
    }
}
