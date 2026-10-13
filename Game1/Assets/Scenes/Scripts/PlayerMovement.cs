using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;
    
    [Header("Standard Movement")]
    public float speed = 6f;
    public float gravity = -9.81f;

    [Header("Flight Settings")]
    [Tooltip("How fast the player flies straight up while holding Space.")]
    [Range(10f, 100f)]
    public float ascendSpeed = 30f; 
    private Vector3 velocity;

    [Header("Boost Settings")]
    [Tooltip("Burst speed when boosting forward/backward/left/right.")]
    [Range(50f, 300f)]
    public float horizontalBoostSpeed = 150f; 

    [Tooltip("Burst speed when boosting Up (Space/Up Arrow) or Down (Left Shift).")]
    [Range(50f, 300f)]
    public float verticalBoostSpeed = 100f; 
    
    [Tooltip("Friction applied to the boost. LOWER slider = sliding FURTHER distance.")]
    [Range(0.5f, 10f)]
    public float boostFriction = 1.5f; 
    private Vector3 boostVelocity;

    private Vector2 moveInput;

    void Update()
    {
        // 1. Read WASD movement
        moveInput = Keyboard.current != null
            ? new Vector2(
                (Keyboard.current.dKey.isPressed ? 1 : 0) -
                (Keyboard.current.aKey.isPressed ? 1 : 0),
                (Keyboard.current.wKey.isPressed ? 1 : 0) -
                (Keyboard.current.sKey.isPressed ? 1 : 0)
            )
            : Vector2.zero;

        // 2. Convert to world-space movement
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);

        // 3. Omnidirectional & Vertical Boost Mechanic
        // wasPressedThisFrame ensures M must be the final key tapped to execute the boost
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            // Calculate horizontal direction
            Vector3 horizontalDir = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
            
            // Check if upward modifiers (Space OR Up Arrow) or downward modifier (Left Shift) are currently held down
            bool isHoldingUp = Keyboard.current.spaceKey.isPressed || Keyboard.current.upArrowKey.isPressed;
            bool isHoldingDown = Keyboard.current.leftShiftKey.isPressed;

            float verticalInput = (isHoldingUp ? 1 : 0) - (isHoldingDown ? 1 : 0);
            Vector3 verticalDir = transform.up * verticalInput;

            // If no directional keys are held, default to boosting straight forward
            if (horizontalDir == Vector3.zero && verticalInput == 0)
            {
                boostVelocity = transform.forward * horizontalBoostSpeed;
            }
            else
            {
                // Apply separate speeds to the directional vectors and combine them
                boostVelocity = (horizontalDir * horizontalBoostSpeed) + (verticalDir * verticalBoostSpeed);
            }
        }

        // Apply the boost movement
        controller.Move(boostVelocity * Time.deltaTime);

        // Smoothly slow down the boost velocity back to zero using the friction slider
        boostVelocity = Vector3.Lerp(boostVelocity, Vector3.zero, boostFriction * Time.deltaTime);

        // 4. Aerial Jump/Fly Mechanic
        if (Keyboard.current != null && Keyboard.current.spaceKey.isPressed)
        {
            velocity.y = ascendSpeed;
        }

        // 5. Apply gravity over time
        velocity.y += gravity * Time.deltaTime;
        
        // 6. Move the controller vertically
        controller.Move(velocity * Time.deltaTime);
    }
}