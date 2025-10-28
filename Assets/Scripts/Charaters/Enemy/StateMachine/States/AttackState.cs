using UnityEngine;

class AttackState : State
{
    private EnemyAttacker _attacker;
    private Animator _animator;
    private EnemyVision _vision;
    private Fliper _fliper;
    private LostTargetTransition _lostTargetTransition;
    private Transform _target;

    public AttackState(StateMachine stateMachine, EnemyAttacker attacker, Animator animator, Fliper fliper, EnemyVision vision, float tryFindTime) : base(stateMachine)
    {
        _attacker = attacker;
        _animator = animator;
        _vision = vision;
        _fliper = fliper;

        _lostTargetTransition = new LostTargetTransition(stateMachine, vision, tryFindTime);

        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine, vision, vision.transform, attacker.AttackDistance),
            _lostTargetTransition
        };
    }

    public override void Enter()
    {
        _vision.TrySeeTarget(out _target);
        _lostTargetTransition.IsNeedTransit();
    }

    public override void Update()
    {
        if (_attacker.IsAttack == false)
            _fliper.LookAtTarget(_target.position);

        if (_attacker.CanAttack)
        {
            _attacker.StartAttack();
            _animator.SetTrigger(ConstantData.AnimatorParameters.Attack);
        }
    }

    public override void TryTransit()
    {
        if (_attacker.IsAttack == false)
        {
            base.TryTransit();
        }

    }
}
