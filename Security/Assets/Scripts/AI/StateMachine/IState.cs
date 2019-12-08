public interface IState
{
    StateType StateType { get; }

    void Act();
    void Enter();
    void Exit();
}
