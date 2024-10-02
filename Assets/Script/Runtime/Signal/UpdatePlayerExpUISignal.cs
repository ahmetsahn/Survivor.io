using System.Collections;
using UnityEngine;

namespace Assets.Script.Runtime.Signal
{
    public readonly struct UpdatePlayerExpUISignal
    {
        public readonly int PlayerExp;
        public readonly int PlayerLevelExp;

        public UpdatePlayerExpUISignal(int playerExp, int playerLevelExp)
        {
            PlayerExp = playerExp;
            PlayerLevelExp = playerLevelExp;
        }
    }
}