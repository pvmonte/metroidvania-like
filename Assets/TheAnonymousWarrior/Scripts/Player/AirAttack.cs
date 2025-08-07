namespace TheAnonymousWarrior.Scripts.Player
{
    public class AirAttack : Attack
    {
        protected PlayerController _controller;

        protected override void Start()
        {
            base.Start();
            _controller = GetComponentInParent<PlayerController>();
            _controller.OnAirAttackEvent += ControllerOnAttackEvent;
        }
    
        protected void OnDestroy()
        {
            _controller.OnAirAttackEvent -= ControllerOnAttackEvent;
        }
    }
}