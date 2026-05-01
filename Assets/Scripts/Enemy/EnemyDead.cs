using UnityEngine;

public class EnemyDead : EnemyBehaviorBase
{
	public EnemyAnimationController EnemyAnimationController;
	public bool Died;
    public EnemyMotor motor;

    public override bool CanRun()
	{
		return !Died;
	}

    public override void Enter()
    {
        Died = true;

        // Stop NavMesh movement safely
        if (motor.agent != null && motor.agent.isActiveAndEnabled)
        {
            //motor.agent.isStopped = true;
            //motor.agent.ResetPath();
            motor.agent.enabled = false;
        }

        // Disable physics collisions
        var collider = GetComponentInParent<Collider>();
        if (collider != null)
            collider.enabled = false;

        var rb = GetComponentInParent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        EnemyAnimationController.Play(EnemyAnimationController.deadAnim);

        isComplete = false;
        timer = 3f;
    }

    public override void Exit()
	{
		Destroy(this.gameObject);
	}
}