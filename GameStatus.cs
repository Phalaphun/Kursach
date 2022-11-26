namespace Kursach
{
    internal class GameStatus
    {
        private Block currentBlock;
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
        public GameStatus(int M, int N)
        {
            _Grid = new Grid(M,N);
            _BlockQueue = new BlockQueue();
            CurrentBlock = _BlockQueue.GetAndUpdate();
        }
        private bool BlockFits()
        {
            foreach(Position p in CurrentBlock.TilePositions())
            {
                if(!_Grid.Empty(p.Row,p.Column))
                    return false;

            }
            return true;
        }
        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            if(!BlockFits())
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
            if(!BlockFits())
            {
                CurrentBlock.Move(0,1);
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
            foreach(Position p in CurrentBlock.TilePositions())
            {
                _Grid[p.Row, p.Column] = CurrentBlock.Id;
            }
            //_Grid.ClearAllRows();
            _Grid.ClearRows();

            if(IsGameOver())
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

            if(!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }    
        }
    }
}
