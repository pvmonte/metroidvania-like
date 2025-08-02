using UnityEngine;

namespace TheAnonymousWarrior.Scripts.PlayerStateMachine
{
    public class PlayerAirbornAttackState : IPlayerState
    {
        private Vector2 _moveVector;
        
        public void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input)
        {
            _moveVector = input.Move.ReadValue<Vector2>();
        }

        public void OnFixedUpdate(PlayerController playerController)
        {
            playerController.OnMoveInput(_moveVector);
        }
    }
}