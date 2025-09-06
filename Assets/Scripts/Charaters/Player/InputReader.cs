using UnityEngine;

public class InputReader : MonoBehaviour
{
    private bool _isJump;
    private bool _isInterect;

    public float Dirrection { get; private set; }

    private void Update()
    {
        Dirrection = Input.GetAxis(ConstantData.InpudData.HORIZONTAL_AXIS);

        if (Input.GetKeyDown(KeyCode.W))
            _isJump = true;

        if (Input.GetKeyDown(KeyCode.F))
        { 
            _isInterect = true;
        }
    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    public bool GetIsInteract() => GetBoolAsTrigger(ref _isInterect);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
