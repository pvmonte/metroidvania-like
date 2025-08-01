using UnityEngine;

namespace TheAnonymousWarrior.Scripts.PlayerStateMachine
{
    public class RunState : IPlayerState
    {
        private Vector2 moveVector;

        public void Enter(PlayerController playerController)
        {
            Debug.Log("Enter Run state");
        }

        public void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input)
        {
            if (input.Attack.triggered)
            {
                playerController.OnAttackAction();
            }

            if (input.Jump.triggered)
            {
                playerController.OnJumpAction();
            }

            moveVector = input.Move.ReadValue<Vector2>();
        }

        public void OnFixedUpdate(PlayerController playerController)
        {
            playerController.OnMoveInput(moveVector);

            if (moveVector.magnitude != 0) return;
            
            playerController.OnIdle();
        }

        public void Exit(PlayerController playerController)
        {
            Debug.Log("Exit Run state");
        }
    }
}