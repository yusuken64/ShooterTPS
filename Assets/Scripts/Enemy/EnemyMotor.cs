using UnityEngine;
using UnityEngine.AI;

public class EnemyMotor : MonoBehaviour
{
    public EnemyAnimationController EnemyAnimationController;
    public NavMeshAgent agent;
    public void MoveTo(Vector3 target)
    {
        if (!agent.isActiveAndEnabled)
            return;

        if (!agent.isOnNavMesh)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(agent.transform.position, out hit, 5f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }
            else
            {
                return;
            }
        }

        NavMeshPath path = new NavMeshPath();

        if (agent.CalculatePath(target, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            agent.isStopped = false;
            agent.SetDestination(target);
        }
        EnemyAnimationController.Play(EnemyAnimationController.chaseAnim);
    }

    public void Stop()
    {
        agent.isStopped = true;
        EnemyAnimationController.Play(EnemyAnimationController.idleAnim);
    }
}