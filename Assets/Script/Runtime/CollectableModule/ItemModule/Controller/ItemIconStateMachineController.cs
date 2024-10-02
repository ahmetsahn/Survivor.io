using System;
using Script.Ahmet.StateMachine;
using Script.Runtime.CollectableModule.ItemModule.State.ItemIconState;
using Script.Runtime.CollectableModule.ItemModule.View;
using Zenject;

namespace Script.Runtime.CollectableModule.ItemModule.Controller
{
    public class ItemIconStateMachineController : IInitializable, ITickable, IDisposable
    {
        private readonly ItemIconView _itemIconView;
        
        private readonly StateMachine _stateMachine;
        
        private readonly ItemIconOnInventoryState _itemIconOnInventoryState;
        
        private readonly ItemIconEquippedState _itemIconEquippedState;
        
        public ItemIconStateMachineController(
            ItemIconView itemIconView,
            StateMachine stateMachine, 
            ItemIconOnInventoryState itemIconOnInventoryState,
            ItemIconEquippedState itemIconEquippedState)
        {
            _itemIconView = itemIconView;
            _stateMachine = stateMachine;
            _itemIconOnInventoryState = itemIconOnInventoryState;
            _itemIconEquippedState = itemIconEquippedState;
            
            SubscribeEvents();
            SetStateMachine();
        }
        
        public void Initialize()
        {
            _stateMachine.SetState(_itemIconOnInventoryState);
        }
        
        private void SubscribeEvents()
        {
            _itemIconView.OnPointerClickEvent += OnPointerClick;
            _itemIconView.OnPointerExitEvent += OnPointerExit;
        }
        
        private void OnPointerClick()
        {
            _stateMachine.CurrentState.OnPointerClick();
        }
        
        private void OnPointerExit()
        {
            _stateMachine.CurrentState.OnPointerExit();
        }
        
        private void SetStateMachine()
        {
            Any(_itemIconOnInventoryState, new FuncPredicate(() => _itemIconView.transform.parent.name == "ItemParent"));
            Any(_itemIconEquippedState, new FuncPredicate(() => _itemIconView.transform.parent.parent.name == "EquipmentSlotsParent"));
        }
        
        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
        
        public void Tick()
        {
            _stateMachine.Update();
        }

        private void UnsubscribeEvents()
        {
            _itemIconView.OnPointerClickEvent -= OnPointerClick;
            _itemIconView.OnPointerExitEvent -= OnPointerExit;
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}