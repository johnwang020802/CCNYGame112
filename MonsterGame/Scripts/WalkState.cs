using UnityEngine;
using UnityEngine.InputSystem;


public class WalkState : IPlayerState
{
    private PlayerController player;

    public WalkState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        // Play walk animation when entering Walk state
        player.animator.Play("MonsterWalk");

        Debug.Log("Entered Walk State");
    }

    public void Update()
    {
        float move = player.MoveInput;

       // Debug.Log("Walk Move = " + move);

        player.rb.linearVelocity =
            new Vector2(
                move * player.moveSpeed,
                player.rb.linearVelocity.y
            );

       // Debug.Log("Position = " + player.transform.position);

        if (player.JumpPressed && player.isGrounded)
        {
            player.ChangeState(player.jumpState);
            return;
        }

        if (player.DashPressed)
        {
            Debug.Log("WALK -> DASH");
            player.ChangeState(player.dashState);
            return;
        }

        if (move == 0)
        {
            Debug.Log("WALK -> IDLE");
            player.ChangeState(player.idleState);
        }
    }

    public void Exit() {

        Debug.Log("Leaving Walk State");
    }
}
