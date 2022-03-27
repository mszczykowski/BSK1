using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSK1.Algorithms
{
    internal class TranspositionAAlgorithm : Algorithm
    {
        public TranspositionAAlgorithm(AlgorithmsFormViewModel viewModel) : base(viewModel)
        {
        }

        public override string Encrypt(string input)
        {
            int[] key = Array.ConvertAll(_viewModel.Key.Split('-'), int.Parse); // Ints array (chars divided by hyphen converted to int)

            List<string> wordsTable = SplitInParts(input, key.Length).ToList();

            string result = "";
            for (int i = 0; i < key.Length; i++)
            {
                int currentKey = key[i];
                foreach (string word in wordsTable)
                {
                    if (word.Length <= currentKey - 1)
                        break;

                    result += word[currentKey - 1];
                }
            }

            return result;
        }

        public override string Decrypt(string input)
        {
            int[] key = Array.ConvertAll(_viewModel.Key.Split('-'), int.Parse); // Ints array (chars divided by hyphen converted to int)
            string inputToCut = input;

            string[] wordsTableVertical = new string[key.Length];

            int wordsLength = (int)Math.Ceiling((double)input.Length / (double)key.Length);

            /* Cutting to words and adding to words table (vertically) */
            for (int i = 0; i < key.Length; i++)
            {
                int currentKey = key[i];
                int tempCount = wordsLength;

                int divisionRest = input.Length % key.Length;
                if (divisionRest != 0 && divisionRest < currentKey)
                    tempCount -= 1;

                wordsTableVertical[currentKey - 1] = inputToCut.Substring(0, tempCount);
                inputToCut = inputToCut.Remove(0, tempCount);
            }

            /* Reading from words table (horizontally) */
            string result = "";
            for (int i = 0; i < wordsLength; i++)
            {
                for (int j = 0; j < key.Length; j++)
                {
                    if (wordsTableVertical[j].Length <= i)
                        break;
                    result += wordsTableVertical[j][i];
                }
            }

            return result;
        }

        // Methods
        public IEnumerable<string> SplitInParts(string s, int partLength)
        {
            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public override bool IsKeyValid(string key)
        {
            return !String.IsNullOrEmpty(key) && Regex.IsMatch(key, @"^([0-9]+-)*[0-9]+$");
        }
    }
}
