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
            char[][] matrix = new char[Int32.Parse(_viewModel.Key)][];
            for (int i = 0; i < Int32.Parse(_viewModel.Key); i++){
                matrix[i] = new char[input.Length];
            }
            bool dirDown = true;

            for (int i = 0; i < Int32.Parse(_viewModel.Key); i++) {
                for (int j = 0; j < input.Length; j++) {
                    matrix[i][j] = '\n';
                }
            }

            int row = 0;
            int column = 0;

            for (int i = 0; i < input.Length; i++) {
                dirDown = setDirDown(Int32.Parse(_viewModel.Key), dirDown, row);
                matrix[row][column++] = '*';
                row = updateRow(dirDown, row);
            }
            int index = 0;
            for (int j = 0; j < Int32.Parse(_viewModel.Key); j++) {
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
                dirDown = setDirDown(Int32.Parse(_viewModel.Key), dirDown, row);

                if (matrix[row][column] != '*') {
                    decryptedBuilder.Append(matrix[row][column++]);
                }
                row = updateRow(dirDown, row);
            }
            return decryptedBuilder.ToString();
        }

        public override string Encrypt(string input)
        {
            StringBuilder sb = new StringBuilder();
            int i;
            bool[] taken;
            taken = new bool[input.Length];
            for (i = 0; i < input.Length; i += Int32.Parse(_viewModel.Key) + 1) {
                if (!taken[i]) {
                    sb.Append(input[i]);
                    taken[i] = true;
                }
            }
            for (int j = 1; j < Int32.Parse(_viewModel.Key); j++) {
                for (i -= input.Length - 1; i < input.Length; i += (Int32.Parse(_viewModel.Key) - j)) {
                    if (!taken[i]) {
                        sb.Append(input[i]);
                        taken[i] = true;
                }
            }
        }
        return sb.ToString();    
        }

        public override bool IsKeyValid(string key)
        {
            int x;
            return (Int32.TryParse(key, out x) && x > 0);
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
