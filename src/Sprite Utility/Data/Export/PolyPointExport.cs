namespace SpriteUtility
{
    public class PolyPointExport
    {
        public PolyPointExport(PolyPoint point)
        {
            X = point.MappedX;
            Y = point.MappedY;
        }

        public int Y { get; set; }
        public int X { get; set; }
    }
}