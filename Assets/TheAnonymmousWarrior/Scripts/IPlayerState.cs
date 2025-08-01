using UnityEngine.InputSystem;

public interface IPlayerState
{
    void Enter(PlayerController playerController);
    void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input);
    void Exit(PlayerController playerController);
}