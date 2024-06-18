using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float coyoteTime = 0.2f; // Time player can still jump after walking off a ledge
    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;
    private float coyoteTimeCounter;

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset y velocity when grounded
            coyoteTimeCounter = coyoteTime; // Reset coyote time
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Decrease coyote time
        }

        // Get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move player
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Jump logic
        if (Input.GetButtonDown("Jump") && (isGrounded || coyoteTimeCounter > 0f))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            coyoteTimeCounter = 0f; // Reset coyote time after jumping
        }

        // Apply gravity with fall and low jump multipliers
        if (velocity.y < 0) // Falling
        {
            velocity.y += gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (velocity.y > 0 && !Input.GetButton("Jump")) // Ascending and jump button released
        {
            velocity.y += gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else // Normal gravity
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Apply velocity
        controller.Move(velocity * Time.deltaTime);
    }
}
