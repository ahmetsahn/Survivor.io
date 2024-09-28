using Script.Runtime.Enum;
using Script.Runtime.Signal;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.Runtime
{
    public class AbilityButton : MonoBehaviour
    {
        public AbilityType AbilityType;
        
        [SerializeField]
        private Sprite[] abilitySprites;
        
        private SignalBus _signalBus;
        
        private Image _abilityImage;
        
        private Button _button;
        
        private int _abilityLevel;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize(int abilityLevel)
        {
            _abilityLevel = abilityLevel;
            _abilityImage.sprite = abilitySprites[_abilityLevel];
        }
        
        private void Awake()
        {
            InitializeDependencies();
        }
        
        private void InitializeDependencies()
        {
            _abilityImage = GetComponent<Image>();
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _button.onClick.AddListener(SelectAbility);
        }
        
        protected virtual void SelectAbility()
        {
            _signalBus.Fire(new HideUIPanelSignal(UIPanelType.AbilitySelectionPanel));
            _signalBus.Fire(new AbilityButtonClickSignal(AbilityType, _abilityLevel));
        }
        
        private void UnsubscribeEvents()
        {
            _button.onClick.RemoveListener(SelectAbility);
        }
        
        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
    }
}