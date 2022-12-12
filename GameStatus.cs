namespace Kursach
{
    internal class GameStatus
    {
        private Block currentBlock;
        private int width;
        public Block CurrentBlock
        {
            get { return currentBlock; }
            set
            {
                currentBlock = value;
                currentBlock.Reset(); //устанавливаем начальное положение
            }
        }
        public Grid _Grid { get; }
        public BlockQueue _BlockQueue { get; }
        public bool GameOver { get; set; }
        public int Scores { get; set; }
        public GameStatus(int height, int width)
        {
            _Grid = new Grid(height, width);
            this.width = width;
            _BlockQueue = new BlockQueue();
            CurrentBlock = _BlockQueue.GetAndUpdate();
            Block.Width = width;
        }
        
        private bool BlockFits()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if (!_Grid.Empty(p.Row, p.Column))
                    return false;
                if (CurrentBlock.Offset.Column + p.Column >= width)
                {
                    CurrentBlock.Offset.Column -= width;
                }
                else if(p.Column + CurrentBlock.Offset.Column < 0)
                {
                    CurrentBlock.Offset.Column += width;
                }
            }
            return true;
        }
        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }
        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }
        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }
        public void MoveBlockRight()
        {



            CurrentBlock.Move(0, 1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
           
        }
        private bool IsGameOver()
        {
            return !(_Grid.RowEmptyChecker(0) && _Grid.RowEmptyChecker(1));
        }
        private void PlaceBlock()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                _Grid[p.Row, p.Column] = CurrentBlock.Id;
            }
            //Scores += _Grid.ClearAllRows();

            // где - то тут
            if (currentBlock.Id == 8)
            {
                foreach (Position p in CurrentBlock.TilePositions())
                {

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (_Grid.Insider(p.Row + i, p.Column + j))
                            {
                                _Grid[p.Row + i, p.Column + j] = 0;
                            }
                        }
                    }
                }
            }

            Scores = _Grid.ClearAllRowsFull();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = _BlockQueue.GetAndUpdate();
            }
        }
        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }
    }
}