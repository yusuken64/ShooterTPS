using System;
using UnityEngine;


public class EnemyAttack : EnemyBehaviorBase
{
    public EnemyAnimationController EnemyAnimationController;
    public float AttackCooldownMax;
    public float AttackCooldown;
    private Transform player;

	void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    public  override void Enter()
    {
        isComplete = false;
        AttackCooldown = AttackCooldownMax;
        TryAttack(player);
    }

	public void TryAttack(Transform target)
    {
        // rotate toward target (optional)
        transform.LookAt(target.position);

        // trigger animation
        EnemyAnimationController.Play(EnemyAnimationController.attackAnim);
    }

	internal override void Update()
	{
		base.Update();
        AttackCooldown -= Time.deltaTime;
	}

	public override bool CanRun()
	{
        return AttackCooldown <= 0;
	}

	public override void Exit()
	{
	}
}
