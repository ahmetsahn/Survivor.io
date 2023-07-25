using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static event Action OnSonarBuff;
    public static event Action<WeaponType> OnElectricBuff;
    public static event Action OnArmorBuff;

    public void ClickSonarBuff()
    {
        OnSonarBuff?.Invoke();
    }

    public void ClickElectricBuff()
    {
        OnElectricBuff?.Invoke(WeaponType.Electric);
    }

    public void ClickArmorBuff()
    {
        OnArmorBuff?.Invoke();
    }
}
