using UnityEngine;
using UnityEngine.AI;

public class ChaseDirect : EnemyBehaviorBase
{
    public EnemyMotor motor;
    public Transform target;

    public float speed = 3f;
    public float duration = 2f;
    public float rotationSpeed = 10f;

    public override bool CanRun()
    {
        if (motor == null || target == null) return false;

        // Only run if normal pathing fails
        if (motor.agent.hasPath &&
            motor.agent.pathStatus == NavMeshPathStatus.PathComplete)
            return false;

        return true;
    }

    public override void Enter()
    {
        isComplete = false;
        timer = duration;

        // Stop navmesh from interfering
        motor.agent.isStopped = true;
    }

    public override void Tick()
    {
        timer -= Time.deltaTime;

        Vector3 dir = (target.position - transform.position);
        dir.y = 0f;

        if (dir.sqrMagnitude > 0.001f)
        {
            dir.Normalize();

            // Manual movement
            transform.position += dir * speed * Time.deltaTime;

            // Rotate toward movement
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rot,
                rotationSpeed * Time.deltaTime
            );
        }

        // End if close OR timeout
        if (timer <= 0f || Vector3.Distance(transform.position, target.position) < 2f)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {
        isComplete = true;

        // Try to snap back to navmesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            motor.agent.Warp(hit.position);
        }

        motor.agent.isStopped = false;
    }
}