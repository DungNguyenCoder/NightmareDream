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

    [Header("Physics Check")]
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private WallDetector _wallDetector;

    [Header("WallMovement")]
    private float _wallSlideSpeed = 2;
    private bool _isWallSliding;

    [SerializeField] private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool dashPressed;

    private int jumpCount;
    private bool isGrounded;
    private bool isDashing;
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
        WallSlide();

        if (!isDashing) MoveHorizontal();
        CheckPhysics();
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
            if (!_isWallSliding)
                jumpCount++;
            else
            {
                jumpCount = 1;
            }

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
    private void WallSlide()
    {
        if (!isGrounded && _wallDetector.WallCheck() && moveInput.x != 0)
        {
            _isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -_wallSlideSpeed));
        }
        else
        {
            _isWallSliding = false;
        }
    }
    private void CheckPhysics()
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