using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(GroundDetector), typeof(Mover))]
[RequireComponent(typeof(PlayerAnimator), typeof(CollisionHandler), typeof(PlayerAttacker))]
public class Player : Character
{

    [SerializeField] private EnemyAnimationEvent _animationEvent;

    private CollisionHandler _collisionHandler;
    private GroundDetector _groundDetector;
    private InputReader _inputReader;
    private PlayerAttacker _attacker;
    private PlayerAnimator _animator;
    private Mover _mover;

    private IInteractable _interactable;



    protected override void Awake()
    {
        base.Awake();

        _collisionHandler = GetComponent<CollisionHandler>();
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _attacker = GetComponent<PlayerAttacker>();
        _animator = GetComponent<PlayerAnimator>();
        _mover = GetComponent<Mover>();

    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _collisionHandler.FinishReached += OnFinishReached;
        _animationEvent.DealingDamage += _attacker.Attack;
        _animationEvent.AttackStarted += _attacker.OnCanAttack;
        _animationEvent.AttackEnded += _attacker.OnCanAttack;
    }


    protected override void OnDisable()
    {
        base.OnDisable();

        _collisionHandler.FinishReached -= OnFinishReached;
        _animationEvent.DealingDamage -= _attacker.Attack;
        _animationEvent.AttackStarted -= _attacker.OnCanAttack;
        _animationEvent.AttackEnded -= _attacker.OnCanAttack;
    }


    private void FixedUpdate()
    {
        if (TimeManager.IsPaused)
            return;

        _animator.SetSpeedX(_inputReader.Dirrection);

        if (_inputReader.Dirrection != 0)
        {
            _mover.Move(_inputReader.Dirrection, _groundDetector.IsGround);
            Fliper.LookAtTarget(transform.position + Vector3.right * _inputReader.Dirrection);
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
        {
            _mover.Jump();
            bool _jump = true;
            _animator.SetJump(_jump, _groundDetector.IsGround);
        }
        else
        {
            bool _jump = false;
            _animator.SetJump(_jump, _groundDetector.IsGround);
        }

        if (_inputReader.GetIsAttack() && _attacker.canAttack)
        {
            _animator.SetAttackTrigger();
            print(" твоя атака");
        }

        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }
    protected override void OnTakingDamage()
    {
        _animator.SetHitTrigger();

        if (_attacker.canAttack == false)
            _attacker.OnCanAttack();
    }

    private void OnFinishReached(IInteractable finish)
    {
        _interactable = finish;
    }
}
