using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSK1.ViewModels;

namespace BSK1.Algorithms
{
    internal class RailFenceAlgorithm : Algorithm
    {
        public RailFenceAlgorithm(AlgorithmsFormViewModel viewModel) : base(viewModel)
        {
        }

        public override string Decrypt(string input)
        {
            int key = Int32.Parse(_viewModel.Key);
            char[][] matrix = new char[key][];
            for (int i = 0; i < key; i++){
                matrix[i] = new char[input.Length];
            }
            bool dirDown = true;

            for (int i = 0; i < key; i++) {
                for (int j = 0; j < input.Length; j++) {
                    matrix[i][j] = '\n';
                }
            }

            int row = 0;
            int column = 0;

            for (int i = 0; i < input.Length; i++) {
                dirDown = setDirDown(key, dirDown, row);
                matrix[row][column++] = '*';
                row = updateRow(dirDown, row);
            }
            int index = 0;
            for (int j = 0; j < key; j++) {
                for (int i = 0; i < input.Length; i++) {
                    if ((matrix[j][i] == '*') && index < input.Length) {
                        matrix[j][i] = input[index++];
                    }
                }
            }
            StringBuilder decryptedBuilder = new StringBuilder();
            row = 0;
            column = 0;
            for (int i = 0; i < input.Length; i++) {
                dirDown = setDirDown(key, dirDown, row);

                if (matrix[row][column] != '*') {
                    decryptedBuilder.Append(matrix[row][column++]);
                }
                row = updateRow(dirDown, row);
            }
            return decryptedBuilder.ToString();
        }

        public override string Encrypt(string input)
        {
            int key = Int32.Parse(_viewModel.Key);
            char[][] matrix = new char[key][];
            for (int i = 0; i < key; i++)
            {
                matrix[i] = new char[input.Length];
            }
            for (int i = 0; i < key; i++) {
                for (int j = 0; j < input.Length; j++) { 
                    matrix[i][j] = '\n'; 
                }
                    
            }

            bool dirDown = false;
            int row = 0, col = 0;

            for (int i = 0; i < input.Length; i++) {
                if (row == 0 || row == key - 1)
                {
                    dirDown = !dirDown;
                }
                    matrix[row][col++] = input[i];
                if (dirDown)
                {
                    row++;
                }
                else
                {
                    row--;
                }
            }


            String encryptedText = "";
            for (int i = 0; i < key; i++) {
                for (int j = 0; j < input.Length; j++) {
                    if (matrix[i][j] != '\n') {
                        encryptedText += matrix[i][j];
                    }
                }
            }
      
        return encryptedText;    
        }

        public override bool IsKeyValid(string key)
        {
            int x;
            return (Int32.TryParse(key, out x) && x > 1);
        }

        private int updateRow(bool dirDown, int row) {
            if (dirDown) {
                row += 1;
            } else {
                row -= 1;
            }
            return row;
        }

        private bool setDirDown(int key, bool dirDown, int row) {
            if (row == 0) {
                dirDown = true;
            } else if (row == key -1) {
                dirDown = false;
            }
            return dirDown;
        } 
    }
}
