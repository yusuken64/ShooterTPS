using UnityEngine;
using UnityEngine.AI;

public class EnemyMotor : MonoBehaviour
{
    public EnemyAnimationController EnemyAnimationController;
    public NavMeshAgent agent;

	public void MoveTo(Vector3 target)
    {
        agent.isStopped = false;
        agent.SetDestination(target);

        EnemyAnimationController.Play(EnemyAnimationController.chaseAnim);
    }

    public void Stop()
    {
        agent.isStopped = true;
        EnemyAnimationController.Play(EnemyAnimationController.idleAnim);
    }
}