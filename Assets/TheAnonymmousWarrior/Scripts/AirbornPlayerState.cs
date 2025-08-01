using UnityEngine;
using UnityEngine.InputSystem;

public class AirbornPlayerState : IPlayerState
{
    public void Enter(PlayerController playerController)
    {
        Debug.Log("Enter Airborn state");
        
    }

    public void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input)
    {
        var moveVector = input.Move.ReadValue<Vector2>();

        if (moveVector.magnitude != 0)
        {
            playerController.OnMoveInput(moveVector);
        }
    }

    public void Exit(PlayerController playerController)
    {
        Debug.Log("Exit Airborn state");
    }
}