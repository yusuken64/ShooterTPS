using UnityEngine;

public class EnemyDead : EnemyBehaviorBase
{
	public EnemyAnimationController EnemyAnimationController;
	public bool Died;

	public override bool CanRun()
	{
		return !Died;
	}

	public override void Enter()
	{
		Died = true;
		EnemyAnimationController.Play(EnemyAnimationController.deadAnim);
		isComplete = false;
		timer = 3f;
	}

	public override void Exit()
	{
		Destroy(this.gameObject);
	}
}