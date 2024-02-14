namespace RTS.AI
{
    public class GridBaker
    {
        public GridBaker()
        {
            AstarPath.FindAstarPath();
        }

        public void Bake()
        {
            AstarPath.active.Scan();
        }
    }
}
