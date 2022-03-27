using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Algorithms
{
    internal class CaesarCipherAlgorithm : Algorithm
    {
        private const int SHIFT = 'A';
        private const int ALPHABET_LENGTH = 26;
        public CaesarCipherAlgorithm(AlgorithmsFormViewModel viewModel) : base(viewModel)
        {
        }

        public override string Encrypt(string input)
        {
            int k = Int32.Parse(_viewModel.Key);

            string output = "";

            input.ToList().ForEach(x =>
            {
                int a = x - SHIFT;

                int c = Mod((a + k), ALPHABET_LENGTH);

                output += Convert.ToChar(c + SHIFT);
            });

            return output;
        }

        public override string Decrypt(string input)
        {
            int k = Int32.Parse(_viewModel.Key);

            string output = "";

            input.ToList().ForEach(x =>
            {
                int c = x - SHIFT;

                int a = Mod(c + (ALPHABET_LENGTH - k), ALPHABET_LENGTH);

                output += Convert.ToChar(a + SHIFT);
            });

            return output;
        }

        private int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public override bool IsKeyValid(string key)
        {
            int x;
            return (Int32.TryParse(key, out x) && x > 0);
        }
    }
}
