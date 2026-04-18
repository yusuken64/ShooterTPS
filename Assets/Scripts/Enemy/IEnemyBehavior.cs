public interface IEnemyBehavior
{
    bool CanRun();
    int Priority { get; }

    void Enter();
    void Tick();
    void Exit();

    bool IsComplete { get; }
}