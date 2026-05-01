using UnityEngine;
using UnityEngine.AI;

public class JumpAttack : EnemyBehaviorBase
{
    public Rigidbody rb;
    public NavMeshAgent agent;
    public Transform target;

    public float jumpForwardForce = 8f;
    public float jumpUpForce = 5f;
    public float duration = 1.5f;

    private bool _hasJumped;

    public override bool CanRun()
    {
        if (target == null || agent == null) return false;

        float dist = Vector3.Distance(transform.position, target.position);

        // Only jump if somewhat close AND path is bad
        if (dist > 10f) return false;

        if (agent.hasPath && agent.pathStatus == NavMeshPathStatus.PathComplete)
            return false;

        return true;
    }

    public override void Enter()
    {
        isComplete = false;
        timer = duration;
        _hasJumped = false;

        // Disable navmesh control
        agent.isStopped = true;
        agent.updatePosition = false;
        agent.updateRotation = false;

        PerformJump();
    }

    void PerformJump()
    {
        if (_hasJumped) return;

        Vector3 dir = (target.position - transform.position);
        dir.y = 0f;
        dir.Normalize();

        Vector3 force = dir * jumpForwardForce + Vector3.up * jumpUpForce;

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(force, ForceMode.Impulse);

        _hasJumped = true;
    }

    public override void Tick()
    {
        timer -= Time.deltaTime;

        // Rotate mid-air toward velocity (optional but nice)
        Vector3 vel = rb.linearVelocity;
        vel.y = 0f;

        if (vel.sqrMagnitude > 0.1f)
        {
            Quaternion rot = Quaternion.LookRotation(vel);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 10f * Time.deltaTime);
        }

        // End conditions
        if (timer <= 0f || IsGrounded())
        {
            isComplete = true;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.2f);
    }

    public override void Exit()
    {
        isComplete = true;

        // Re-enable navmesh
        agent.updatePosition = true;
        agent.updateRotation = true;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }

        agent.isStopped = false;
    }
}
