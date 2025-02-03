using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public float jumpCutMultiplier = 0.5f;
    public float maxJumpDuration = 0.3f;

    float horizontalMove = 0f;
    bool jump = false;
    bool isJumping = false;
    bool jumpWasPressed = false;
    float jumpTimeCounter;
    Rigidbody2D rb;

    void Start()
    {
        if (controller == null)
        {
            controller = GetComponent<CharacterController2D>();
        }
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (controller.Grounded && !Input.GetButton("Jump"))
        {
            jumpWasPressed = false;
        }

        if (Input.GetButtonDown("Jump") && controller.Grounded && !jumpWasPressed)
        {
            jump = true;
            isJumping = true;
            jumpWasPressed = true;
            jumpTimeCounter = maxJumpDuration;
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
