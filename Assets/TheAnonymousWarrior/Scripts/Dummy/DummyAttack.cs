using TheAnonymousWarrior.Scripts.Player;
using UnityEngine;

public class DummyAttack : Attack
{
    protected EnemyDummy _controller;

    protected override void Start()
    {
        base.Start();
        _controller = GetComponentInParent<EnemyDummy>();
        _controller.OnAttackEvent += ControllerOnAttackEvent;
    }
    
    protected void OnDestroy()
    {
        _controller.OnAttackEvent -= ControllerOnAttackEvent;
    }
}
