using UnityEngine;

namespace TheAnonymousWarrior.Scripts.PlayerStateMachine
{
    public class AttackState : IPlayerState
    {
        private float _timer = 0.45f;
        
        public void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                playerController.OnIdle();
            }
        }

        public void OnFixedUpdate(PlayerController playerController)
        {
        
        }
    }
}