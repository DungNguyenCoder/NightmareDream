using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private ParticleSystem dust;
    private int _maxJumpCount = 2;
    private bool jumpPressed;
    private int jumpCount;

    [Header("Dash")]
    [SerializeField] private float _dashSpeed = 20f;
    [SerializeField] private float _dashTime = 0.15f;
    [SerializeField] private TrailRenderer dashTrail;
    private int _maxDashCount = 1;
    private bool dashPressed;
    private bool isDashing;
    private int dashCount;
    private float dashTimer;

    [Header("Physics Check")]
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private WallDetector _wallDetector;

    [Header("WallMovement")]
    private float _wallSlideSpeed = 2;
    private bool _isWallSliding;

    [Header("Audio")]
    private float runSoundDelay = 0.35f;
    private float runSoundTimer;

    [SerializeField] private Animator anim;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private SpawnNightmare _nightmare;
    [SerializeField] private ScreenFader screenFader;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private bool isGrounded;
    public bool isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTrail.emitting = false;
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
        if (isDead) return;

        HandleJump();
        HandleDash();
        UpdateAnimator();
    }
    private void FixedUpdate()
    {
        if (isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        _groundDetector?.Check();
        WallSlide();

        if (!isDashing) MoveHorizontal();
        CheckPhysics();
    }
    private void MoveHorizontal()
    {
        rb.linearVelocity = new Vector2(moveInput.x * _moveSpeed, rb.linearVelocity.y);
        if (moveInput.x != 0)
        {
            if(isGrounded)
            {
                if (runSoundTimer <= 0f)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.run);
                    runSoundTimer = runSoundDelay;
                }
                runSoundTimer -= Time.fixedDeltaTime;
            }
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
        }
        else
        {
            runSoundTimer = 0f;
        }
    }

    private void HandleJump()
    {
        if (!_groundDetector.WasGroundedLastFrame && _groundDetector.IsGrounded)
            jumpCount = 0;

        if (jumpPressed && jumpCount < _maxJumpCount)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
            dust.Play();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, _jumpForce);
            if (!_isWallSliding)
                jumpCount++;
            else
            {
                jumpCount = 1;
            }

            if (jumpCount == 1)
            {
                anim.SetTrigger(GameConfig.ANIM_COL_JUMP);
            }
            else
                anim.SetTrigger(GameConfig.ANIM_COL_DOUBLE_JUMP);
        }
        jumpPressed = false;
    }
    private void HandleDash()
    {
        if (_groundDetector.IsGrounded)
            dashCount = 0;
        if (dashPressed && !isDashing && dashCount < _maxDashCount)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.dash);
            ++dashCount;
            isDashing = true;
            dashTimer = _dashTime;
            dashTrail.Clear();
            dashTrail.emitting = true;
            anim.SetBool(GameConfig.ANIM_COL_DASHING, true);
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            rb.linearVelocity = new Vector2(transform.localScale.x * _dashSpeed, 0);

            if (dashTimer <= 0)
            {
                isDashing = false;
                dashTrail.emitting = false;
                anim.SetBool(GameConfig.ANIM_COL_DASHING, false);
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
        anim.SetFloat(GameConfig.ANIM_COL_RUN, Mathf.Abs(moveInput.x));
        anim.SetFloat(GameConfig.ANIM_COL_YVERLOCITY, rb.linearVelocity.y);
        anim.SetBool(GameConfig.ANIM_COL_IS_GROUNDED, isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag(GameConfig.TAG_ENEMY) || collision.CompareTag(GameConfig.TAG_OBSTACLE)) && !isDead)
        {
            Debug.Log("Dead");
            Death();
        }
    }
    private void Death()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.dead);
        EventManager.onUpdateDeadCount?.Invoke();
        isDead = true;

        moveInput = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        anim.SetBool(GameConfig.ANIM_COL_IS_DEATH, true);
        anim.SetBool(GameConfig.ANIM_COL_IS_GROUNDED, true);
        anim.SetFloat(GameConfig.ANIM_COL_YVERLOCITY, 0f);

        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        _nightmare.IsActiveXNightmare(false);
        yield return screenFader.FadeOut();

        _nightmare.SetStateNightmare();
        _nightmare.IsActiveXNightmare(true);

        transform.position = respawnPoint.position;

        rb.simulated = true;
        isDead = false;

        anim.SetBool(GameConfig.ANIM_COL_IS_DEATH, false);
        anim.Play("Idle");

        jumpCount = 0;
        dashCount = 0;
        cameraController.transform.position = new Vector3(0, 0, -10);
        cameraController._minPos = new Vector3(0, 0, -10);
        cameraController._maxPos = new Vector3(0, 0, -10);
        PlayerRecorder.Instance.snapshots.Clear();
        yield return screenFader.FadeIn(1f);
        
    }
}