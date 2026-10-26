using UnityEngine;

/// Common contract for all player states.
/// Allows the PlayerController to treat Idle, Walk, Jump, and Dash states the same way. 
public interface IPlayerState
{
    /// Called once when entering the state.
    void Enter();

    /// Called every frame while the state is active.
    void Update();

    /// Called once when leaving the state.
    void Exit();
}
