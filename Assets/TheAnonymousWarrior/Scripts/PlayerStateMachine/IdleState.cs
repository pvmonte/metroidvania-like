using UnityEngine;

namespace TheAnonymousWarrior.Scripts.PlayerStateMachine
{
    public class IdleState : IPlayerState
    {
        public void Enter(PlayerController playerController)
        {
            Debug.Log("Enter Idle state");
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

            if (input.Move.ReadValue<Vector2>().magnitude != 0)
            {
                playerController.OnMoveAction();
            }
        }

        public void OnFixedUpdate(PlayerController playerController)
        {
        
        }

        public void Exit(PlayerController playerController)
        {
            Debug.Log("Exit Idle state");
        }
    }
}