using Pathfinding;

namespace Platformer.Core
{
    public interface IAIMovable
    {
        public float Speed { get; }

        public AIPath Pathfinder { get; }
    }
}