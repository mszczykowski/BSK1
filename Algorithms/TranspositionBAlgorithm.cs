using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSK1.Algorithms
{
    internal class TranspositionBAlgorithm : Algorithm
    {
        public TranspositionBAlgorithm(AlgorithmsFormViewModel viewModel) : base(viewModel)
        {
        }

        public override string Encrypt(string input)
        {
            if (_viewModel.Key == null || _viewModel.Key.Length <= 0) // Checking if key is null or empty
                return "Klucz nie może być pusty.";

            string key = _viewModel.Key; // Key without spaces
            string keyAlphabetical = SortString(key); // Key with alphabetical letters

            bool[] wasLetterUsed = new bool[key.Length]; // Array of key letters
            for (int i = 0; i < wasLetterUsed.Length; i++)
                wasLetterUsed[i] = false;

            List<string> wordsTable = SplitInParts(input, key.Length).ToList(); // Diving input by key length and generating table

            string result = "";
            foreach (char alphabeticalLetter in keyAlphabetical)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if(alphabeticalLetter == key[i] && wasLetterUsed[i] == false)
                    {
                        foreach (string word in wordsTable)
                        {
                            if (word.Length <= i)
                                break;

                            result += word[i];
                        }
                        wasLetterUsed[i] = true;
                    }
                }
            }

            return result;
        }

        public override string Decrypt(string input)
        {
            throw new NotImplementedException();
        }

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

        static string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }
    }
}
