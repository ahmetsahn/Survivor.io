using Assets.Script.Runtime.Signal;
using Script.Runtime.Signal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.Runtime.UIModule
{
    public class GameUIPanelView : MonoBehaviour
    {
        public Image PlayerExpBar;

        public TextMeshProUGUI PlayerLevelText;

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _signalBus.Subscribe<UpdatePlayerExpUISignal>(UpdatePlayerExpUI);
            _signalBus.Subscribe<PlayerLevelUpSignal>(PlayerLevelUp);
        }

        private void UpdatePlayerExpUI(UpdatePlayerExpUISignal signal)
        {
            PlayerExpBar.fillAmount = (float)signal.PlayerExp / signal.PlayerLevelExp;
        }

        private void PlayerLevelUp(PlayerLevelUpSignal signal)
        {
            PlayerLevelText.text = "LV " + signal.PlayerLevel;
            PlayerExpBar.fillAmount = 0;
        }

        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<UpdatePlayerExpUISignal>(UpdatePlayerExpUI);
            _signalBus.Unsubscribe<PlayerLevelUpSignal>(PlayerLevelUp);
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
    }
}