using UnityEngine;

public class DashState : IPlayerState
{
    private PlayerController player;

    private float timer;

    // Store dash direction
    private float dashDirection;

    public DashState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animator.Play("MonsterDash");

        timer = player.dashDuration;

        // Save direction once
        /*dashDirection = player.MoveInput;

         if (dashDirection == 0)
         {
             dashDirection = 1;
         }
        */

        dashDirection = player.FacingDirection;

        Debug.Log("Entered Dash State");
        Debug.Log("FacingDirection = " + player.FacingDirection);
        Debug.Log("Dash Direction = " + dashDirection);
    }

    public void Update()
    {
        // Keep same direction entire dash
        player.rb.linearVelocity =
            new Vector2(
                dashDirection * player.dashSpeed,
                player.rb.linearVelocity.y
            );

        timer -= Time.deltaTime;

        // Debug.Log("Dash Timer = " + timer);

        if (Mathf.RoundToInt(timer * 10) % 5 == 0)
        {
            Debug.Log("Timer = " + timer);
        }

        if (timer <= 0)
        {
            Debug.Log("Dash Finished");

            if (!player.isGrounded)
            {
                player.ChangeState(player.jumpState);
                return;
            }

            if (player.MoveInput != 0)
            {
                player.ChangeState(player.walkState);
            }
            else
            {
                player.ChangeState(player.idleState);
            }
        }
    }

    public void Exit()
    {
        Debug.Log("Leaving Dash State");
    }
}