namespace Kursach
{
    internal class Grid
    {
        private int[,] grid;
        private int rows;
        private int columns;
        public int Cleared { get; set; }
        public int Rows { get { return rows;} }
        public int Columns { get { return columns; } }
        public Grid(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            grid = new int[rows, columns];
        }
        public int this[int r, int c]
        {
            get
            {
                return this.grid[r, c];
            }
            set
            {
                this.grid[r, c] = value;
            }
        }
        public bool Insider(int r, int c)
        {
            return (r >= 0 && r < Rows) && (c >= 0 && c < Columns);
        }
        public bool Empty(int r, int c)
        {
            return Insider(r, c) && grid[r, c] == 0;
        }
        public bool RowFullChecker(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                    return false;
            }
            return true;
        }
        public bool RowEmptyChecker(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                    return false;
            }
            return true;
        }
        private void ClearRow(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }
        private void MoveRowDown(int r, int numRows)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (r == Rows-1)
                {
                    grid[r, c] = 0;
                    
                }
                else
                {
                    grid[r + numRows, c] = grid[r, c];
                    grid[r, c] = 0;
                }
            }
        }

        public int ClearAllRows()
        {
            int cleared = 0; //нужна чтобы определить на сколько потом сдвинуть все строчки вниз
            for (int r = Rows - 1; r > 0; r--) // идём сверху вниз см.MoveRowDown 
            {
                if(RowFullChecker(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                if(cleared>0)
                {
                    MoveRowDown(r-1, cleared);
                }
            }
            return cleared;
        }
        public int ClearRows()
        {
            
            while (RowFullCheckerAll())
            {
                Cleared += ClearAllRows();
            }
            return Cleared;
        }
        private bool RowFullCheckerAll()
        {
            bool temp = false;
            for(int r = Rows - 1; r > 0; r--)
            {
                temp = RowFullChecker(r);
                if (temp)
                {
                    break;
                }
            }
            return temp;
        }
    }
}