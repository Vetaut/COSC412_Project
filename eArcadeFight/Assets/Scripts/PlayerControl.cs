using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = false;             // Determines which way the player is facing
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
        anim = this.transform.Find("model").GetComponent<Animator>();
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

        // The Speed animator parameter is set to the absolute value of the horizontal input
        anim.SetFloat("Speed", Mathf.Abs(xInput));

        // If the player is changing direction (xInput has a different sign to velocity.x) or hasn't reached max speed yet
        if (xInput * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            // add force to the player
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * xInput * moveForce);

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (xInput < 0 && !facingRight)
            Flip();
        else if (xInput > 0 && facingRight)
            Flip();

        if(jump)
        {
            anim.SetTrigger("Jump");

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            jump = false;
        }
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
