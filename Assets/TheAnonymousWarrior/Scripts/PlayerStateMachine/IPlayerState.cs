using UnityEngine.InputSystem;

public interface IPlayerState
{
    void Enter(PlayerController playerController);
    void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input);
    void OnFixedUpdate(PlayerController playerController);
    void Exit(PlayerController playerController);
}