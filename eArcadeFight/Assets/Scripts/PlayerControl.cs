using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;             // Determines which way the player is facing
    [HideInInspector]
    public bool jump = false;                   // Condition for whether the player can jump

    public float moveForce = 365f;              // Amount of force added to move player left and right
    public float maxSpeed = 6f;                 // The fastest the player can travel in the x-axis
    public float jumpForce = 50f;               // Amount of force added when the player jumps

    private bool isGrounded = false;              // Whether or not the player is grounded
    private Transform groundCheck;              // A position marking where to check if the player is grounded
    private Animator anim;                      // Reference to the player's animator componenet

    void Awake()
    {
        // Setting up references
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        // If the jump button is pressed and the player is grounded then the player should jump
        if (Input.GetButtonDown("Jump") && isGrounded)
            jump = true;
    }

    // FixedUpdate is an update function interdependent 
    void FixedUpdate()
    {
        // Cache the x axis input
        float xInput = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(xInput));


    }

    void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
