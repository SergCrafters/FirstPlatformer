using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator _animator;

    public void SetSpeedX(float speedX) 
    {
        _animator.SetFloat(ConstantData.AnimatorParameters.SpeedX,Mathf.Abs(speedX));
    }

    //public void SetJump(bool jump) 
    //{
    //    _animator.SetFloat(ConstantData.AnimatorParameters.Jump, jump);
    //}
}
