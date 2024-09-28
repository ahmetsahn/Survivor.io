namespace Script.Ahmet.StateMachine {
    public interface IState {
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
        void OnPointerClick();
        void OnPointerExit();
    }
}