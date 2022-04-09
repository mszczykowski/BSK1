using BSK1.Algorithms.KeyGenerators;
using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSK1.Algorithms.BinaryAlgorithms
{
    internal class StreamCipher : Algorithm
    {
        public StreamCipher(AlgorithmsFormViewModel viewModel) : base(viewModel)
        {
        }

        public override string Decrypt(string input)
        {
            return this.Encrypt(input);
        }

        public override string Encrypt(string input)
        {
            IKeyGenerator keyGenerator = _viewModel.AlgorithmViewModel.KeyGenerator;

            string key = keyGenerator.GenerateKey(input.Length);


            for (int i = 0; i < input.Length; i++)
            {
                string replacement = (input[i] ^ key[i]).ToString();
                input = input.Remove(i, 1).Insert(i, replacement);
            }

            return input;
        }

        public override bool IsKeyValid(string key)
        {
            if (String.IsNullOrEmpty(key) || !Regex.IsMatch(key, @"^ *\d+ *(?:, *\d+ *)*$"))
                return false;

            int[] powers = key.Replace(" ", string.Empty).Split(",").Select(int.Parse).Distinct().OrderBy(x => x).ToArray();

            if (powers.Any(x => x <= 0))
                return false;

            return true;
        }

    }
}
