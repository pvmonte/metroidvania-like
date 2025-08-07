using UnityEngine;

namespace TheAnonymousWarrior.Scripts.PlayerStateMachine
{
    public class PlayerAirbornAttackState : IPlayerState
    {
        private Vector2 _moveVector;
        private float _timer = 0.35f;
        
        public void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input)
        {
            _moveVector = input.Move.ReadValue<Vector2>();
            
            if (_timer <= 0)
            {
                playerController.OnAirborn();
            }
        }

        public void OnFixedUpdate(PlayerController playerController)
        {
            playerController.OnMoveInput(_moveVector);
        }
    }
}