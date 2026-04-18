using UnityEngine;

public abstract class EnemyBehaviorBase : MonoBehaviour, IEnemyBehavior
{
    public int SelectionPriority;
    internal bool isComplete;
    internal float timer;
    public bool IsComplete => isComplete;
    public int Priority => SelectionPriority;
	public abstract bool CanRun();
	public abstract void Enter();
	public abstract void Exit();
	public virtual void Tick()
	{
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isComplete = true;
        }
    }

    internal virtual void Update()
	{
		
	}
}
