using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private FloatReference currentHealth;
    [SerializeField] private FloatReference maxHealth;
    [SerializeField] private Image healthBarImage;

    [SerializeField] private FloatReference currentExp;
    [SerializeField] private FloatReference maxExp;
    [SerializeField] private Image expBarImage;

    [SerializeField] private GameObject buffPanel;

    public event Action OnBuffPanelActive;
    public event Action OnBuffPanelDeactive;

    private void Start()
    {
        currentExp.Value = 0;
    }
    public void UpdateHealth()
    {
        healthBarImage.fillAmount = currentHealth.Value / maxHealth.Value;
    }

    public void UpdateExp()
    {
        expBarImage.fillAmount = currentExp.Value / maxExp.Value;

        if(currentExp.Value == maxExp.Value)
        {
            OpenBuffPanel();
            
        }
    }

    private void OpenBuffPanel()
    {
        buffPanel.SetActive(true);
        buffPanel.transform.DOScale(1f, 1f).SetEase(Ease.OutBack);
        GameManager.Instance.ChangeState(GameStates.Pause);
        OnBuffPanelActive?.Invoke();
    }

    public void CloseBuffPanel()
    {
        buffPanel.transform.DOScale(0, 1f).SetEase(Ease.InBack).onComplete += () =>
        {
            buffPanel.SetActive(false);
            GameManager.Instance.ChangeState(GameStates.Game);
            OnBuffPanelDeactive?.Invoke();
            currentExp.Value = 0;
        };
           
           
    }
}
