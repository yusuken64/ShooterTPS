using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public Transform target;

    public List<EnemyBehaviorBase> Behaviors;
    public EnemyIdle EnemyIdle;
    public EnemyDead EnemyDead;

    public EnemyBehaviorBase Current;

	private void Start()
	{
        SetBehavior(PickNextBehavior());
    }

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
        EnemyBehaviorBase best = null;
        int bestPriority = int.MinValue;

        foreach (var behavior in Behaviors)
        {
            if (behavior == Current) continue;
            if (behavior.IsOnCooldown) continue;
            if (!behavior.CanRun()) continue;

            if (behavior.Priority > bestPriority)
            {
                bestPriority = behavior.Priority;
                best = behavior;
            }
        }

        return best ?? EnemyIdle;
    }

    public void OnDeath()
    {
        if (EnemyDead.CanRun())
        {
            SetBehavior(EnemyDead);
        }
    }
}
