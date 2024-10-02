namespace Script.Runtime.Signal
{
    public readonly struct PlayerLevelUpSignal
    {
        public readonly int PlayerLevel;

        public PlayerLevelUpSignal(int playerLevel)
        {
            PlayerLevel = playerLevel;
        }
    }
}