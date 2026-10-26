using UnityEngine;
using UnityEngine.InputSystem;


/// Main player controller.
/// Manages movement data, references to sprites, and switching between states.

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;


    // References to Unity components
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public Animator animator;

    // Tracks if the player is touching the ground
    [HideInInspector] public bool isGrounded;

    // State objects
    public IdleState idleState;
    public WalkState walkState;
    public JumpState jumpState;
    public DashState dashState;

    // Current active state
    private IPlayerState currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        idleState = new IdleState(this);
        walkState = new WalkState(this);
        jumpState = new JumpState(this);
        dashState = new DashState(this);

        ChangeState(idleState);
    }

    public float MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool DashPressed { get; private set; }

    // Remembers the last direction the monster faced
    public float FacingDirection { get; private set; } = 1;

    private void Update()
    {
        // Let the current state run its own logic
        MoveInput = 0;

        if (Keyboard.current.aKey.isPressed)
        { 
           MoveInput = -1;
            FacingDirection = -1;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            MoveInput = 1;
            FacingDirection = 1;

        } 

      // Debug.Log("Player MoveInput = " + MoveInput);

        JumpPressed =
       Keyboard.current.spaceKey.wasPressedThisFrame;

        DashPressed =
            Keyboard.current.leftShiftKey.wasPressedThisFrame;

        currentState.Update();
    }


    /// Switches from one state to another.
    public void ChangeState(IPlayerState newState)
    {
        // Clean up current state
        currentState?.Exit();

        // Switch reference
        currentState = newState;

        // Initialize new state
        currentState.Enter();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}