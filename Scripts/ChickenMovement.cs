using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class ChickenMovement : MonoBehaviour
{
    public float speed = 5f;             // Speed of the chicken
    public float jumpForce = 5f;         // Force applied for jumping
    public float minX = -20f;            // Minimum x-axis boundary
    public float maxX = 20f;             // Maximum x-axis boundary
    private bool isGrounded = true;      // Check if the chicken is on the ground
    public ScoreCounter scoreCounter;    // For keeping track of score

    private Rigidbody rb;

    private void Start()
    {
        // Get the Rigidbody component for physics interactions
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent the chicken from rotating uncontrollably

        // Dynamically find and assign scoreCounter
        scoreCounter = FindObjectOfType<ScoreCounter>();
    }

    private void Update()
    {
        // Capture horizontal input for left/right movement
        float movementInput = Input.GetAxis("Horizontal");

        // Calculate movement on the x-axis
        Vector3 movement = new Vector3(movementInput * speed, rb.velocity.y, rb.velocity.z);

        // Move the chicken on the x-axis
        rb.velocity = new Vector3(movementInput * speed, rb.velocity.y, rb.velocity.z);

        // Clamp position within the specified x-axis boundaries
        float clampedX = Mathf.Clamp(rb.position.x, minX, maxX);
        rb.position = new Vector3(clampedX, rb.position.y, rb.position.z);

        // Jumping logic
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the chicken has landed on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // Checks if a boulder has been hit
        if (collision.gameObject.CompareTag("Boulder"))
        {
            Debug.Log("Boulder Hit! Transitioning to Failing Scene...");
            SceneManager.LoadScene("Failing");
        }
    }

    // Score tracking for coins
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);

            if (collision.gameObject.name.StartsWith("Coin 3"))
            {
                ScoreCounter.score += 300;
            }
            else if (collision.gameObject.name.StartsWith("Coin 2"))
            {
                ScoreCounter.score += 200;
            }
            else if (collision.gameObject.name.StartsWith("Coin"))
            {
                ScoreCounter.score += 100;
            }
        }
    }
}
