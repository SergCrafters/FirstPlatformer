using UnityEngine;


[RequireComponent(typeof(EnemyAttacker), typeof(EnemyVision), typeof(Mover))]
public class Enemy : Character
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemyAnimationEvent _animationEvent;
    [SerializeField] private float _maxSqrDistance = 0.1f;
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private float _tryFindTime = 1f;

    private EnemyAttacker _attacker;
    private EnemyVision _vision;
    private EnemyStateMachine _stateMachine;

    protected override void Awake()
    {
        base.Awake();

        _attacker = GetComponent<EnemyAttacker>();
        _vision = GetComponent<EnemyVision>();
        _animationEvent.DealingDamage += _attacker.Attack;
        _animationEvent.AttackEnded += _attacker.OnAttackEnded;

    }

    private void Start()
    {
        var mover = GetComponent<Mover>();

        _stateMachine = new EnemyStateMachine(Fliper, mover, _vision, _animator, _attacker, _wayPoints ,_maxSqrDistance, transform,
                                                _waitTime, _tryFindTime);
    }

    private void FixedUpdate()
    { 
        if(TimeManager.IsPaused)
            return;

        _stateMachine.Update();
    }

    private void OnDestroy()
    {
        _animationEvent.DealingDamage -= _attacker.Attack;
        _animationEvent.AttackEnded -= _attacker.OnAttackEnded;
    }

    protected override void OnTakingDamage()
    {
        _animator.SetTrigger(ConstantData.AnimatorParameters.Hit);
    
        if (_vision.TrySeeTarget(out _) == false)
            Fliper.Flip();
    }

    protected override void OnDied()
    {
        Destroy(gameObject);
    }
}
