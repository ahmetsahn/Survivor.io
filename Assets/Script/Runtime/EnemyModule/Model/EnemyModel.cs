namespace Script.Runtime.EnemyModule.Model
{
    public class EnemyModel
    {
        public EnemySo Data;

        public int Health;

        public EnemyModel(EnemySo data)
        {
            Data = data;
            Health = data.MaxHealth;
        }
    }
}