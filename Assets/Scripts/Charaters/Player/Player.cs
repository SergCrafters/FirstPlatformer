using System;
using UnityEngine;

[RequireComponent(typeof(PlayerSound), typeof(GroundDetector), typeof(Mover))]
[RequireComponent(typeof(PlayerAnimator), typeof(CollisionHandler), typeof(PlayerAttacker))]
public class Player : Character
{

    [SerializeField] private EnemyAnimationEvent _animationEvent;
    [SerializeField] private Canvas _interactableCanvas;
    [SerializeField] private InventoryView _inventoryView;


    private CollisionHandler _collisionHandler;
    private GroundDetector _groundDetector;
    private IInputReader _inputReader;
    private PlayerAttacker _attacker;
    private PlayerAnimator _animator;
    private Mover _mover;
    private PlayerSound _audio;

    private Inventory _inventory;

    private IInteractable _interactable;



    protected override void Awake()
    {
        base.Awake();

        _collisionHandler = GetComponent<CollisionHandler>();
        _groundDetector = GetComponent<GroundDetector>();
        _attacker = GetComponent<PlayerAttacker>();
        _animator = GetComponent<PlayerAnimator>();
        _mover = GetComponent<Mover>();
        _audio = GetComponent<PlayerSound>();

        _inventory = new Inventory();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _collisionHandler.InteractableFounded += OnInteractableFounded;
        _collisionHandler.MedKitFounded += OnMedKitFounded;
        _collisionHandler.KeyFounded += OnKeyFounded;

        _animationEvent.DealingDamage += _attacker.Attack;
        _animationEvent.AttackStarted += _attacker.OnCanAttack;
        _animationEvent.AttackEnded += _attacker.OnCanAttack;

        _inventory.itemAdded += AddItemToInventory;
        _inventory.itemRemoved += _inventoryView.Remove;
    }


    protected override void OnDisable()
    {
        base.OnDisable();

        _collisionHandler.InteractableFounded -= OnInteractableFounded;
        _collisionHandler.MedKitFounded -= OnMedKitFounded;
        _collisionHandler.KeyFounded -= OnKeyFounded;
        _animationEvent.DealingDamage -= _attacker.Attack;
        _animationEvent.AttackStarted -= _attacker.OnCanAttack;
        _animationEvent.AttackEnded -= _attacker.OnCanAttack;

        _inventory.itemAdded -= AddItemToInventory;
        _inventory.itemRemoved -= _inventoryView.Remove;
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

            if (_groundDetector.IsGround)
                _audio.PlayStepSound();
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
        {
            _mover.Jump();
            bool _jump = true;
            _animator.SetJump(_jump, _groundDetector.IsGround);
            _audio.PlayJumpSound();
        }
        else
        {
            bool _jump = false;
            _animator.SetJump(_jump, _groundDetector.IsGround);
        }

        if (_inputReader.GetIsAttack() && _attacker.canAttack)
        {
            _animator.SetAttackTrigger();
            _audio.PlayAttackSound();
        }

        if (_inputReader.GetIsInteract() && _interactable != null)
        {
            if (_interactable.IsLock)
            {

                if (_inventory.Contains(_interactable.Key))
                {
                    _interactable.Unlock((Key)_inventory.Take(_interactable.Key));
                }
                else
                {
                    _interactable.Interact();
                }
            }
            else
            {
                _interactable.Interact();
                _interactableCanvas.gameObject.SetActive(false);
            }
        }
    }

    public void Initialize(IInputReader inputReader) => _inputReader = inputReader;


    protected override void OnTakingDamage()
    {
        _animator.SetHitTrigger();
        _audio.PlayHitSound();

        if (_attacker.canAttack == false)
            _attacker.OnCanAttack();
    }

    protected override void OnDied()
    {
        base.OnDied();

        _audio.PlayDeathSound();
    }

    private void OnInteractableFounded(IInteractable interactable)
    {
        _interactable = interactable;
        _interactableCanvas.gameObject.SetActive(interactable != null);
    }

    private void OnMedKitFounded(MedKit medKit)
    {
        if (Health.MaxValue > Health.Value)
        {
            Heal(medKit.Value);
            medKit.Collect();
        }
    }

    private void OnKeyFounded(Key key)
    {
        _inventory.Add(key);
    }

    private void AddItemToInventory(IItem item)
    {
        _inventoryView.Add(item);
        item.Collect();
    }

}
