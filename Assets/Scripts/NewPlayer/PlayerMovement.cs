using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float sprintSpd = 8.0f;

    public float slideSpeed = 10.0f;
    public float slideDist = 10.0f;
    public float slideDuration = 1.0f;
    private bool isSliding = false;
    private float crouchHeight = 1.0f;
    private float originalHeight;
    public bool isCrouching = false;

    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float coyoteTime = 0.2f; // Time player can still jump after walking off a ledge

    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    public bool isGrounded;
    private bool canJump;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    private Vector3 lastPos;
    private Vector3 slideStartPosition;


    private void Start()
    {
        originalHeight = controller.height;
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Debug log for ground detection
        if (isGrounded)
        {
            Debug.Log("Grounded!");
        }
        else
        {
            Debug.Log("Not Grounded!");
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset y velocity when grounded
            canJump = true; // Reset jumping ability
            coyoteTimeCounter = coyoteTime; // Reset coyote time
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Decrease coyote time
        }

        // Get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Determine movement speed
        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) && !isSliding && !isCrouching)
        {
            currentSpeed = sprintSpd; // Sprinting
        }

        // Move player
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Apply velocity
        controller.Move(velocity * Time.deltaTime);


        // Jump and Slide Mechanic
        Jump();
        CrouchSlide();

        lastPos = controller.transform.position;

        // Adjust movement speed while crouching
        if (isCrouching)
        {
            currentSpeed = speed / 4f; // Adjust as needed
        }
    }

    void Jump()
    {
        // Jump logic
        if (Input.GetButtonDown("Jump") && canJump && (isGrounded || coyoteTimeCounter > 0f))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canJump = false; // Prevent double jumping
        }

        if (!isGrounded)
        {
            canJump = false; // Prevent jumping while in air
        }

        // Apply gravity with fall and low jump multipliers
        if (velocity.y < 0) // Falling
        {
            velocity.y += gravity * (fallMultiplier) * Time.deltaTime;
        }
        else if (velocity.y > 0 && !Input.GetButton("Jump")) // Ascending and jump button released
        {
            velocity.y += gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        else // Normal gravity
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    void CrouchSlide()
    {
        // Slide logic
        if (Input.GetKeyDown(KeyCode.C) && !isSliding && controller.transform.position != lastPos)
        {
            StartCoroutine(Slide());
        }

        // Crouching input detection
        if (Input.GetKeyDown(KeyCode.C) && !isCrouching && controller.transform.position == lastPos)
        {
            StartCoroutine(EnterCrouch());
        }
        else if (!Input.GetKey(KeyCode.C) && isCrouching)
        {
            StartCoroutine(ExitCrouch());
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        isCrouching = true;
        slideStartPosition = transform.position;
        controller.height = crouchHeight;
        float currentSpeed = slideSpeed;

        // Move player while sliding
        Vector3 slideDirection = transform.forward;

        while (Input.GetKey(KeyCode.C) && Vector3.Distance(slideStartPosition, transform.position) < slideDist)
        {
            controller.Move(slideDirection * currentSpeed * Time.deltaTime);
            yield return null;
        }

        // Revert speed after sliding, stay crouched
        isSliding = false;
        if (!Input.GetKey(KeyCode.C))
        {
            StartCoroutine(ExitCrouch());
        }
    }

    private IEnumerator EnterCrouch()
    {
        float startTime = Time.time;
        float duration = 0.1f; // Duration to reach crouch height, adjust as needed
        isCrouching = true;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            controller.height = Mathf.Lerp(originalHeight, crouchHeight, t);
            yield return null;
        }

        controller.height = crouchHeight;
        
    }

    private IEnumerator ExitCrouch()
    {
        float revertStartTime = Time.time;
        float revertDuration = 0.1f; // Duration to revert height, adjust as needed
        isCrouching = false;

        while (Time.time < revertStartTime + revertDuration)
        {
            float t = (Time.time - revertStartTime) / revertDuration;
            controller.height = Mathf.Lerp(crouchHeight, originalHeight, t);
            yield return null;
        }

        controller.height = originalHeight;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere to visualize ground check position and distance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
