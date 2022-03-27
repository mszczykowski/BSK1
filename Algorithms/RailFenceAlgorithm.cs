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
            char[][] matrix = new char[key][encrypted.length()];
            boolean dirDown = true;

            for (int i = 0; i < key; i++) {
                for (int j = 0; j < encrypted.length(); j++) {
                    matrix[i][j] = '\n';
                }
            }

            int row = 0;
            int column = 0;

            for (int i = 0; i < encrypted.length(); i++) {
                dirDown = setDirDown(key, dirDown, row);
                matrix[row][column++] = '*';
                row = updateRow(dirDown, row);
            }
            int index = 0;
            for (int j = 0; j < key; j++) {
                for (int i = 0; i < encrypted.length(); i++) {
                    if ((matrix[j][i] == '*') && index < encrypted.length()) {
                        matrix[j][i] = encrypted.charAt(index++);
                    }
                }
            }
            StringBuilder decryptedBuilder = new StringBuilder();
            row = 0;
            column = 0;
            for (int i = 0; i < encrypted.length(); i++) {
                dirDown = setDirDown(key, dirDown, row);

                if (matrix[row][column] != '*') {
                    decryptedBuilder.append(matrix[row][column++]);
                }
                row = updateRow(dirDown, row);
            }
            return decryptedBuilder.ToString();
        }

        public override string Encrypt(string input)
        {
            StringBuilder sb = new StringBuilder();
            int i;
            boolean[] taken;
            taken = new boolean[initialMessage.length()];
            for (i = 0; i < initialMessage.length(); i += Int32.Parse(_viewModel.Key) + 1) {
                if (!taken[i]) {
                    sb.append(initialMessage.charAt(i));
                    taken[i] = true;
                }
            }
            for (int j = 1; j < Int32.Parse(_viewModel.Key); j++) {
                for (i -= initialMessage.length() - 1; i < initialMessage.length(); i += (_viewModel.Key - j)) {
                    if (!taken[i]) {
                        sb.append(initialMessage.charAt(i));
                        taken[i] = true;
                }
            }
        }
        return sb;    
        }

        public override bool IsKeyValid(string key)
        {
            int x;
            return (Int32.TryParse(key, out x) && x > 0);
        }

        private int updateRow(boolean dirDown, int row) {
            if (dirDown) {
                row += 1;
            } else {
                row -= 1;
            }
            return row;
        }

        private boolean setDirDown(int key, boolean dirDown, int row) {
            if (row == 0) {
                dirDown = true;
            } else if (row == key -1) {
                dirDown = false;
            }
            return dirDown;
        } 
    }
}
