using UnityEngine;


[RequireComponent(typeof(EnemyAttacker), typeof(EnemyVision), typeof(Mover))]
[RequireComponent(typeof(EnemySound), typeof(EnemyGroundDetector))]
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
    private EnemyGroundDetector _groundDetector;
    private EnemySound _audio;
    private EnemyStateMachine _stateMachine;

    protected override void Awake()
    {
        base.Awake();

        _attacker = GetComponent<EnemyAttacker>();
        _vision = GetComponent<EnemyVision>();
        _groundDetector = GetComponent<EnemyGroundDetector>();
        _audio = GetComponent<EnemySound>();

        _animationEvent.DealingDamage += _attacker.Attack;
        _animationEvent.AttackEnded += _attacker.OnAttackEnded;

    }

    private void Start()
    {
        var mover = GetComponent<Mover>();

        _stateMachine = new EnemyStateMachine(Fliper, mover, _vision, _groundDetector, _animator, _attacker, _audio, _wayPoints ,_maxSqrDistance, transform,
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
        _audio.PlayHitSound();

        if (_vision.TrySeeTarget(out _) == false)
            Fliper.Flip();
    }

    protected override void OnDied()
    {
        _audio.PlayDeathSound();
        Destroy(gameObject);
    }
}
