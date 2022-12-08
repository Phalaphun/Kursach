namespace Kursach
{
    internal  class Block
    {
        protected Position[][] Tiles { get; set; }
        protected Position StartOffset { get; set; }
        public int Id { get; set; }
        private int rotationState;
        private Position offset;
        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
        }
        public Block(Position[][] Tiles, Position StartOffset, int Id)
        {
            this.Id = Id;
            this.Tiles = Tiles;
            this.StartOffset = StartOffset;
            offset = new Position(this.StartOffset.Row, this.StartOffset.Column);
        }
        public void Move(int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }
        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
        public IEnumerable<Position> TilePositions()
        {   
            foreach(Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }
        public void RotateCW()
        {
            if(rotationState !=3)
            {
                rotationState++;
            }
            else { rotationState=0; }
        }
        public void RotateCCW() 
        {
            if(rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }
    }
}