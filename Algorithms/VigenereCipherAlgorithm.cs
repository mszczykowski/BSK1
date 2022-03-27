using BSK1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSK1.Algorithms
{
    internal class VigenereCipherAlgorithm : Algorithm
    {
        public VigenereCipherAlgorithm(AlgorithmsFormViewModel viewModel) : base(viewModel)
        {
        }

        public override string Decrypt(string input)
        {
            String decryptedText = "";

            for (int i = 0; i < input.Length && 
                                i < _viewModel.Key.Length; i++) {
                int letter = (input[i] - _viewModel.Key[i] 
                                + 26) % 26;
                letter += 'A';
                decryptedText += (char) (letter);                        
            }
            return decryptedText;
        }

        public override string Encrypt(string input)
        {
            String encrypedText = "";

            for (int i = 0; i < input.Length; i++) {
                int letter = (input[i] + _viewModel[i]) % 26;

                letter += 'A';

                encrypedText += (char) (letter);
            }
            return encrypedText;
        }

        public override bool IsKeyValid(string key)
        {
            return !String.IsNullOrEmpty(key) && Regex.IsMatch(key, @"^[a-zA-Z]+$");
        }
    }
}
