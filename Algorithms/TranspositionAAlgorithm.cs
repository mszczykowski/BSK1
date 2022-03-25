using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            string key = _viewModel.Key.Replace("-", string.Empty); // Key without dash

            List<string> wordsTable = SplitInParts(input, key.Length).ToList();

            string result = "";
            for (int i = 0; i < key.Length; i++)
            {
                int currentKey = key[i] - '0';
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
            //string key = _viewModel.Key.Replace("-", string.Empty); // Key without dash

            //string[] wordsTableVertical = new string[key.Length];
        }

        /*public override bool IsKeyValid(string key)
        {
            throw new NotImplementedException();
        }*/

        // Methods
        public IEnumerable<string> SplitInParts(string s, int partLength)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public override bool IsKeyValid(string key)
        {
            throw new NotImplementedException();
        }
    }
}
