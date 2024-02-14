namespace RTS.Resources
{
    public interface IResourceVisiter
    {
        public void Visit(Resource resource);

        public void Visit(Detail detail);
    }
}
