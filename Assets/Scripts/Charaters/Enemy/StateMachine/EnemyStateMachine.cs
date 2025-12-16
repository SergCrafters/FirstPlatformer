using System;
using System.Collections.Generic;
using UnityEngine;

class EnemyStateMachine : StateMachine
{
    public EnemyStateMachine(Fliper fliper, Mover mover, EnemyVision vision, EnemyGroundDetector groundDetector, Animator animator, EnemyAttacker attacker, EnemySound audio, WayPoint[] wayPoints, 
                            float maxSqrDistance, Transform transform, float waitTime, float tryFindTime)
    {
        States = new Dictionary<Type, State>()
        {
            {typeof(PatrolState), new PatrolState(this, animator, fliper, mover, vision, audio, wayPoints, maxSqrDistance, transform, attacker.AttackDistance) },
            {typeof(IdleState), new IdleState(this, vision, waitTime, attacker.AttackDistance) },
            {typeof(FollowState), new FollowState(this, animator, fliper, mover, vision, groundDetector, audio, tryFindTime, attacker.AttackDistance) },
            {typeof(AttackState), new AttackState(this, attacker, animator, fliper, vision, audio, attacker.Delay) },

        };

        ChacgeState<PatrolState>();
    }
}
