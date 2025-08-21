using UnityEngine;

public class InputReader : MonoBehaviour
{
    private bool _isJump;

    public float Dirrection { get; private set; }

    private void Update()
    {
        Dirrection = Input.GetAxis(ConstantData.InpudData.HORIZONTAL_AXIS);

        if (Input.GetKeyDown(KeyCode.W))
            _isJump = true;
    }

    public bool GetIsJump()
    {
        bool isJump = _isJump;
        _isJump = false;
        return isJump;
    }
}
