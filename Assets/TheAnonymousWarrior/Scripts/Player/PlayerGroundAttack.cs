namespace TheAnonymousWarrior.Scripts.Player
{
    public class PlayerGroundAttack : PlayerAttack
    {
        protected override void Start()
        {
            base.Start();
            _controller.OnAttackEvent += ControllerOnAttackEvent;
        }
    
        protected void OnDestroy()
        {
            _controller.OnAttackEvent -= ControllerOnAttackEvent;
        }
    }
}