using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    public class CharMatrixPosition
    {
        private char[,] matrix;
        private char letter;
        private int row, column;

        public CharMatrixPosition(char[,] matrix, char letter)
        {
            this.matrix = (char[,])matrix.Clone();
            this.letter = letter;
            GetIndexes();
        }

        private void GetIndexes()
        {
            bool letterIsFind = false;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (letter == matrix[i, j])
                    {
                        row = i;
                        column = j;
                        letterIsFind = true;
                    }
                }

                if (letterIsFind) 
                { 
                    break; 
                }
            }
        }

        public int Row
        {
            get => row;
        }

        public int Column
        {
            get => column;
        }
    }  
}
