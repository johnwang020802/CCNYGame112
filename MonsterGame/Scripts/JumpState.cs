using UnityEngine;


public class JumpState : IPlayerState
{
    private PlayerController player;

    public JumpState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        // Play jump animation
        player.animator.Play("MonsterJump");

        player.rb.linearVelocity =
            new Vector2(player.rb.linearVelocity.x,
                        player.jumpForce);

        Debug.Log("Entered Jump State");
    }

    public void Update()
    {
        float move = player.MoveInput;

        player.rb.linearVelocity =
            new Vector2(
                move * player.moveSpeed,
                player.rb.linearVelocity.y
            );

        //Land only when falling
        if (player.rb.linearVelocity.y <= 0 && player.isGrounded)
        {
            Debug.Log("LANDED");

            if (move != 0)
            {
               
                player.ChangeState(player.walkState);
                return;
            }

            player.ChangeState(player.idleState);
        }
    }

    public void Exit() {
       
        Debug.Log("Leaving Jump State");

    }
}