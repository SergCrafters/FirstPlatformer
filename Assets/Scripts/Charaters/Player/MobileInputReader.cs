using UnityEngine;
using UnityEngine.UI;

public class MobileInputReader : MonoBehaviour, IInputReader
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private ToutchHandler _jumpButton;
    [SerializeField] private ToutchHandler _attackButton;
    [SerializeField] private ToutchHandler _interactButton;

    private bool _isJump;
    private bool _isInterect;
    private bool _isAttack;

    public float Dirrection => _joystick.Horizontal;

    private void OnEnable()
    {
        _jumpButton.Down += SetJump;
        _attackButton.Down += SetAttack;
        _interactButton.Down += SetInteract;
    }

    private void OnDisable()
    {
        _jumpButton.Down -= SetJump;
        _attackButton.Down -= SetAttack;
        _interactButton.Down -= SetInteract;
    }

    private void Update()
    {
        if (TimeManager.IsPaused)
            return;
    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    public bool GetIsInteract() => GetBoolAsTrigger(ref _isInterect);

    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);

    public void SetJump() => _isJump = true;

    public void SetInteract() => _isInterect = true;

    public void SetAttack() => _isAttack = true;

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
