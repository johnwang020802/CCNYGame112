using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;  // How fast the player moves

    void Update()
    {
        // WASD movement using the new Input System asked copilot
        float x =
            (Keyboard.current.aKey.isPressed ? -1 :
            Keyboard.current.dKey.isPressed ? 1 : 0);

        float y =
            (Keyboard.current.sKey.isPressed ? -1 : 
            Keyboard.current.wKey.isPressed ? 1 : 0);


        // Create a movement direction
        Vector3 move = new Vector3(x, y, 0f);

        // Move the player every frame
        transform.position += move * speed * Time.deltaTime;
    }
}

