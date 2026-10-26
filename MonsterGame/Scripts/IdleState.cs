using UnityEngine;
using UnityEngine.InputSystem;

/// Player is standing still.

public class IdleState : IPlayerState
{
    private PlayerController player;

    public IdleState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        // Play the idle animation when entering the Idle state
        player.animator.Play("MonsterIdle");

        Debug.Log("Entered Idle State");
    }

    public void Update()
    {
       // Debug.Log("Idle Move = " + player.MoveInput);

        if (player.DashPressed)
        {
            Debug.Log("IDLE -> DASH");
            player.ChangeState(player.dashState);
            return; 
        }

        if (player.MoveInput != 0)
        {
            Debug.Log("IDLE -> WALK");
            player.ChangeState(player.walkState);
            return;
        }

        if (player.JumpPressed && player.isGrounded)
        {
            player.ChangeState(player.jumpState);
            return;
        }

        
    }

    public void Exit()
    {
        // Called before transitioning to another state
        Debug.Log("Leaving Idle State");
    }
}