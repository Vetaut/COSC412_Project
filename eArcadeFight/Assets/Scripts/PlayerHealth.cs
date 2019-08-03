using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;             // The players health
    public float repeatDamagePeriod = 2f;   // How frequently the player can be damaged
    public float hurtForce = 10f;           // The force with which the player is pushed when hurt
    public float damageAmount = 10f;        // The amount of damagee to take when enemies touch the player

    private SpriteRenderer healthBar;       // Reference to the sprite renderer of the health bar
    private float lastHitTime;              // The time at which the player was last hit
    private Vector3 healthScale;            // The local scale of the health bar initially (with full health)
    private Knight knightScript; // Reference to the PlayerController Script
    private Animator anim;                  // Reference to the Animator on the player
	//private GameManager gameManager;

    void Awake()
    {
        // Setting up references
        knightScript = GetComponent<Knight>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        anim = this.transform.Find("model").GetComponent<Animator>();

        // Getting the intial scale of the healthbae (while player is at full health)
        healthScale = healthBar.transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Entered Collision with " + collision.gameObject.tag);
        // If the colliding gameobject is an Enemy...
        if(collision.gameObject.tag == "Enemy")
        {
            // and if the time exceeeds the time of the last hit plus the time between hits
            if (Time.time > lastHitTime + repeatDamagePeriod)
            {
                // and if the player still has health
                if(health > 0f)
                {
                    // take damage and reset lastHitTime
                    TakeDamage(collision.transform);
                    lastHitTime = Time.time;
                }
                // If the player doesnt have health die
                else
                {
                    anim.SetTrigger("Die");
                    GameObject UI_HP = GameObject.Find("UI_HealthBar");
                    Destroy(UI_HP);
                    Destroy(this.gameObject);
                    //gameManager.DecreasePlayerCount(1);
                }
            }
        }
    }

    public void TakeDamage (Transform enemy)
    {
        // Make sure the player can't jump
        //knightScript.jump = false;

        Vector3 hurtDirection = new Vector3(0, 0, 0);

        hurtDirection.x = (transform.position.x - enemy.position.x) / Mathf.Abs(transform.position.x - enemy.position.x);

        // Create a vector that's from the enemy to the player with an upwards boost
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f + (hurtDirection * 5f);

        // Add a force to the player in the direction of the vector and multiply by the hurtforce
        GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

        // Reduce the players health by 10
        health -= damageAmount;

        // Update what the health bar looks like
        UpdateHealthBar();

    }

    public void TakeDamage ()
    {
        if (Time.time > lastHitTime + repeatDamagePeriod)
        {
            // and if the player still has health
            if (health > 0f)
            {
                lastHitTime = Time.time;
            }
            // If the player doesnt have health die
            else
            {
                anim.SetTrigger("Die");
                GameObject UI_HP = GameObject.Find("UI_HealthBar");
                Destroy(UI_HP);
                Destroy(this.gameObject);
                //gameManager.DecreasePlayerCount(1);
            }
        }

        // Reduce the players health by 10
        health -= damageAmount;

        // Update what the health bar looks like
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        // Set the health bar's color to proportion of the way between greeen and red based on players health
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

        // Set the scale of the health bar to be proportional to the player's health
        healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
    }
}
