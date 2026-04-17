using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public Transform player;

    public EnemyAttack EnemyAttack;
    public EnemyChase EnemyChase;
    public EnemyIdle EnemyIdle;

    private IEnemyBehavior _current;

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

    void SetBehavior(IEnemyBehavior next)
    {
        _current?.Exit();
        _current = next;
        _current.Enter();
    }

    IEnemyBehavior PickNextBehavior()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < 2.5f)
        {
            return EnemyAttack;
        }

        return EnemyChase;
    }
}