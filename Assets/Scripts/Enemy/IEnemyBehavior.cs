public interface IEnemyBehavior
{
    bool IsComplete { get; }

    void Enter();
    void Tick();
    void Exit();
}