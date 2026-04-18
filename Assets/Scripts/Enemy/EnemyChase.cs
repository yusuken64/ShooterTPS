using UnityEngine;

public class EnemyChase : EnemyBehaviorBase
{
    public EnemyMotor motor;
    public Transform player;
    public EnemyAttack EnemyAttack;

    public float repathRate = 0.2f;
    public float rotationSpeed = 10f;

    private float _timer;

	void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    void Update()
    {
        if (player == null || motor == null) return;

        if (_timer <= 0f)
        {
            _timer = repathRate;
        }
    }

    public override void Tick()
    {
        _timer -= Time.deltaTime;

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

        if (Vector3.Distance(transform.position, player.position) < 5f)
        {
            isComplete = true;
        }
    }

	public override bool CanRun()
	{
        return true;
	}

	public override void Enter()
    {
        isComplete = false;
        motor.MoveTo(player.position);
    }

	public override void Exit()
    {
        isComplete = true;
        motor.Stop();
    }
}
