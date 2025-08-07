using UnityEngine;

namespace TheAnonymousWarrior.Scripts.PlayerStateMachine
{
    public class HurtState : IPlayerState
    {
        private float _timer = 0.25f;
        
        public void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input)
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                playerController.Recover();
            }
        }

        public void OnFixedUpdate(PlayerController playerController)
        {
        
        }
    }
}