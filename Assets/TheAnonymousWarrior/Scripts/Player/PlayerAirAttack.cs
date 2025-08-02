namespace TheAnonymousWarrior.Scripts.Player
{
    public class PlayerAirAttack : PlayerAttack
    {
        protected override void Start()
        {
            base.Start();
            _controller.OnAirAttackEvent += ControllerOnAttackEvent;
        }
    
        protected void OnDestroy()
        {
            _controller.OnAirAttackEvent -= ControllerOnAttackEvent;
        }
    }
}