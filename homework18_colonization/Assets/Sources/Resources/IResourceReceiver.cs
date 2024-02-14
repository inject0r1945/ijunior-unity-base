using RTS.Units;

namespace RTS.Resources
{
    public interface IResourceReceiver
    {
        public bool TryReceiveResource(ResourcesCollectorUnit unit, Resource resource);
    }
}
