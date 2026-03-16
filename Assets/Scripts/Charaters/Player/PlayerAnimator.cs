using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator _animator;

    public void SetSpeedX(float speedX) 
    {
        _animator.SetFloat(ConstantData.AnimatorParameters.SpeedX,Mathf.Abs(speedX));
    }

    public void SetJump(bool isJump, bool isGround) 
    {
        _animator.SetBool(ConstantData.AnimatorParameters.Jump, isJump);
        _animator.SetBool(ConstantData.AnimatorParameters.Ground, isGround);
    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger(ConstantData.AnimatorParameters.Attack);
    }

    public void SetHitTrigger()
    {
        _animator.SetTrigger(ConstantData.AnimatorParameters.Hit);
    }
}
