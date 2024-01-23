namespace Platformer.Core
{
    public interface IDamager
    {
        public int Damage { get; }

        public float AttackDistance { get; }

        public float AttackDelay { get; }
    }
}
