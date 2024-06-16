using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

[RequireComponent(typeof(CharacterController))]

public class FPSPlayerController : MonoBehaviour
{
    // Declare variables
    
    // Movement
    public float walkSpeed = 7.5f;
    public float runSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    // Player view
    public Camera playerCam;
    public float lookSpd = 20.0f;
    public float lookXLimit = 45.0f;

    // Slide
    public float slideSpd = 2.0f;
    public float slideDuration = 0.0f;
    public float slideCooldown = 2.0f;

    private bool isSliding;
    private float slideTime;
    private float slideCooldownTimer;

    // Wall Jumps
    public LayerMask wallLayer;
    public float wallJumpForce = 10.0f;
    private bool isTouchingWall = false;

    private float wallJumpCooldown = 1.2f;
    private bool canWallJump = true;

    // Character Control
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        IsPlayerTouchingWall();
    }

    void Move()
    {
        // We are grounded, calculate move direction based on axis
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Left Shift to Sprint
        bool isRun = Input.GetKey(KeyCode.LeftShift);
        float curSpdX = canMove ? (isRun ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpdY = canMove ? (isRun ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpdX) + (right * curSpdY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }


        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        
        if (Input.GetButton("Jump") && canMove && isTouchingWall && !characterController.isGrounded && canWallJump)
        {
            moveDirection.y = wallJumpForce;

            // This adds a bit of horizontal force opposite the wall for a more dynamic jump
            if (Physics.Raycast(transform.position, transform.right, 1f, wallLayer))
            {
                moveDirection += -transform.right * wallJumpForce / 1.5f; // Adjust the divisor for stronger/weaker push.
            }
            else if (Physics.Raycast(transform.position, -transform.right, 1f, wallLayer))
            {
                moveDirection += transform.right * wallJumpForce / 1.5f;
            }

            canWallJump = false;
            StartCoroutine(CooldownWallJump());
        }
        else if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

         // Move Controller
         characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpd;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpd, 0);
        }
    }

    void IsPlayerTouchingWall()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.right, out hit, 1f, wallLayer) || 
            Physics.Raycast(transform.position, -transform.right, out hit, 1f, wallLayer))
        {
            isTouchingWall = true;
        }
        else
        {
            isTouchingWall = false;
        }
    }

    IEnumerator CooldownWallJump()
    {
        yield return new WaitForSeconds(wallJumpCooldown);
        canWallJump = true;
    }
}
