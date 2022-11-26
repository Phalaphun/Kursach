namespace Kursach
{
    internal class BlockO:Block
    {
        private Position[][] tiles = new Position[][]
        {
            new Position[] { new(0,0), new(0,1), new(1,0), new(1,1) },
            new Position[] { new(0,0), new(0,1), new(1,0), new(1,1) },
            new Position[] { new(0,0), new(0,1), new(1,0), new(1,1) },
            new Position[] { new(0,0), new(0,1), new(1,0), new(1,1) }
        };
        public override int Id => 4;
        protected override Position StartOffset => new Position(0, 4);
        protected override Position[][] Tiles => tiles;
    }
}
