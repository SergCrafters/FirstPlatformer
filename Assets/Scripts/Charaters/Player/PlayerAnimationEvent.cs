using System;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public event Action AttackStarted;
    public event Action AttackEnded;

    public void InvokeDealingDamageEvent() => AttackStarted?.Invoke();

    public void InvokeAttackEndedEvent() => AttackEnded?.Invoke();
}
