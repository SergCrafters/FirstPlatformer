using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(GroundDetector), typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMover _mover;
    private GroundDetector _groundDetector;
    private PlayerAnimator _animator;

    private void Awake()
    {
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void FixedUpdate()
    {
        _animator.SetSpeedX(_inputReader.Dirrection);

        if (_inputReader.Dirrection != 0)
            _mover.Move(_inputReader.Dirrection, _groundDetector.IsGround);

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
        {
            _mover.Jump();
            bool _jump = true;
            //bool _isGround = false;
            _animator.SetJump(_jump, _groundDetector.IsGround);
        }
        else
        {
            bool _jump = false;
            //bool _isGround = true;
            _animator.SetJump(_jump, _groundDetector.IsGround);
        }
    }
}
