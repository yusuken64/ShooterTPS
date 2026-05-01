using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : EnemyBehaviorBase
{
    public EnemyMotor motor;
    public float rotationSpeed = 10f;
    public Player target;

	private void Start()
	{
        target = FindFirstObjectByType<Player>();
	}

	public override void Tick()
    {
        // ignore vertical movement
        Vector3 velocity = motor.agent.velocity;

        velocity.y = 0f;

        if (velocity.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );
        }

        if (Vector3.Distance(transform.position, target.transform.position) < 5f ||
            timer <= 0)
        {
            isComplete = true;
        }
    }

    public override bool CanRun()
    {
        if (!motor.agent.isActiveAndEnabled)
            return false;

        if (target == null) { return false; }

        NavMeshHit hit;

        Vector3 start = motor.agent.transform.position;
        Vector3 end = target.transform.position;

        Debug.DrawLine(start, end, Color.red, 1f);
        //Debug.Log($"Start on NavMesh: {NavMesh.SamplePosition(start, out _, 2f, NavMesh.AllAreas)}");
        //Debug.Log($"End on NavMesh: {NavMesh.SamplePosition(end, out _, 2f, NavMesh.AllAreas)}");

        if (NavMesh.SamplePosition(start, out hit, 2f, NavMesh.AllAreas))
            start = hit.position;
        else
            return false;

        if (NavMesh.SamplePosition(end, out hit, 2f, NavMesh.AllAreas))
            end = hit.position;
        else
            return false;

        NavMeshPath path = new NavMeshPath();

        bool hasPath = NavMesh.CalculatePath(
            start,
            end,
            NavMesh.AllAreas,
            path
        );

        if (!hasPath || path.status != NavMeshPathStatus.PathComplete)
            return false;

        return true;
    }

    public override void Enter()
    {
        isComplete = false;

        target = FindFirstObjectByType<Player>();

        motor.MoveTo(target.transform.position);
        timer = 3f;
    }

	public override void Exit()
    {
        isComplete = true;
        motor.Stop();
    }
}
