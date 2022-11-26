namespace Kursach
{
    // суть - разделить всё доступное поле на ячейки
    // каждая ячейка будет хранить значение, указывающее на наличие фигуры внутри. 0 - нет фигуры
    /// <summary>
    /// по сути координатное поле для игры.
    /// </summary>
    internal class Grid
    {
        private int[,] grid;
        private int rows;
        private int columns;


        public int Rows { get { return rows;} }
        public int Columns { get { return columns; } }

        public Grid(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            grid = new int[rows, columns];
        }

        public int this[int r, int c] // получаем доступ к массиву
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

        /// <summary>
        /// ВОзвращает true если клетка находится в массиве (чтобы определить поле в котором работаем)
        /// </summary>
        /// <param name="r">строка</param>
        /// <param name="c">столбец</param>
        /// <returns></returns>
        public bool Insider(int r, int c)
        {
            return (r >= 0 && r < Rows) && (c >= 0 && c < Columns);
        }
        /// <summary>
        /// Возвращает true если клетка существует и пуста, иначе false
        /// </summary>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool Empty(int r, int c)
        {
            return Insider(r, c) && grid[r, c] == 0;
        }

        /// <summary>
        /// Возвращает true если строка заполнен, иначе false
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool RowFullChecker(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Возвращает true если строка заполнена, иначе false
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool RowEmptyChecker(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Удаляет все блоки в строке
        /// </summary>
        /// <param name="r"></param>
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
                //Это так, потому что нумерация идёт так, что Rows находится внизу. На верху же как бы 0. Поэтому и прибаляю чтобы пойти вниз 

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
            int cleared = 0;
            while (RowFullCheckerAll())
            {
                cleared+=ClearAllRows();
            }
            return cleared;
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
