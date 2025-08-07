namespace TheAnonymousWarrior.Scripts.Player
{
    public class GroundAttack : Attack
    {
        protected PlayerController _controller;

        protected override void Start()
        {
            base.Start();
            _controller = GetComponentInParent<PlayerController>();
            _controller.OnAttackEvent += ControllerOnAttackEvent;
        }
    
        protected void OnDestroy()
        {
            _controller.OnAttackEvent -= ControllerOnAttackEvent;
        }
    }
}