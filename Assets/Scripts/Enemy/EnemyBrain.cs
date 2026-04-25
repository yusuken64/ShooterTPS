using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public Transform target;

    public EnemyAttack EnemyAttack;
    public EnemyChase EnemyChase;
    public EnemyIdle EnemyIdle;
    public EnemyDead EnemyDead;

    public EnemyBehaviorBase Current;

    void Update()
    {
        if (Current == null)
        {
            SetBehavior(PickNextBehavior());
            return;
        }

        Current.Tick();

        if (Current.IsComplete)
        {
            SetBehavior(PickNextBehavior());
        }
    }

    void SetBehavior(EnemyBehaviorBase next)
    {
        if (EnemyDead.Died) { return; }

        Current?.Exit();
        Current = next;
        Current.Enter();
    }

    EnemyBehaviorBase PickNextBehavior()
    {
        if (EnemyAttack.CanRun())
        {
            return EnemyAttack;
        }

        if (EnemyChase.CanRun())
        {
            return EnemyChase;
        }

        return EnemyIdle;
    }

    public void OnDeath()
    {
        if (EnemyDead.CanRun())
        {
            SetBehavior(EnemyDead);
        }
    }
}
