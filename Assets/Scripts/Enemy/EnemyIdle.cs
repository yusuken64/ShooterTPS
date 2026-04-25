using UnityEngine;

public class EnemyIdle : EnemyBehaviorBase
{
	public override bool CanRun()
	{
		return true;
	}

	public override void Enter()
	{
		isComplete = false;
		timer = 5f;
	}

	public override void Exit()
	{
		isComplete = true;
	}
}
