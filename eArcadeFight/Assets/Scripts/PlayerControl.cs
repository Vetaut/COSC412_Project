using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    public bool facingRight = true;             // Determines which way the player is facing
    [HideInInspector]
    public bool jump = false;                   // Condition for whether the player can jump
    [HideInInspector]
    public bool doubleJump = false;
    [HideInInspector]
    public bool block = false;

    public float moveForce = 365f;              // Amount of force added to move player left and right
    public float maxSpeed = 6f;                 // The fastest the player can travel in the x-axis
    public float jumpForce = 50f;               // Amount of force added when the player jumps
    public float maxJumpSpeed = 6f;

    public int currentJumpCount = 0;

    public string xCtrl = "Horizontal_P1";
    public string yCtrl = "Vertical_P1";
    public string jumpButton = "Jump_P1";
    public string blockButton = "Fire2_P1";

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
        //Debug.Log(currentJumpCount);

        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        float EPSILON = 1;
        if (isGrounded && System.Math.Abs(GetComponent<Rigidbody2D>().velocity.y) < EPSILON)
        {
            currentJumpCount = 0;
        }

        // If the jump button is pressed and the player is grounded then the player should jump
        if (Input.GetButtonDown(jumpButton) && isGrounded)
        {
            jump = true;
            currentJumpCount++;
        }

        if(Input.GetButtonDown(jumpButton) && currentJumpCount < 2 && !jump)
        {
            doubleJump = true;
            currentJumpCount++;
        }

        if (Input.GetButton(blockButton) && System.Math.Abs(GetComponent<Rigidbody2D>().velocity.y) < EPSILON) // TODO: Possibly fix condition
        {
            anim.SetTrigger("Shield");
            block = true;
            maxSpeed = 2f;
        }

        if(Input.GetButtonUp(blockButton))
        {
            maxSpeed = 6f;
            block = false;
        }
    }

    // FixedUpdate is an update function interdependent 
    void FixedUpdate()
    {
        // Cache the x axis input
        float xInput = Input.GetAxis(xCtrl);

        // The Speed animator parameter is set to the absolute value of the horizontal input
        anim.SetFloat("Speed", Mathf.Abs(xInput));

        // If the player is changing direction (xInput has a different sign to velocity.x) or hasn't reached max speed yet
        if (xInput * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            // add force to the player
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * xInput * moveForce);

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (xInput > 0 && !facingRight)
            Flip();
        else if (xInput < 0 && facingRight)
            Flip();

        float yInput = Input.GetAxis(yCtrl);

        if(jump && !block)
        {
            anim.SetTrigger("Jump");

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            jump = false;
        }

        if (doubleJump && !block)
        {
            anim.SetTrigger("Jump");

            if (yInput * GetComponent<Rigidbody2D>().velocity.y < maxJumpSpeed) 
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > maxJumpSpeed)
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, Mathf.Sign(GetComponent<Rigidbody2D>().velocity.y) * maxJumpSpeed);

            doubleJump = false;
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
