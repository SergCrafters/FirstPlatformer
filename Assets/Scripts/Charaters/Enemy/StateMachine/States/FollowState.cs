using UnityEngine;

class FollowState : State
{
    private Animator _animator;
    private EnemyVision _vision;
    private Mover _mover;
    private Transform _target;
    private Fliper _fliper;


    public FollowState(StateMachine stateMachine, Animator animator, Fliper fliper, Mover mover, EnemyVision vision, 
                        float tryFindTime) : base(stateMachine)
    {
        _animator = animator;
        _vision = vision;
        _mover = mover;
        _fliper = fliper;

        Transitions = new Transition[]
{
            new LostTargetTransition(stateMachine, vision, tryFindTime),
};
    }

    public override void Enter()
    {
        _vision.TrySeeTarget(out _target);
        _animator.SetBool(ConstantData.AnimatorParameters.IsRun, true);
    }

    public override void Exit()
    {
        _animator.SetBool(ConstantData.AnimatorParameters.IsRun, false);
    }

    public override void Update()
    {
        if (_target != null)
        { 
            _mover.Run(_target);
            _fliper.LookAtTarget(_target.position);
        }

    }
}
