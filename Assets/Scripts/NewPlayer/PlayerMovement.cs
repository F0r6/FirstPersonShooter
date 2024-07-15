using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    //public float speed = 5.0f;
    //public float sprintSpd = 8.0f;
    //
    //public float slideSpeed = 10.0f;
    //public float slideDist = 10.0f;
    //public float slideDuration = 1.0f;
    //private bool isSliding = false;
    //private float crouchHeight = 1.0f;
    //private float originalHeight;
    //public bool isCrouching = false;
    //
    //public float jumpHeight = 1.5f;
    //public float gravity = -9.81f;
    //public float fallMultiplier = 2.5f;
    //public float lowJumpMultiplier = 2f;
    //public float coyoteTime = 0.2f; // Time player can still jump after walking off a ledge
    //
    //public CharacterController controller;
    //public Transform groundCheck;
    //public float groundDistance = 0.4f;
    //public LayerMask groundMask;
    //
    //private Vector3 velocity;
    //public bool isGrounded;
    //private bool canJump;
    //private float coyoteTimeCounter;
    //private float jumpBufferCounter;
    //
    //private Vector3 lastPos;
    //private Vector3 slideStartPosition;
    //
    //
    //private void Start()
    //{
    //    originalHeight = controller.height;
    //}
    //
    //void Update()
    //{
    //    // Ground check
    //    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    //
    //    // Debug log for ground detection
    //    if (isGrounded)
    //    {
    //        Debug.Log("Grounded!");
    //    }
    //    else
    //    {
    //        Debug.Log("Not Grounded!");
    //    }
    //
    //    if (isGrounded && velocity.y < 0)
    //    {
    //        velocity.y = -2f; // Reset y velocity when grounded
    //        canJump = true; // Reset jumping ability
    //        coyoteTimeCounter = coyoteTime; // Reset coyote time
    //    }
    //    else
    //    {
    //        coyoteTimeCounter -= Time.deltaTime; // Decrease coyote time
    //    }
    //
    //    // Get input
    //    float x = Input.GetAxis("Horizontal");
    //    float z = Input.GetAxis("Vertical");
    //
    //    // Determine movement speed
    //    float currentSpeed = speed;
    //    if (Input.GetKey(KeyCode.LeftShift) && !isSliding && !isCrouching)
    //    {
    //        currentSpeed = sprintSpd; // Sprinting
    //    }
    //
    //    // Move player
    //    Vector3 move = transform.right * x + transform.forward * z;
    //    controller.Move(move * currentSpeed * Time.deltaTime);
    //
    //    // Apply velocity
    //    controller.Move(velocity * Time.deltaTime);
    //
    //
    //    // Jump and Slide Mechanic
    //    Jump();
    //    CrouchSlide();
    //
    //    lastPos = controller.transform.position;
    //
    //    // Adjust movement speed while crouching
    //    if (isCrouching)
    //    {
    //        currentSpeed = speed / 4f; // Adjust as needed
    //    }
    //}
    //
    //void Jump()
    //{
    //    // Jump logic
    //    if (Input.GetButtonDown("Jump") && canJump && (isGrounded || coyoteTimeCounter > 0f))
    //    {
    //        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    //        canJump = false; // Prevent double jumping
    //    }
    //
    //    if (!isGrounded)
    //    {
    //        canJump = false; // Prevent jumping while in air
    //    }
    //
    //    // Apply gravity with fall and low jump multipliers
    //    if (velocity.y < 0) // Falling
    //    {
    //        velocity.y += gravity * (fallMultiplier) * Time.deltaTime;
    //    }
    //    else if (velocity.y > 0 && !Input.GetButton("Jump")) // Ascending and jump button released
    //    {
    //        velocity.y += gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
    //    }
    //    else // Normal gravity
    //    {
    //        velocity.y += gravity * Time.deltaTime;
    //    }
    //}
    //
    //void CrouchSlide()
    //{
    //    // Slide logic
    //    if (Input.GetKeyDown(KeyCode.C) && !isSliding && controller.transform.position != lastPos)
    //    {
    //        StartCoroutine(Slide());
    //    }
    //
    //    // Crouching input detection
    //    if (Input.GetKeyDown(KeyCode.C) && !isCrouching && controller.transform.position == lastPos)
    //    {
    //        StartCoroutine(EnterCrouch());
    //    }
    //    else if (!Input.GetKey(KeyCode.C) && isCrouching)
    //    {
    //        StartCoroutine(ExitCrouch());
    //    }
    //}
    //
    //private IEnumerator Slide()
    //{
    //    isSliding = true;
    //    isCrouching = true;
    //    slideStartPosition = transform.position;
    //    controller.height = crouchHeight;
    //    float currentSpeed = slideSpeed;
    //
    //    // Move player while sliding
    //    Vector3 slideDirection = transform.forward;
    //
    //    while (Input.GetKey(KeyCode.C) && Vector3.Distance(slideStartPosition, transform.position) < slideDist)
    //    {
    //        controller.Move(slideDirection * currentSpeed * Time.deltaTime);
    //        yield return null;
    //    }
    //
    //    // Revert speed after sliding, stay crouched
    //    isSliding = false;
    //    if (!Input.GetKey(KeyCode.C))
    //    {
    //        StartCoroutine(ExitCrouch());
    //    }
    //}
    //
    //private IEnumerator EnterCrouch()
    //{
    //    float startTime = Time.time;
    //    float duration = 0.1f; // Duration to reach crouch height, adjust as needed
    //    isCrouching = true;
    //
    //    while (Time.time < startTime + duration)
    //    {
    //        float t = (Time.time - startTime) / duration;
    //        controller.height = Mathf.Lerp(originalHeight, crouchHeight, t);
    //        yield return null;
    //    }
    //
    //    controller.height = crouchHeight;
    //
    //}
    //
    //private IEnumerator ExitCrouch()
    //{
    //    float revertStartTime = Time.time;
    //    float revertDuration = 0.1f; // Duration to revert height, adjust as needed
    //    isCrouching = false;
    //
    //    while (Time.time < revertStartTime + revertDuration)
    //    {
    //        float t = (Time.time - revertStartTime) / revertDuration;
    //        controller.height = Mathf.Lerp(crouchHeight, originalHeight, t);
    //        yield return null;
    //    }
    //
    //    controller.height = originalHeight;
    //}
    //
    //void OnDrawGizmosSelected()
    //{
    //    // Draw a wire sphere to visualize ground check position and distance
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    //}

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask WhatIsGround;
    bool isGrounded;

    [Header("Slope Handle")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MoveState state;
    public enum MoveState
    {
        WALKING,
        SPRINTING,
        CROUCHING,
        SLIDING,
        AIR
    }

    public bool isSliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        // Ground Check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, WhatIsGround);

        playerInput();
        SpeedControl();
        StateHandler();

        // Handle Drag
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void playerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // When to jump
        if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Start Crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5.0f, ForceMode.Impulse);
        }

        // End Crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        // Mode - Sliding
        if (isSliding)
        {
            state = MoveState.SLIDING;

            if (OnSlope() && rb.velocity.y < 0.1f)
                desiredMoveSpeed = slideSpeed;

            else
                desiredMoveSpeed = sprintSpeed;
        }

        // Mode - Crouching
        else if (Input.GetKey(crouchKey))
        {
            state = MoveState.CROUCHING;
            desiredMoveSpeed = crouchSpeed;
        }

        // Mode - Sprinting
        else if (isGrounded && Input.GetKey(sprintKey))
        {
            state = MoveState.SPRINTING;
            desiredMoveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (isGrounded)
        {
            state = MoveState.WALKING;
            desiredMoveSpeed = walkSpeed;
        }

        // Move - Air
        else
        {
            state = MoveState.AIR;
        }

        // Check if desiredMoveSpeed has changed drastically
        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            moveSpeed = desiredMoveSpeed;
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (OnSlope())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else
                time += Time.deltaTime * speedIncreaseMultiplier;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void Move()
    {
        // Calculate move direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // On Slope
        if (OnSlope() && !exitSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * moveSpeed * 20.0f, ForceMode.Force);

            if (rb.velocity.y > 0) // Change 80----------------------------------------------------------------
                rb.AddForce(Vector3.down * 50.0f, ForceMode.Force);
        }

        // On Ground
        else if (isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f, ForceMode.Force);

        // In Air
        else if (!isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10.0f * airMultiplier, ForceMode.Force);
        
        // Turn Gravity off while on slope
        rb.useGravity = !OnSlope();

    }

    private void SpeedControl()
    {
        // Limit on slope
        if (OnSlope() && !exitSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // Limit on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // Limit velocity
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel =  flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

    }

    private void Jump()
    {
        exitSlope = true;   

        // Reset Y Velocity
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        exitSlope = false;
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f +  0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}

