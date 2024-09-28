using UnityEngine;

namespace Script.Runtime.CollectableModule
{
    [CreateAssetMenu(fileName = "ExpGemData", menuName = "Scriptable Object/ExpGemData", order = 0)]
    public class ExpGemSo : ScriptableObject
    {
        public int ExpValue;
    }
}