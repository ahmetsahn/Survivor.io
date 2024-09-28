using Script.Ahmet.StateMachine;
using UnityEngine;

namespace Script.Runtime.CollectableModule.ItemModule.State.ItemIconState
{
    public abstract class ItemIconBaseState : IState
    {
        private readonly GameObject _interactPanel;
        
        protected ItemIconBaseState(GameObject interactPanel)
        {
            _interactPanel = interactPanel;
        }

        public virtual void OnEnter() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public virtual void OnExit()
        {
            _interactPanel.SetActive(false);
        }
        
        public virtual void OnPointerClick()
        {
            _interactPanel.SetActive(true);
        }

        public virtual void OnPointerExit()
        {
            _interactPanel.SetActive(false);
        }
    }
}