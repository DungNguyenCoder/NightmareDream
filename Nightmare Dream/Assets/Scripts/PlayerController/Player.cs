using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 10f;
    private int _maxJumpCount = 2;

    [Header("Dash")]
    [SerializeField] private float _dashSpeed = 20f;
    [SerializeField] private float _dashTime = 0.15f;
    private int _maxDashCount = 1;

    [Header("Ground Check")]
    [SerializeField] private GroundDetector _groundDetector;

    [SerializeField] private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool dashPressed;

    private bool isGrounded;
    private bool isDashing;
    private int jumpCount;
    private int dashCount;
    private float dashTimer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        jumpPressed = true;
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        dashPressed = true;
    }
    private void Update()
    {
        HandleJump();
        HandleDash();
        UpdateAnimator();
    }
    private void FixedUpdate()
    {
        _groundDetector?.Check();
        CheckGround();
        if (!isDashing)
            MoveHorizontal();
    }
    private void MoveHorizontal()
    {
        rb.linearVelocity = new Vector2(moveInput.x * _moveSpeed, rb.linearVelocity.y);

        if (moveInput.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
    }
    private void HandleJump()
    {
        if (!_groundDetector.WasGroundedLastFrame && _groundDetector.IsGrounded)
            jumpCount = 0;

        if (jumpPressed && jumpCount < _maxJumpCount)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, _jumpForce);
            jumpCount++;

            if (jumpCount == 1)
            {
                anim.SetTrigger("Jump");
            }
            else
                anim.SetTrigger("DoubleJump");
        }
        jumpPressed = false;
    }
    private void HandleDash()
    {
        if (_groundDetector.IsGrounded) 
            dashCount = 0;
        if (dashPressed && !isDashing && dashCount < _maxDashCount)
        {
            ++dashCount;
            isDashing = true;
            dashTimer = _dashTime;
            anim.SetBool("IsDashing", true);
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            rb.linearVelocity = new Vector2(transform.localScale.x * _dashSpeed, 0);

            if (dashTimer <= 0)
            {
                isDashing = false;
                anim.SetBool("IsDashing", false);
            }
        }
        dashPressed = false;
    }
    private void CheckGround()
    {
        isGrounded = _groundDetector.IsGrounded;
    }
    private void UpdateAnimator()
    {
        anim.SetFloat("Run", Mathf.Abs(moveInput.x));
        anim.SetFloat("YVelocity", rb.linearVelocity.y);
        anim.SetBool("IsGrounded", isGrounded);
    }
}