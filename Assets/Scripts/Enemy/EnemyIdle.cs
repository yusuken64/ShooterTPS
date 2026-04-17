using UnityEngine;

public class EnemyIdle : MonoBehaviour, IEnemyBehavior
{
    public float IdleTime = 3f;
    private bool isComplete;
    private float _timer;
    public bool IsComplete => throw new System.NotImplementedException();

	public void Enter()
	{
        isComplete = false;
	}

	public void Exit()
	{
	}

	public void Tick()
	{
        _timer -= Time.deltaTime;
        if (_timer <= 0)
		{
            isComplete = true;
		}
	}
}