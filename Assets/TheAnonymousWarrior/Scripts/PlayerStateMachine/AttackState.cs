using UnityEngine;

namespace TheAnonymousWarrior.Scripts.PlayerStateMachine
{
    public class AttackState : IPlayerState
    {
        public void Enter(PlayerController playerController)
        {
            Debug.Log("Enter Attack state");
        }

        public void OnUpdate(PlayerController playerController, InputSystem_Actions.PlayerActions input)
        {
            
        }

        public void OnFixedUpdate(PlayerController playerController)
        {
        
        }

        public void Exit(PlayerController playerController)
        {
            Debug.Log("Exit Attack state");
        }
    }
}