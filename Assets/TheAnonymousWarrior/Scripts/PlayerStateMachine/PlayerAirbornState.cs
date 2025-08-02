using UnityEngine;

namespace TheAnonymousWarrior.Scripts.PlayerStateMachine
{
    public class PlayerAirbornState : IPlayerState
    {
        private Vector2 moveVector;

        public void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input)
        {
            moveVector = input.Move.ReadValue<Vector2>();
        }

        public void OnFixedUpdate(PlayerController playerController)
        {
            playerController.OnMoveInput(moveVector);
        }
    }
}