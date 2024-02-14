namespace RTS.StateMachine
{
    public interface IStateSwitcher
    {
        public void SwitchState<T>() where T : IState;
    }
}