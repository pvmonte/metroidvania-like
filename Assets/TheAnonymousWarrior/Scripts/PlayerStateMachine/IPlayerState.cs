using UnityEngine.InputSystem;

public interface IPlayerState
{
    void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input);
    void OnFixedUpdate(PlayerController playerController);
}