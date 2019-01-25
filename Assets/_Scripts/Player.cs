using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNum;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpSpeed;
    public int maxJumps;
    public LayerMask groundMask;
    public Transform rayStart, rayEnd;

    // Private
    Rigidbody2D rb;
    private float horizontal;
    private bool jump, shoot1, shoot2;
    private int nJumps;
    public bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        nJumps = maxJumps;
    }

    private void Update()
    {
        HandleInput();
        CheckFloor();
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 vel = rb.velocity;
        vel.x = horizontal * moveSpeed;
        if (jump && nJumps > 0)
        {
            --nJumps;
            vel.y = jumpSpeed;
        }
        rb.velocity = vel;
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
    }

    private void CheckFloor()
    {
        RaycastHit2D hit = Physics2D.Linecast(rayStart.position, rayEnd.position, groundMask);

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
}
