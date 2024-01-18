using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3
{
    public class PlayfairCipher
    {
        private char[,] matrix = new char[5,5];
        private string key;
        public PlayfairCipher(string key)
        {
            this.key = key;
            InitializeMatrix();
        }

        private void InitializeMatrix()
        {
            string cleanKey = new string(key.Distinct().Where(char.IsLetter).ToArray()).ToUpper();
            string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ";

            cleanKey = cleanKey.Replace("J", "I") + alphabet;

            cleanKey = new string(cleanKey.Distinct().ToArray());

            int keyCharIndex = 0;
            for (int i = 0; i < matrix.GetLength(0); i++) 
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = cleanKey[keyCharIndex++];
                }
            }
        }

        public string Encrypt(string plainText)
        {
            string preparedText = PreparePlainText(plainText);

            StringBuilder cipherText = new StringBuilder();

            for (int i = 0; i < preparedText.Length; i += 2)
            {
                CharMatrixPosition char1 = new CharMatrixPosition(matrix, preparedText[i]);
                CharMatrixPosition char2 = new CharMatrixPosition(matrix, preparedText[i + 1]);
                if (char1.Row == char2.Row)
                {
                    cipherText.Append(matrix[char1.Row, (char1.Column + 1) % 5]);
                    cipherText.Append(matrix[char2.Row, (char2.Column + 1) % 5]);
                }
                else if (char1.Column == char2.Column)
                {
                    cipherText.Append(matrix[(char1.Row + 1) % 5, char1.Column]);
                    cipherText.Append(matrix[(char2.Row + 1) % 5, char2.Column]);
                }
                else
                {
                    cipherText.Append(matrix[char1.Row, char2.Column]);
                    cipherText.Append(matrix[char2.Row, char1.Column]);
                }
            }

            return cipherText.ToString();
        }

        private string PreparePlainText(string plainText)
        {
            string fixlLetter = "X";
            plainText = plainText.Replace('J', 'I').ToUpper();
            plainText = string.Concat(plainText.Where(c => !char.IsWhiteSpace(c) && char.IsLetter(c)));

            string preparedText = plainText;

            for (int i = 0; i < preparedText.Length - 1; i++)
            {
                if (preparedText[i] == preparedText[i + 1])
                {
                    preparedText = preparedText.Insert(i + 1, fixlLetter);
                }
            }

            if (preparedText.Length % 2 != 0)
            {
                preparedText += fixlLetter;
            }

            return preparedText;
        }

        public string Decrypt(string ciphedText)
        {
            StringBuilder cipherText = new StringBuilder();

            for (int i = 0; i < ciphedText.Length; i += 2)
            {
                CharMatrixPosition char1 = new CharMatrixPosition(matrix, ciphedText[i]);
                CharMatrixPosition char2 = new CharMatrixPosition(matrix, ciphedText[i + 1]);
                if (char1.Row == char2.Row)
                {
                    cipherText.Append(matrix[char1.Row, (char1.Column + 4) % 5]);
                    cipherText.Append(matrix[char2.Row, (char2.Column + 4) % 5]);
                }
                else if (char1.Column == char2.Column)
                {
                    cipherText.Append(matrix[(char1.Row + 4) % 5, char1.Column]);
                    cipherText.Append(matrix[(char2.Row + 4) % 5, char2.Column]);
                }
                else
                {
                    cipherText.Append(matrix[char1.Row, char2.Column]);
                    cipherText.Append(matrix[char2.Row, char1.Column]);
                }
            }

            return cipherText.ToString();
        }

        public string GetStringMatrix()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++) 
                {
                    stringBuilder.Append(matrix[i, j] + " ");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}
