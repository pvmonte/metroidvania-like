using UnityEngine;

namespace TheAnonymousWarrior.Scripts.PlayerStateMachine
{
    public class IdleState : IPlayerState
    {
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

            if (input.Move.ReadValue<Vector2>().x != 0)
            {
                playerController.OnMoveAction();
            }
        }

        public void OnFixedUpdate(PlayerController playerController)
        {
        
        }
    }
}