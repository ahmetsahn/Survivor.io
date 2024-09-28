using UnityEngine;
using Zenject;

namespace Script.Runtime.UIModule
{
    public class AbilitySelectionPanelView : MonoBehaviour
    {
        [SerializeField]
        private Transform abilityButtonParent;
        
        private AbilityButtonManager _abilityButtonManager;
        
        [Inject]
        private void Construct(AbilityButtonManager abilityButtonManager)
        {
            _abilityButtonManager = abilityButtonManager;
        }
        
        private void Start()
        {
            Time.timeScale = 0;
            _abilityButtonManager.InstantiateAbilityButtons(abilityButtonParent);
        }
        
        private void OnDestroy()
        {
            Time.timeScale = 1;
        }
    }
}