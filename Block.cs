using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach
{
    internal abstract class Block
    {
        protected abstract Position[][] Tiles { get; }
        protected abstract Position StartOffset { get; }
        public abstract int Id { get; }

        private int rotationState;
        private Position offset;

        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
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
            //Берется интерфейс, который позволяет использовать foreach, дальше с помощью yield итератора
            // выводятся все положения блока учитывая его повороты
            foreach(Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        public void RotateCW() // по часовой
        {
            //rotationState = (rotationState + 1) % Tiles.Length;

            if(rotationState !=3)
            {
                rotationState++;
            }
            else { rotationState=0; }
        }
        public void RotateCCW() // против часовой
        {
            if(rotationState == 0)
            {
                rotationState = Tiles.Length - 1; // от масимального числа состояний отнимаем 1
            }
            else
            {
                rotationState--;
            }
        }
    }
}
