using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public Transform player;

    public EnemyAttack EnemyAttack;
    public EnemyChase EnemyChase;
    public EnemyIdle EnemyIdle;
    public EnemyDead EnemyDead;

    private EnemyBehaviorBase _current;

    void Update()
    {
        if (_current == null)
        {
            SetBehavior(PickNextBehavior());
            return;
        }

        _current.Tick();

        if (_current.IsComplete)
        {
            SetBehavior(PickNextBehavior());
        }
    }

    void SetBehavior(EnemyBehaviorBase next)
    {
        if (EnemyDead.Died) { return; }

        _current?.Exit();
        _current = next;
        _current.Enter();
    }

    EnemyBehaviorBase PickNextBehavior()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < 2.5f)
        {
            return EnemyAttack;
        }

        return EnemyChase;
    }

    public void OnDeath()
    {
        if (EnemyDead.CanRun())
        {
            SetBehavior(EnemyDead);
        }
    }
}
