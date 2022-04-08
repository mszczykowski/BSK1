using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSK1.Algorithms.TextAlgorithms
{
    internal class TranspositionCAlgorithm : Algorithm
    {
        public TranspositionCAlgorithm(AlgorithmsFormViewModel viewModel) : base(viewModel)
        {
        }

        public override string Encrypt(string input)
        {
            string key = _viewModel.KeyInput;

            int[] translatedKey = TranslateKeyToNumbers(key);

            int[] rowsLengths = CalculateRowsLengths(translatedKey);

            List<Row> matrix = GenerateMatrix(input, key, translatedKey, rowsLengths);

            string result = "";

            for(int i = 0; i < rowsLengths.Length; i++)
            {
                matrix.ForEach((row) =>
                {
                    if (row.HasLetterAt(rowsLengths[i] - 1)) result += row.GetLetterAt(rowsLengths[i] - 1);
                });
            }

            return result;
        }

        public override string Decrypt(string input)
        {

            string key = _viewModel.KeyInput.ToUpper();

            int[] translatedKey = TranslateKeyToNumbers(key);

            int[] rowsLengths = CalculateRowsLengths(translatedKey);

            List<Row> matrix = GenerateMatrix(input, key, translatedKey, rowsLengths);

            int positionInKey = 0;

            int positionInInput = 0;

            int currentColumn = 0;

            int currentRow = 0;

            while (positionInInput < input.Length)
            {
                if (positionInKey >= key.Length) positionInKey = 0;

                currentColumn = 0;
                currentRow = 0;

                while (positionInKey != translatedKey[currentColumn])
                {
                    currentColumn++;
                }

                while (positionInInput < input.Length && currentRow < matrix.Count)
                {
                    if (matrix[currentRow].HasLetterAt(currentColumn)) matrix[currentRow].SetLetterAt(currentColumn, input[positionInInput++]);
                    currentRow++;
                }

                positionInKey++;
            }

            string result = "";

            matrix.ForEach((row) =>
            {
                result += row.GetString();
            });

            return result;
        }

        private List<Row> GenerateMatrix(string input, string key, int[] translatedKey, int[] rowsLengths)
        {
            List<Row> matrix = new List<Row>();

            int positionInKey = 0;

            int positionInInput = 0;

            while (positionInInput < input.Length)
            {
                if (positionInKey >= key.Length) positionInKey = 0;

                Row row = new Row(rowsLengths[positionInKey++]);

                while (!row.IsFull() && positionInInput < input.Length)
                {
                    row.AddLetter(input[positionInInput++]);
                }

                matrix.Add(row);
            }

            return matrix;
        }

        private int[] TranslateKeyToNumbers(string key)
        {
            int[] result = new int[key.Length];

            Array.Fill<int>(result, -1);
            
            char[] keySorted = SortAlphabeticly(key);

            for(int i = 0; i < key.Length; i++)
            {
                for(int j = 0; j < key.Length; j++)
                {
                    if(keySorted[i] == key[j] && result[j] == -1)
                    {
                        result[j] = i;
                        break;
                    }
                }
            }

            return result;
        }

        public int[] CalculateRowsLengths(int[] translatedKey)
        {
            int[] result = new int[(int)translatedKey.Length];

            for(int i = 0; i < translatedKey.Length; i++)
            {
                result[translatedKey[i]] = i + 1;
            }

            return result;
        }

        private char[] SortAlphabeticly(string input)
        {
            char[] keySorted = input.ToCharArray();
            Array.Sort(keySorted);
            return keySorted;
        }

        private class Row
        {
            private char[] letters;
            private int position;
            public Row(int capacity)
            {
                letters = new char[capacity];
                position = 0;
            }

            public bool IsFull()
            {
                return position >= letters.Length;
            }

            public bool HasLetterAt(int pos)
            {
                return pos < letters.Length && Char.IsLetter(letters[pos]);
            }

            public void AddLetter(char c)
            {
                letters[position++] = c;
            }

            public char GetLetterAt(int pos)
            {
                return letters[pos];
            }

            public void SetLetterAt(int pos, char c)
            {
                letters[pos] = c;
            }

            public string GetString()
            {
                return new String(letters);
            }
        }

        public override bool IsKeyValid(string key)
        {
            return !String.IsNullOrEmpty(key) && Regex.IsMatch(key, @"^[a-zA-Z]+$");
        }
    }

    
}
