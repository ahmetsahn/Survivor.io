using Script.Ahmet.StateMachine;

namespace Script.StateMachineModule {
    public interface ITransition {
        IState To { get; }
        IPredicate Condition { get; }
    }
}